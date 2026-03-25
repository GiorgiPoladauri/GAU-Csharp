// =============================================================
//  FILE: Behaviors/Service/CustomServiceBehavior.cs
//
//  TOPIC: IServiceBehavior
//
//  This file demonstrates how to build a custom WCF service-level
//  behavior. It contains:
//    1. GlobalErrorHandler      — implements IErrorHandler
//    2. LoggingInstanceProvider — implements IInstanceProvider
//    3. CustomServiceBehavior   — implements IServiceBehavior  ← MAIN CLASS
// =============================================================

using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WcfBehaviorsDemo.Behaviors.Service
{
    // ==============================================================
    //  HELPER 1 — GlobalErrorHandler
    //
    //  WCF calls this whenever an unhandled exception bubbles out
    //  of any service operation on this host.
    //
    //  Without a custom error handler, WCF sends a generic fault
    //  to the client AND closes the channel after an exception.
    //  Our handler gives us full control over both behaviors.
    // ==============================================================
    public class GlobalErrorHandler : IErrorHandler
    {
        // -----------------------------------------------------------------
        // ProvideFault — Step 1: convert the raw .NET exception into
        // a SOAP FaultMessage that WCF can serialize and send to the client.
        //
        // If you skip this (or leave fault as null), WCF sends its own
        // default fault — usually with very little information.
        // -----------------------------------------------------------------
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            Console.WriteLine($"\n  [GlobalErrorHandler] ProvideFault → {error.GetType().Name}: {error.Message}");

            // Wrap the exception message in a standard WCF FaultException
            var faultException = new FaultException(
                reason:    $"Server error: {error.Message}",
                code:      new FaultCode("ServerError"));

            // Convert FaultException → MessageFault → Message
            var messageFault = faultException.CreateMessageFault();
            fault = Message.CreateMessage(version, messageFault, faultException.Action);
        }

        // -----------------------------------------------------------------
        // HandleError — Step 2: decide what happens AFTER the fault is sent.
        //
        // Return TRUE  → channel stays open (client can keep calling)
        // Return FALSE → WCF closes the channel (client must reconnect)
        //
        // This is the ideal place for structured logging / alerting.
        // -----------------------------------------------------------------
        public bool HandleError(Exception error)
        {
            Console.WriteLine($"  [GlobalErrorHandler] HandleError → logged and channel kept alive.");
            // In production: write to a log file, Event Log, or Serilog/NLog here
            return true; // keep the channel alive
        }
    }


    // ==============================================================
    //  HELPER 2 — LoggingInstanceProvider
    //
    //  By default WCF creates and releases service objects silently.
    //  Replacing the default IInstanceProvider lets us observe
    //  (or fully control) service object lifecycle.
    //
    //  Useful for: object pooling, dependency injection, audit trails.
    // ==============================================================
    public class LoggingInstanceProvider : IInstanceProvider
    {
        // We store the service type so we can instantiate it via reflection
        private readonly Type _serviceType;

        public LoggingInstanceProvider(Type serviceType)
        {
            _serviceType = serviceType;
        }

        // Called by WCF when it needs a new service instance
        public object GetInstance(InstanceContext instanceContext)
        {
            var instance = Activator.CreateInstance(_serviceType);
            Console.WriteLine($"  [LoggingInstanceProvider] Created → {_serviceType.Name} (id: {instance.GetHashCode()})");
            return instance;
        }

        // Overload used when the incoming Message is also available
        public object GetInstance(InstanceContext instanceContext, Message message)
            => GetInstance(instanceContext);

        // Called by WCF when it is done with the instance
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            Console.WriteLine($"  [LoggingInstanceProvider] Released → {instance.GetType().Name} (id: {instance.GetHashCode()})");
            (instance as IDisposable)?.Dispose(); // safe cleanup if IDisposable
        }
    }


    // ==============================================================
    //  MAIN CLASS — CustomServiceBehavior  (implements IServiceBehavior)
    //
    //  This is what you attach to a ServiceHost to extend the ENTIRE
    //  service. WCF calls the three methods below in this order:
    //    1. Validate
    //    2. AddBindingParameters
    //    3. ApplyDispatchBehavior
    //
    //  We also extend Attribute so it can be used as [CustomServiceBehavior]
    //  directly on a service class if desired.
    // ==============================================================
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomServiceBehavior : Attribute, IServiceBehavior
    {
        // ------------------------------------------------------------
        // 1. Validate
        //    WCF calls this BEFORE opening the host.
        //    Throw InvalidOperationException to block startup if config is wrong.
        // ------------------------------------------------------------
        public void Validate(ServiceDescription serviceDescription,
                             ServiceHostBase serviceHostBase)
        {
            Console.WriteLine("\n  [CustomServiceBehavior] Validate() ─ checking configuration...");

            if (serviceDescription.Endpoints.Count == 0)
                throw new InvalidOperationException(
                    "The service must expose at least one endpoint.");

            Console.WriteLine($"  [CustomServiceBehavior] Validate() ─ OK. " +
                              $"Service={serviceDescription.ServiceType.Name}, " +
                              $"Endpoints={serviceDescription.Endpoints.Count}");
        }

        // ------------------------------------------------------------
        // 2. AddBindingParameters
        //    Use this to pass extra information to binding elements
        //    (e.g., custom security tokens, message encoders).
        //    Most demos leave this empty — it still must be implemented.
        // ------------------------------------------------------------
        public void AddBindingParameters(ServiceDescription serviceDescription,
                                         ServiceHostBase serviceHostBase,
                                         Collection<ServiceEndpoint> endpoints,
                                         BindingParameterCollection bindingParameters)
        {
            Console.WriteLine("  [CustomServiceBehavior] AddBindingParameters() ─ nothing to inject.");
        }

        // ------------------------------------------------------------
        // 3. ApplyDispatchBehavior  ← THE MOST IMPORTANT METHOD
        //    Called after WCF builds the runtime, before it opens.
        //
        //    serviceHostBase.ChannelDispatchers has one entry per
        //    listening channel (one per endpoint/transport binding).
        //    We loop through all of them to attach our components.
        // ------------------------------------------------------------
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription,
                                          ServiceHostBase serviceHostBase)
        {
            Console.WriteLine("  [CustomServiceBehavior] ApplyDispatchBehavior() ─ wiring components...");

            var errorHandler = new GlobalErrorHandler();

            foreach (ChannelDispatcherBase dispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = dispatcherBase as ChannelDispatcher;
                if (channelDispatcher == null) continue;

                // ── Attach the error handler ──────────────────────────────
                // Every channel dispatcher has an ErrorHandlers list.
                // WCF walks this list when an exception is thrown.
                channelDispatcher.ErrorHandlers.Add(errorHandler);
                Console.WriteLine($"  [CustomServiceBehavior] GlobalErrorHandler attached to channel: " +
                                  $"{channelDispatcher.Listener?.Uri}");

                // ── Attach the instance provider ──────────────────────────
                // Each channel has one or more EndpointDispatchers.
                // The DispatchRuntime.InstanceProvider controls object lifecycle.
                foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                {
                    endpointDispatcher.DispatchRuntime.InstanceProvider =
                        new LoggingInstanceProvider(serviceDescription.ServiceType);

                    Console.WriteLine($"  [CustomServiceBehavior] LoggingInstanceProvider attached to: " +
                                      $"{endpointDispatcher.EndpointAddress?.Uri?.AbsoluteUri ?? "(mex)"}");
                }
            }
        }
    }
}

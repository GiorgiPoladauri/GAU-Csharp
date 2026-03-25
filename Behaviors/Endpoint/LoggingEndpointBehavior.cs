// =============================================================
//  FILE: Behaviors/Endpoint/LoggingEndpointBehavior.cs
//
//  TOPIC: IEndpointBehavior
//
//  This file demonstrates how to build a custom WCF endpoint-level
//  behavior. It contains:
//    1. LoggingMessageInspector — implements BOTH
//                                 IDispatchMessageInspector (server)
//                                 IClientMessageInspector   (client)
//    2. LoggingEndpointBehavior — implements IEndpointBehavior ← MAIN CLASS
//
//  KEY DIFFERENCE from IServiceBehavior:
//    IEndpointBehavior has ApplyClientBehavior() — meaning it can
//    hook into the CLIENT-SIDE WCF runtime as well, not just the server.
// =============================================================

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WcfBehaviorsDemo.Behaviors.Endpoint
{
    // ==============================================================
    //  HELPER — LoggingMessageInspector
    //
    //  A "message inspector" sits between the WCF transport layer
    //  and your service/client code. It sees every raw SOAP message.
    //
    //  IDispatchMessageInspector = server side
    //  IClientMessageInspector   = client side
    //
    //  We implement BOTH in one class so a single instance can be
    //  attached to either side depending on the context.
    // ==============================================================
    public class LoggingMessageInspector : IDispatchMessageInspector, IClientMessageInspector
    {
        private readonly string _side; // "SERVER" or "CLIENT" — makes console output clearer

        public LoggingMessageInspector(string side)
        {
            _side = side;
        }

        // ─────────────────────────────────────────────────────────────
        //  SERVER-SIDE HOOKS (IDispatchMessageInspector)
        // ─────────────────────────────────────────────────────────────

        // AfterReceiveRequest — fires after WCF deserializes the incoming
        // message but BEFORE it routes it to the right operation method.
        //
        // Return value = "correlationState" — any object you want WCF to
        // pass back to you in BeforeSendReply. We pass a DateTime so we
        // can measure how long the operation took.
        public object AfterReceiveRequest(ref Message request,
                                          IClientChannel channel,
                                          InstanceContext instanceContext)
        {
            Console.WriteLine($"\n  [{_side} Inspector] ─── Incoming Request ───────────────────");
            Console.WriteLine($"  [{_side} Inspector] Action : {request.Headers.Action}");
            Console.WriteLine($"  [{_side} Inspector] To     : {request.Headers.To}");

            // Read a custom header if the client sent one
            int idx = request.Headers.FindHeader("X-Client-ID", "http://wcfdemo.headers");
            if (idx >= 0)
            {
                string clientId = request.Headers.GetHeader<string>(idx);
                Console.WriteLine($"  [{_side} Inspector] Header X-Client-ID = {clientId}");
            }

            return DateTime.UtcNow; // ← correlationState: the time we received the request
        }

        // BeforeSendReply — fires just BEFORE WCF sends the reply to the client.
        // correlationState is what we returned from AfterReceiveRequest.
        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            var receivedAt = (DateTime)correlationState;
            var elapsed    = DateTime.UtcNow - receivedAt;
            Console.WriteLine($"  [{_side} Inspector] ─── Outgoing Reply ────────────────────");
            Console.WriteLine($"  [{_side} Inspector] Processing time: {elapsed.TotalMilliseconds:F2} ms");
        }

        // ─────────────────────────────────────────────────────────────
        //  CLIENT-SIDE HOOKS (IClientMessageInspector)
        // ─────────────────────────────────────────────────────────────

        // BeforeSendRequest — fires just BEFORE the client sends the message.
        // Perfect for injecting custom headers (auth tokens, trace IDs, etc.)
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            Console.WriteLine($"\n  [{_side} Inspector] ─── Sending Request ──────────────────");
            Console.WriteLine($"  [{_side} Inspector] Action : {request.Headers.Action}");

            // Inject a custom SOAP header so the server can identify the client
            var header = MessageHeader.CreateHeader(
                name:  "X-Client-ID",
                ns:    "http://wcfdemo.headers",
                value: $"DemoClient-{Environment.MachineName}");

            request.Headers.Add(header);
            Console.WriteLine($"  [{_side} Inspector] Injected header X-Client-ID");

            return DateTime.UtcNow; // ← correlationState: time we sent the request
        }

        // AfterReceiveReply — fires after the client receives the server's response.
        // correlationState is what we returned from BeforeSendRequest.
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            var sentAt    = (DateTime)correlationState;
            var roundTrip = DateTime.UtcNow - sentAt;
            Console.WriteLine($"  [{_side} Inspector] ─── Reply Received ───────────────────");
            Console.WriteLine($"  [{_side} Inspector] Round-trip time: {roundTrip.TotalMilliseconds:F2} ms");
        }
    }


    // ==============================================================
    //  MAIN CLASS — LoggingEndpointBehavior (implements IEndpointBehavior)
    //
    //  This is what you attach to a SINGLE endpoint — either on the
    //  ServiceHost (server) or on a ChannelFactory (client).
    //
    //  Methods called in order:
    //    1. Validate
    //    2. AddBindingParameters
    //    3. ApplyDispatchBehavior  (if attached to a ServiceHost endpoint)
    //    4. ApplyClientBehavior    (if attached to a ChannelFactory endpoint)
    // ==============================================================
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class LoggingEndpointBehavior : Attribute, IEndpointBehavior
    {
        // ------------------------------------------------------------
        // 1. Validate
        //    Endpoint-level validation — runs before the host/factory opens.
        // ------------------------------------------------------------
        public void Validate(ServiceEndpoint endpoint)
        {
            Console.WriteLine($"\n  [LoggingEndpointBehavior] Validate() ─ endpoint: {endpoint.Address?.Uri}");

            if (endpoint.Contract.Operations.Count == 0)
                throw new InvalidOperationException(
                    $"Endpoint '{endpoint.Address}' has a contract with no operations.");

            Console.WriteLine($"  [LoggingEndpointBehavior] Validate() ─ OK. " +
                              $"Contract={endpoint.Contract.Name}, " +
                              $"Operations={endpoint.Contract.Operations.Count}");
        }

        // ------------------------------------------------------------
        // 2. AddBindingParameters
        //    Inject extra data the binding layer may need.
        //    Left empty in this demo — still required by the interface.
        // ------------------------------------------------------------
        public void AddBindingParameters(ServiceEndpoint endpoint,
                                         BindingParameterCollection bindingParameters)
        {
            Console.WriteLine("  [LoggingEndpointBehavior] AddBindingParameters() ─ nothing to inject.");
        }

        // ------------------------------------------------------------
        // 3. ApplyDispatchBehavior  ← SERVER SIDE
        //    Called when this behavior is attached to a SERVICE endpoint.
        //    We wire in the server-side message inspector here.
        // ------------------------------------------------------------
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint,
                                          EndpointDispatcher endpointDispatcher)
        {
            Console.WriteLine("  [LoggingEndpointBehavior] ApplyDispatchBehavior() ─ SERVER side.");

            var inspector = new LoggingMessageInspector("SERVER");
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);

            Console.WriteLine("  [LoggingEndpointBehavior] Server LoggingMessageInspector attached.");
        }

        // ------------------------------------------------------------
        // 4. ApplyClientBehavior  ← CLIENT SIDE  (unique to IEndpointBehavior!)
        //    Called when this behavior is attached to a CLIENT ChannelFactory.
        //    This method does NOT exist on IServiceBehavior.
        //    We wire in the client-side message inspector here.
        // ------------------------------------------------------------
        public void ApplyClientBehavior(ServiceEndpoint endpoint,
                                         ClientRuntime clientRuntime)
        {
            Console.WriteLine("  [LoggingEndpointBehavior] ApplyClientBehavior() ─ CLIENT side.");

            var inspector = new LoggingMessageInspector("CLIENT");
            clientRuntime.ClientMessageInspectors.Add(inspector);

            Console.WriteLine("  [LoggingEndpointBehavior] Client LoggingMessageInspector attached.");
        }
    }
}

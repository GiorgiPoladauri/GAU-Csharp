// =============================================================
//  FILE: Program.cs
//
//  Entry point — runs both demos back-to-back:
//    DEMO 1 — IServiceBehavior  (port 8080)
//    DEMO 2 — IEndpointBehavior (port 9090)
//
//  Run as Administrator (needed to register HTTP listeners).
// =============================================================

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfBehaviorsDemo.Behaviors.Endpoint;
using WcfBehaviorsDemo.Behaviors.Service;
using WcfBehaviorsDemo.Contracts;
using WcfBehaviorsDemo.Services;

namespace WcfBehaviorsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "WCF Behaviors Demo — IServiceBehavior & IEndpointBehavior";
            PrintBanner();

            try
            {
                // ══════════════════════════════════════════════════════
                //  DEMO 1 — IServiceBehavior
                // ══════════════════════════════════════════════════════
                RunServiceBehaviorDemo();

                Console.WriteLine("\nPress any key to start Demo 2 — IEndpointBehavior...");
                Console.ReadKey(true);

                // ══════════════════════════════════════════════════════
                //  DEMO 2 — IEndpointBehavior
                // ══════════════════════════════════════════════════════
                RunEndpointBehaviorDemo();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[FATAL] {ex.GetType().Name}: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine("Make sure you are running as Administrator.");
            }

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey(true);
        }


        // ────────────────────────────────────────────────────────────
        //  DEMO 1 — IServiceBehavior
        //
        //  Shows:
        //   ✔ Validate()             — blocks startup if misconfigured
        //   ✔ AddBindingParameters() — extension point (empty here)
        //   ✔ ApplyDispatchBehavior()
        //       ├─ GlobalErrorHandler       — intercepts unhandled exceptions
        //       └─ LoggingInstanceProvider  — logs object creation/release
        // ────────────────────────────────────────────────────────────
        static void RunServiceBehaviorDemo()
        {
            PrintSectionHeader("DEMO 1 — IServiceBehavior", ConsoleColor.Cyan);

            var baseAddress = new Uri("http://localhost:8080/CalculatorService");

            using (var host = new ServiceHost(typeof(CalculatorService), baseAddress))
            {
                // ── ATTACH THE SERVICE BEHAVIOR ────────────────────────────
                // This single line triggers Validate → AddBindingParameters
                // → ApplyDispatchBehavior when host.Open() is called.
                host.Description.Behaviors.Add(new CustomServiceBehavior());

                // ── ADD ENDPOINT ───────────────────────────────────────────
                host.AddServiceEndpoint(
                    typeof(ICalculatorService),
                    new BasicHttpBinding(),
                    "basic");

                // ── METADATA ENDPOINT (optional — lets svcutil generate proxy) ──
                host.Description.Behaviors.Add(
                    new ServiceMetadataBehavior { HttpGetEnabled = true });

                // host.Open() calls all three IServiceBehavior methods
                host.Open();

                PrintStep("Host is open. Starting client calls...");

                // ── CLIENT ─────────────────────────────────────────────────
                var binding  = new BasicHttpBinding();
                var endpoint = new EndpointAddress("http://localhost:8080/CalculatorService/basic");

                using (var factory = new ChannelFactory<ICalculatorService>(binding, endpoint))
                {
                    var client = factory.CreateChannel();

                    // ── Call 1: Normal operation ───────────────────────────
                    PrintStep("Calling Add(10, 5)...");
                    int sum = client.Add(10, 5);
                    PrintResult($"Add(10, 5) = {sum}");

                    // ── Call 2: Triggers GlobalErrorHandler ────────────────
                    PrintStep("Calling Divide(10, 0) — this will throw...");
                    try
                    {
                        int bad = client.Divide(10, 0);
                    }
                    catch (FaultException ex)
                    {
                        // GlobalErrorHandler.ProvideFault() converted the
                        // DivideByZeroException into this FaultException
                        PrintResult($"FaultException received (expected): {ex.Message}");
                    }

                    // ── Call 3: Valid division ─────────────────────────────
                    PrintStep("Calling Divide(20, 4)...");
                    int div = client.Divide(20, 4);
                    PrintResult($"Divide(20, 4) = {div}");

                    ((IClientChannel)client).Close();
                }

                host.Close();
                PrintStep("Host closed. Demo 1 complete.");
            }
        }


        // ────────────────────────────────────────────────────────────
        //  DEMO 2 — IEndpointBehavior
        //
        //  Shows:
        //   ✔ Validate()              — endpoint-level config check
        //   ✔ AddBindingParameters()  — extension point (empty here)
        //   ✔ ApplyDispatchBehavior() — server-side message inspector
        //   ✔ ApplyClientBehavior()   — client-side message inspector
        //                               (UNIQUE to IEndpointBehavior!)
        //
        //  Two endpoints are created:
        //   • "with-logging"  — has LoggingEndpointBehavior attached
        //   • "no-logging"    — plain endpoint, no custom behavior
        //
        //  This demonstrates that IEndpointBehavior is SELECTIVE —
        //  it affects only the endpoint you attach it to.
        // ────────────────────────────────────────────────────────────
        static void RunEndpointBehaviorDemo()
        {
            PrintSectionHeader("DEMO 2 — IEndpointBehavior", ConsoleColor.Yellow);

            var baseAddress = new Uri("http://localhost:9090/OrderService");

            using (var host = new ServiceHost(typeof(OrderService), baseAddress))
            {
                // ── ENDPOINT 1: WITH logging behavior ─────────────────────
                // Only this endpoint gets the LoggingEndpointBehavior.
                ServiceEndpoint loggingEndpoint = host.AddServiceEndpoint(
                    typeof(IOrderService),
                    new BasicHttpBinding(),
                    "with-logging");

                loggingEndpoint.EndpointBehaviors.Add(new LoggingEndpointBehavior());

                Console.WriteLine("\n  Endpoint 1 (/with-logging) ─ LoggingEndpointBehavior attached.");

                // ── ENDPOINT 2: WITHOUT any custom behavior ────────────────
                // Compare console output — no inspector logs for this endpoint.
                host.AddServiceEndpoint(
                    typeof(IOrderService),
                    new BasicHttpBinding(),
                    "no-logging");

                Console.WriteLine("  Endpoint 2 (/no-logging)    ─ no custom behavior.");

                host.Description.Behaviors.Add(
                    new ServiceMetadataBehavior { HttpGetEnabled = true });

                host.Open();

                // ═══════════════════════════════════════════════════════
                //  CLIENT CALL A — endpoint WITH behavior
                //  Both server-side AND client-side inspectors will fire.
                // ═══════════════════════════════════════════════════════
                PrintStep("Calling endpoint WITH logging...");

                CallOrderEndpoint(
                    address:       "http://localhost:9090/OrderService/with-logging",
                    addBehavior:   true,
                    product:       "Mechanical Keyboard",
                    quantity:      3);

                Console.WriteLine();

                // ═══════════════════════════════════════════════════════
                //  CLIENT CALL B — endpoint WITHOUT behavior
                //  No inspector output — behavior is endpoint-scoped.
                // ═══════════════════════════════════════════════════════
                PrintStep("Calling endpoint WITHOUT logging (no inspector output expected)...");

                CallOrderEndpoint(
                    address:       "http://localhost:9090/OrderService/no-logging",
                    addBehavior:   false,
                    product:       "Wireless Mouse",
                    quantity:      1);

                host.Close();
                PrintStep("Host closed. Demo 2 complete.");
            }
        }

        // Helper: creates a client channel, optionally attaches the endpoint behavior
        static void CallOrderEndpoint(string address, bool addBehavior,
                                       string product, int quantity)
        {
            var binding  = new BasicHttpBinding();
            var endpoint = new EndpointAddress(address);

            using (var factory = new ChannelFactory<IOrderService>(binding, endpoint))
            {
                if (addBehavior)
                {
                    // ApplyClientBehavior() fires here when factory.CreateChannel() is called
                    factory.Endpoint.EndpointBehaviors.Add(new LoggingEndpointBehavior());
                }

                var client = factory.CreateChannel();
                string result = client.PlaceOrder(product, quantity);
                PrintResult($"Response: {result}");
                ((IClientChannel)client).Close();
            }
        }


        // ────────────────────────────────────────────────────────────
        //  Console formatting helpers (not WCF-related)
        // ────────────────────────────────────────────────────────────
        static void PrintBanner()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║     WCF Custom Behaviors — Presentation Demo         ║");
            Console.WriteLine("║     IServiceBehavior  &  IEndpointBehavior           ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        static void PrintSectionHeader(string title, ConsoleColor color)
        {
            Console.WriteLine();
            Console.ForegroundColor = color;
            Console.WriteLine($"┌─────────────────────────────────────────────────────┐");
            Console.WriteLine($"│  {title,-51}│");
            Console.WriteLine($"└─────────────────────────────────────────────────────┘");
            Console.ResetColor();
        }

        static void PrintStep(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("  ▶ ");
            Console.ResetColor();
            Console.WriteLine(message);
        }

        static void PrintResult(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  ✔ ");
            Console.ResetColor();
            Console.WriteLine(message);
        }
    }
}

// =============================================================
//  FILE: Services/Services.cs
//
//  Concrete implementations of our WCF service contracts.
//  These are plain C# classes — no WCF attributes needed here
//  because all the WCF decoration is on the interface (contract).
// =============================================================

using System;
using WcfBehaviorsDemo.Contracts;

namespace WcfBehaviorsDemo.Services
{
    // ------------------------------------------------------------------
    // CalculatorService — used in the IServiceBehavior demo
    //
    // Notice: NO [ServiceBehavior] attribute here on purpose.
    // Our custom behavior (CustomServiceBehavior) is added
    // PROGRAMMATICALLY to the ServiceHost in Program.cs, which
    // is the clean, code-first approach we want to demonstrate.
    // ------------------------------------------------------------------
    public class CalculatorService : ICalculatorService
    {
        public int Add(int a, int b)
        {
            Console.WriteLine($"     [CalculatorService] Add({a}, {b}) called.");
            return a + b;
        }

        public int Divide(int a, int b)
        {
            Console.WriteLine($"     [CalculatorService] Divide({a}, {b}) called.");

            // Intentional exception — demonstrates our GlobalErrorHandler
            if (b == 0)
                throw new DivideByZeroException("Division by zero is not allowed.");

            return a / b;
        }
    }

    // ------------------------------------------------------------------
    // OrderService — used in the IEndpointBehavior demo
    // ------------------------------------------------------------------
    public class OrderService : IOrderService
    {
        public string PlaceOrder(string product, int quantity)
        {
            Console.WriteLine($"     [OrderService] PlaceOrder({product}, {quantity}) called.");
            return $"ORDER-{DateTime.Now:yyyyMMddHHmmss} — {quantity}x {product} confirmed.";
        }
    }
}

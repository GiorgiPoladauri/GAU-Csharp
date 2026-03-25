// =============================================================
//  FILE: Contracts/IContracts.cs
//
//  This file holds all [ServiceContract] definitions.
//  In real projects, contracts live in a shared "Contracts" DLL
//  so both the server and client reference the same interface.
// =============================================================

using System.ServiceModel;

namespace WcfBehaviorsDemo.Contracts
{
    // ------------------------------------------------------------------
    // ICalculatorService — used by the IServiceBehavior demo
    //
    // [ServiceContract]  marks this interface as a WCF service contract.
    // [OperationContract] marks each method the client can call (SOAP operation).
    // ------------------------------------------------------------------
    [ServiceContract(Namespace = "http://wcfdemo.behaviors/calculator")]
    public interface ICalculatorService
    {
        [OperationContract]
        int Add(int a, int b);

        // Divide is intentionally risky — we'll trigger an error
        // to demonstrate the GlobalErrorHandler from IServiceBehavior
        [OperationContract]
        int Divide(int a, int b);
    }

    // ------------------------------------------------------------------
    // IOrderService — used by the IEndpointBehavior demo
    // ------------------------------------------------------------------
    [ServiceContract(Namespace = "http://wcfdemo.behaviors/orders")]
    public interface IOrderService
    {
        [OperationContract]
        string PlaceOrder(string product, int quantity);
    }
}

# WCF Behaviors Demo
## IServiceBehavior & IEndpointBehavior — Presentation Project

---

## Project Structure

```
WcfBehaviorsDemo/
│
├── Contracts/
│   └── IContracts.cs              ← Service contracts ([ServiceContract])
│
├── Services/
│   └── Services.cs                ← Service implementations
│
├── Behaviors/
│   ├── Service/
│   │   └── CustomServiceBehavior.cs   ← IServiceBehavior + helpers
│   └── Endpoint/
│       └── LoggingEndpointBehavior.cs ← IEndpointBehavior + message inspector
│
├── Program.cs                     ← Entry point, runs both demos
├── App.config                     ← Framework targeting + optional tracing
├── WcfBehaviorsDemo.csproj        ← Project file
└── README.md                      ← This file
```

---

## How to Open & Run

### Requirements
- Visual Studio 2019 or 2022
- .NET Framework 4.7.2 (included with VS)
- No NuGet packages needed — `System.ServiceModel` is built-in

### Steps
1. Open `WcfBehaviorsDemo.csproj` in Visual Studio
2. Right-click the project → **Properties** → confirm Target Framework = **4.7.2**
3. Right-click Visual Studio in the taskbar → **Run as Administrator**
   *(Required to register HTTP listeners on localhost)*
4. Press **F5** to run

---

## What You Will See

### Demo 1 — IServiceBehavior (port 8080)
```
[CustomServiceBehavior] Validate()            ← config check
[CustomServiceBehavior] AddBindingParameters()
[CustomServiceBehavior] ApplyDispatchBehavior() ← wires components

[LoggingInstanceProvider] Created → CalculatorService
[CalculatorService] Add(10, 5) called.
  ✔ Add(10, 5) = 15

[GlobalErrorHandler] ProvideFault  ← catches Divide(10,0)
[GlobalErrorHandler] HandleError   ← logs it, keeps channel alive
  ✔ FaultException received (expected): Server error: Division by zero...

[LoggingInstanceProvider] Released → CalculatorService
```

### Demo 2 — IEndpointBehavior (port 9090)
```
Endpoint 1 (/with-logging) — LoggingEndpointBehavior attached
Endpoint 2 (/no-logging)   — no custom behavior

[CLIENT Inspector] Sending Request...        ← ApplyClientBehavior fired
[CLIENT Inspector] Injected header X-Client-ID
[SERVER Inspector] Incoming Request...       ← ApplyDispatchBehavior fired
[SERVER Inspector] Header X-Client-ID = DemoClient-MACHINE
  ✔ Response: ORDER-... 3x Mechanical Keyboard confirmed.
[CLIENT Inspector] Round-trip time: X ms

(calling /no-logging — no inspector output, behavior is selective)
  ✔ Response: ORDER-... 1x Wireless Mouse confirmed.
```

---

## Key Concepts to Explain

| | IServiceBehavior | IEndpointBehavior |
|---|---|---|
| **Scope** | Entire service host | Single endpoint |
| **Server hook** | `ApplyDispatchBehavior` | `ApplyDispatchBehavior` |
| **Client hook** | ✗ Not available | ✔ `ApplyClientBehavior` |
| **Typical use** | Error handling, instance mgmt | Message logging, header injection |
| **Attach to** | `host.Description.Behaviors` | `endpoint.EndpointBehaviors` |

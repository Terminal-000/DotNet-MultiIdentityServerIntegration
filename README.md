# DotNet — Using Multiple IdentityServers

This project demonstrates how to integrate and consume multiple **IdentityServer** instances within a single **.NET application**.  
In many enterprise systems, different APIs or services may rely on separate authentication authorities — for example, in banking applications, some microservices handle user-facing operations that rely on customer account privileges, while other internal services operate under a separate security or authorization system.

The goal of this demo is to show a **clean and maintainable** approach to handling **multiple authentication schemes** side by side.  
It includes configuration examples for registering multiple OpenID Connect handlers, isolating authentication flows per endpoint, and managing tokens for each authority independently.

---

### > Key Aspects Covered

- Configuring multiple authentication schemes in **ASP.NET Core**  
- Mapping each endpoint or controller to a specific **IdentityServer**  
- Handling token validation and middleware setup for different issuers  
- Maintaining a **modular and scalable** authentication structure  

---

### > Purpose

This repository is designed as a **learning and reference project** for developers building complex authentication setups across microservices or multi-tenant architectures.  
The solution follows **clean architecture principles**, keeping authentication logic isolated, extensible, and ready to adapt for real production use cases involving multiple authorization providers.

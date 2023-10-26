# Replace Identity Server with .NET 8 Identity Endpoints

- Status: accepted
- Deciders: Daniel Mackay, Matt Goldman
- Date: 2023-10-26
- Tags: Identity

Technical Story: https://github.com/SSWConsulting/Northwind365/issues/76

## Context and Problem Statement

Previously, Northwind 365 used Identity Server to secure Northwind 365 via OIDC.  This was overly complex for a system that only needs to secure an API.  The authentication system will not be used as an IDP for any other systems.

## Decision Drivers <!-- optional -->

- Simplify the Authentication mechanism
- APIs need to be protected
- Users need to be register and login

## Considered Options

1. Upgrade to Duende v6
2. Use ASP.NET Identity Endpoints

## Decision Outcome

Chosen option: "Option 2 - Use ASP.NET Identity Endpoints", because it is the simplest solution that solves our main use case which is securing an API.

## Pros and Cons of the Options 

### Option 1 - Upgrade to Duende v6

- ✅ Uses ASP.NET Identity for storage
- ✅ Uses ASP.NET Identity for UI
- ❌ OIDC is overkill for the solution
- ❌ No need to authenticate other applications
- ❌ Complicated callbacks need to be managed the FE
- ❌ Possible licensing options
- ❌ Adds complexity to integration tests

### Option 2 - Use ASP.NET Identity Endpoints

- ✅ Simpler solution
- ✅ Can secure APIs
- ✅ Uses ASP.NET Identity for storage
- ✅ Simplifies integration tests (no special setup needed)
- ❌ Need to create a custom UI

## Links

- [improvements-auth-identity-aspnetcore-8](https://devblogs.microsoft.com/dotnet/improvements-auth-identity-aspnetcore-8/)
- [exploring-the-dotnet-8-preview-introducing-the-identity-api-endpoints](https://andrewlock.net/exploring-the-dotnet-8-preview-introducing-the-identity-api-endpoints/)

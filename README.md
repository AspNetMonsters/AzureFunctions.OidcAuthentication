# AzureFunctions.OidcAuthentication
[![Build status](https://github.com/AspNetMonsters/AzureFunctions.OidcAuthentication/workflows/Continuous%20Integration/badge.svg)](https://github.com/AspNetMonsters/AzureFunctions.OidcAuthentication/actions?query=workflow%3A%22Continuous+Integration%22) [![NuGet Badge](https://buildstats.info/nuget/AzureFunctions.OidcAuthentication)](https://www.nuget.org/packages/AzureFunctions.OidcAuthentication/)

> This project is originally forked from https://github.com/bryanknox/AzureFunctionsOpenIDConnectAuthSample. Thank you to Bryan for the helpful sample.

## Why?
As of writing this, securing Azure Functions using Bearer token is clumsy. For some auth providers, you can enable App Service Authentication in the Azure Portal but that only works for the deployed version of your app which makes testing locally difficult and clumsy.

This library makes it easy to authenticate a user by validating a bearer token.

## Requirements

Azure Functions v3 
Dependency Injection using Azure Functions Extensions 
An identity provider (e.g. Auth0, Azure AD, Okta)

## How to use it

First, configure dependency injection for your Azure Functions project. Start by adding the Microsoft.Azure.Functions.Exentions NuGet package.

> dotnet add package Microsoft.Azure.Functions.Extensions --version 1.1.0

Add the OidcAuthentication NuGet package to your Azure Functions project.

> dotnet package install AzureFunctions.OidcAuthentication

Now add a `FunctionStartup` class to configure services that will be used in your Azure Functions app. In the `Configure` method, add a call to `builder.Services.AddOidcApiAuthorization();` This will configure the `IApiAuthentication` service that you will use to authenticate users.

[assembly: FunctionsStartup(typeof(Curbsy.API.Startup.DependencyInjection))]

```
namespace MySecuredApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOidcApiAuthorization();
        }
    }
}
```

Configuration is loaded from a environment variables which can be set in `local.settings.json` for local development or in the Azure portal for your deployed app. Settings are prefixed with `OidcApiAuthSettings:`.

Here is an example `local.settings.json` file for Azure AD B2C:

```
{
    "IsEncrypted": false,
    "Values": {
      "AzureWebJobsStorage": "UseDevelopmentStorage=true",
      "FUNCTIONS_WORKER_RUNTIME": "dotnet",
      "OidcApiAuthSettings:Audience": "Your Application's (client) ID",
      "OidcApiAuthSettings:MetadataAddress": "https://yourb2cdomain.b2clogin.com/yourb2cdomain.onmicrosoft.com/yoursigninuserflowname/v2.0/.well-known/openid-configuration/",
      "OidcApiAuthSettings:IssuerUrl": "https://yourb2cdomain.b2clogin.com/Your Directory (tenant) ID/v2.0/"
    }
}
```

### Settings 

**OidcApiAuthSettings:Audience** - Required
Identifies the API to be authorized by the Open ID Connect provider (issuer).

The "Audience" is the identifer used by the authorization provider to identify the API (HTTP triggered Azure Function) being protected. This is often a URL but it is not used as a URL is is simply used as an identifier.

For Auth0 use the API's Identifier in the Auth0 Dashboard.

For Azure AD B2C, use your Application's (client) ID.

**OidcApiAuthSettings:IssuerUrl** - Required
The URL of the Open ID Connect provider (issuer) that will perform API authorization.

The "Issuer" is the URL for the authorization provider's end-point. This URL will be used as part of the OpenID Connect protocol to obtain the the signing keys that will be used to validate the JWT Bearer tokens in incoming HTTP request headers.

For Auth0 the URL format is:  `https://{Auth0-tenant-domain}.auth0.com`
For Auzre AD B2C, the format is: `https://yourb2cdomain.b2clogin.com/Your Directory (tenant) ID/v2.0/`

**OidcApiAuthSettings:MetadataAddress** - Optional (depending on identity provider)
The URL for the identity provider's well-known openid-configuration url.

Default Vaule: `$"{IssuerUrl}.well-known/openid-configuration"`

For Auth0, leave this blank.
For Azure AD B2C, use `https://yourb2cdomain.b2clogin.com/yourb2cdomain.onmicrosoft.com/yoursigninuserflowname/v2.0/.well-known/openid-configuration/`

**OidcApiAuthSettings:NameClaimType** - Optional
(Optional) A string defining the name of the claim that will identify the user's name

Default value: `http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier`

**OidcApiAuthSettings:RoleClaimType** - Optional
(Optional) A string defining the name of the claim that will identify the user's role membership

Default value: "http://schemas.microsoft.com/ws/2008/06/identity/claims/roleidentifier"




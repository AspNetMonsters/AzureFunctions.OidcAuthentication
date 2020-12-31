# AzureFunctions.OidcAuthentication

> This project is originally forked from https://github.com/bryanknox/AzureFunctionsOpenIDConnectAuthSample. Thank you to Bryan for the helpful sample.

## Why?
As of writing this, securing Azure Functions using Bearer token is clumsy. For some auth providers, you can enable App Service Authentication in the Azure Portal but that only works for the deployed version of your app which makes testing locally difficult and clumsy.

This library makes it easy to authenticate a user by validating a bearer token.

## How to use it

First, add the NuGet package to your Azure Functions project

> dotnet package install AzureFunctions.OidcAuthentication

Next, provide configuration values for your application. Here are some samples from popular auth providers:


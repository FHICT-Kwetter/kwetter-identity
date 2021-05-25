# Identity Service


[![SonarCloud](https://sonarcloud.io/images/project_badges/sonarcloud-orange.svg)](https://sonarcloud.io/dashboard?id=FHICT-Kwetter_kwetter-identity)

![build](https://github.com/FHICT-Kwetter/kwetter-identity/workflows/pipeline/badge.svg)
![Coverage](https://sonarcloud.io/api/project_badges/measure?project=FHICT-Kwetter_kwetter-identity&metric=coverage)
![Maintainability](https://sonarcloud.io/api/project_badges/measure?project=FHICT-Kwetter_kwetter-identity&metric=sqale_rating)
![Reliability](https://sonarcloud.io/api/project_badges/measure?project=FHICT-Kwetter_kwetter-identity&metric=reliability_rating)
![Security](https://sonarcloud.io/api/project_badges/measure?project=FHICT-Kwetter_kwetter-identity&metric=security_rating)
![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=FHICT-Kwetter_kwetter-identity&metric=vulnerabilities)
![Bugs](https://sonarcloud.io/api/project_badges/measure?project=FHICT-Kwetter_kwetter-identity&metric=bugs)


## Overview

The Identity Service is an OAuth2 + OIDC provider. This project contains the source code for the Identity Provider (With IdentityServer4) and User Management with (Asp.NET Core Identity).


### OIDC + OAuth2

This project uses an implementation for the OIDC and OAuth2 standards.
This is done by using the [IdentityServer4](https://identityserver4.readthedocs.io/en/latest/) library.


### User Management

User Management is implemented by extending the [ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-5.0&tabs=visual-studio) model.


## Technologies

This project is created with:

- Language: C# 9.0
- Framework: ASP.NET Core 5.0

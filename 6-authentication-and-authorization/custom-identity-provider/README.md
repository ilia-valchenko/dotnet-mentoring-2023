# Custom Identity Provider
This ASP.NET app acts as an OpenID Provider and OAuth authorization server. This is a working IdentityServer implementation. IdentityServer takes care of the protocol support, but user authentication is up to you, the developer.

## Disco doc
Check out the OpenID Connect discovery document (affectionately known as the “disco doc”) at:
https://localhost:7193/.well-known/openid-configuration

This document contains metadata information such as the:
 - location of various endpoints (for example, the authorization endpoint and the token endpoint);
 - location of it’s public keys (a JSON Web Key Set (JWKS));
 - grant types the provider supports;
 - scopes it can authorize.

## IdentityServer Signing Credentials
Signing credentials are private keys used to sign tokens. IdentityServer uses the **private key** to create signatures, while other applications use the corresponding **public key** to verify the signature. These public keys are accessible to client applications via the **jwks_uri in** the OpenID Connect discovery document. IdentityServer is only interested in the private key. 

## Private and public keys
We usually use RSA keys. We can use a tool such as OpenSSL.

## Store of client applications
We need to have a store of client applications that are allowed to use IdentityServer, as well as the protected resources that those clients can use, and the users that can authenticate in our system. In this application we use the in-memory stores.

## IdentityServer clients
IdentityServer needs to know what client applications are allowed to use it.
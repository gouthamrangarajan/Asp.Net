## azure ad authentication and call a protected web api using Microsoft Identity library

https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-web-app-call-api-overview

### Please change the following form appsettings.json

```json
"AzureAd": {
"Instance": "https://login.microsoftonline.com/",
"Domain": "Domain",
"ClientId": "ClientId",
"TenantId": "TenanId",
"CallbackPath": "/signin-oidc",
"ClientSecret": "Secret"
},
"WebApi": {
"BaseUrl": "http://localhost:5002",
"Scopes": "Scopes"
},
"Redis": {
"Url": "REDIS_URL_WITH_PORT",
"Password": "REDIS_PASSWORD",
"Instance": "REDIS_INSTANCE"
}
```

#### please change the following in startup.cs for InMemoryCache instead of redis

```C#
services.AddStackExchangeRedisCache(options=>{
    options.InstanceName=Configuration.GetValue<string>("Redis:Instance");
    options.Configuration=$"{Configuration.GetValue<string>("Redis:Url")},password={Configuration.GetValue<string>("Redis:Password")}";
});
services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
.AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi(new string[]{ Configuration.GetSection("WebApi").GetValue<string>("Scopes")})
                .AddDownstreamWebApi("WebApi", Configuration.GetSection("WebApi"))
.AddDistributedTokenCaches();
```

to

```C#
services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
.AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi(new string[]{ Configuration.GetSection("WebApi").GetValue<string>("Scopes")})
        .AddDownstreamWebApi("WebApi", Configuration.GetSection("WebApi"))
.AddInMemoryTokenCaches();
```

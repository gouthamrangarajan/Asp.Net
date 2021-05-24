## azure ad authentication and call a protected web api using Microsoft Identity library

https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-web-app-call-api-overview

### Please change the following form appsettings.json

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
}

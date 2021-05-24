## azure ad authentication using Microsoft Identity library

https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-protected-web-api-overview

### Please change the following form appsettings.json

"AzureAd": {
"Instance": "https://login.microsoftonline.com/",
"Domain": "Domain",
"ClientId": "ClientId",
"TenantId": "TenanId"
}

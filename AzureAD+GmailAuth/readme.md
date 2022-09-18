## .Net Core Azure Ad(Microsoft) & Gmail authentication(mutiple) for web app

### Code Changes Highlights

1. Startup.cs

```C#
services.AddIdentity<IdentityUser,IdentityRole>().AddUserStore<UserStore>().AddRoleStore<RoleStore>()
                    .AddDefaultTokenProviders();

services.AddAuthentication().AddMicrosoftAccount(microsoftOptions=>{
    microsoftOptions.ClientId = Configuration["MicrosoftAuth:ClientId"];
    microsoftOptions.ClientSecret = Configuration["MicrosoftAuth:ClientSecret"];
    microsoftOptions.SaveTokens=true;
})
.AddGoogle(googleOptions=>{
    googleOptions.ClientId = Configuration["GoogleAuth:ClientId"];
    googleOptions.ClientSecret = Configuration["GoogleAuth:ClientSecret"];
    googleOptions.SaveTokens=true;

});
```

2. Index.cshtml (UI where External Provider selection occurs)

```html
<form class="flex flex-col space-y-2" method="post">
  <button
    name="provider"
    type="submit"
    class="appearance-non outline-none py-2 px-4 rounded bg-red-600 text-white transition 
                duration-300 focus:ring-2 focus:ring-red-600
                focus:ring-offset-2 focus:ring-offset-red-50 w-48"
    value="Google"
  >
    Login With Google
  </button>
  <button
    name="provider"
    type="submit"
    class="appearance-non outline-none py-2 px-4 rounded bg-blue-600 text-white transition
                 duration-300 focus:ring-2 focus:ring-blue-600
                focus:ring-offset-2 focus:ring-offset-blue-50 w-48"
    value="Microsoft"
  >
    Login With Microsoft
  </button>
</form>
```

3. Index.cshtml.cs (page code where authentication happens)

```C#
public async Task<IActionResult> OnPost(string provider)
{
    var  data=await _signInManager.GetExternalAuthenticationSchemesAsync();
    var redirectUrl = Url.Page("./Index", pageHandler: "Callback");
    var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
    return new ChallengeResult(provider, properties);
}
public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
{
    var info = await _signInManager.GetExternalLoginInfoAsync();
    if (info == null)
    {
        _logger.LogWarning("Call back from External Provider: Information is empty");
        return RedirectToPage("./");
    }
    _logger.LogInformation("Call back from External Provider: Received Claims");

    await _signInManager.SignInWithClaimsAsync(new IdentityUser{
        Email=info.Principal.Claims.First(f=>f.Type==System.Security.Claims.ClaimTypes.Email).Value,
        Id=info.Principal.Claims.First(f=>f.Type==System.Security.Claims.ClaimTypes.NameIdentifier).Value,
        UserName=info.Principal.Claims.First(f=>f.Type==System.Security.Claims.ClaimTypes.Email).Value
    },true,info.Principal.Claims);
    return RedirectToPage("./Auth");
}
```

4. Please change the following form appsettings.json

```json
"MicrosoftAuth": {
    "ClientId": "CLIENT_ID",
    "ClientSecret": "CLIENT_SECRET"
  },
  "GoogleAuth": {
    "ClientId": "CLIENT_ID",
    "ClientSecret": "CLIENT_SECRET"
  }
```

5. Please check the official document links and repo samples in the bottom after screenshots

### Screenshots

![Screenshot](https://github.com/gouthamrangarajan/Asp.Net/blob/master/AzureAD%2BGmailAuth/Screenshot1.png)
![Screenshot](https://github.com/gouthamrangarajan/Asp.Net/blob/master/AzureAD%2BGmailAuth/Screenshot2.png)
![Screenshot](https://github.com/gouthamrangarajan/Asp.Net/blob/master/AzureAD%2BGmailAuth/Screenshot3.png)

#### Few documentation I glanced to achieve this

- [Client Id, Client Secret & Microsoft Login Code Changes](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/microsoft-logins?view=aspnetcore-6.0)

- [Client Id, Client Secret & Google Login Code Changes](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-6.0)

- [Code to request External Provider Authorization Url based on selected Extenal provider by user](https://github.com/dotnet/aspnetcore/blob/main/src/Security/samples/Identity.ExternalClaims/Pages/Account/ExternalLogin.cshtml.cs)

- [Custom Store example](https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/security/authentication/identity-custom-storage-providers/sample/CustomIdentityProviderSample/CustomProvider/CustomUserStore.cs)

- [https://github.com/dotnet/aspnetcore/blob/main/src/Security/samples/Identity.ExternalClaims/Startup.cs](https://github.com/dotnet/aspnetcore/blob/main/src/Security/samples/Identity.ExternalClaims/Startup.cs)

- [https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/security/authentication/identity-custom-storage-providers/sample/CustomIdentityProviderSample/Startup.cs](https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/security/authentication/identity-custom-storage-providers/sample/CustomIdentityProviderSample/Startup.cs)

- [https://github.com/dotnet/aspnetcore/tree/main/src/Security/samples](https://github.com/dotnet/aspnetcore/tree/main/src/Security/samples)

using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TicketingSystemFrontend.Client.Auth;
using TicketingSystemFrontend.Client.Services;
using TicketingSystemFrontend.Client.Services.Auth;
using TicketingSystemFrontend.Client.Services.Interfaces;
using TicketingSystemFrontend.Client.Services.Interfaces.Auth;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomAuthProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthProvider>());



builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]!)
});
builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddScoped<ITokenStorage, LocalStorageTokenStorage>();
builder.Services.AddScoped<ITokenStorage, CookieTokenStorage>();


//builder.Services.AddHttpClient<ITicketService, TicketService>(client =>
//{
//    client.BaseAddress = new Uri("https://your-api-base-url.com/");
//});



await builder.Build().RunAsync();

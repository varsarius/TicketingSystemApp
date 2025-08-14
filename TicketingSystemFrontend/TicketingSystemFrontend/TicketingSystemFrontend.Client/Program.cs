using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TicketingSystemFrontend.Client.Auth;
using TicketingSystemFrontend.Client.Services;
using TicketingSystemFrontend.Client.Services.Auth;
using TicketingSystemFrontend.Client.Services.Interfaces;
using TicketingSystemFrontend.Client.Services.Interfaces.Auth;
using Microsoft.Extensions.DependencyInjection;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomAuthProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthProvider>());



builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IArticleCategoryService, ArticleCategoryService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]!)
});

builder.Services.AddScoped<AuthTokenHandler>();


builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]!);
})
.AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddScoped(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("ApiClient");
});


builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddScoped<ITokenStorage, LocalStorageTokenStorage>();
builder.Services.AddScoped<ITokenStorage, CookieTokenStorage>();
builder.Services.AddScoped<IUserService, UserService>();


//builder.Services.AddHttpClient<ITicketService, TicketService>(client =>
//{
//    client.BaseAddress = new Uri("https://your-api-base-url.com/");
//});



await builder.Build().RunAsync();

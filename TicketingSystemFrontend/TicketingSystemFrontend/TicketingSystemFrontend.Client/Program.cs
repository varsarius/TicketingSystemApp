using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using TicketingSystemFrontend.Client.Auth;
using TicketingSystemFrontend.Client.Services;
using TicketingSystemFrontend.Client.Services.Auth;
using TicketingSystemFrontend.Client.Services.Interfaces;
using TicketingSystemFrontend.Client.Services.Interfaces.Auth;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomAuthProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthProvider>());


builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IArticleCategoryService, ArticleCategoryService>();

builder.Services.AddScoped<ITicketCategoryService, TicketCategoryService>();
builder.Services.AddScoped<ITicketCommentService, TicketCommentService>();



builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddScoped(sp =>
{
    var client = new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    };
    // Don't set UseCookies, the browser handles cookies
    return client;
});


builder.Services.AddScoped<AuthTokenHandler>();


builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]!);
});
// var client = httpClientFactory.CreateClient("ApiClient");

// var client = httpClientFactory.CreateClient("ApiClient");



builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddScoped<ITokenStorage, LocalStorageTokenStorage>();
builder.Services.AddScoped<ITokenStorage, CookieTokenStorage>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITokenRefresher, TokenRefresher>();


builder.Services.AddMudServices();

//builder.Services.AddHttpClient<ITicketService, TicketService>(client =>
//{
//    client.BaseAddress = new Uri("https://your-api-base-url.com/");
//});



await builder.Build().RunAsync();

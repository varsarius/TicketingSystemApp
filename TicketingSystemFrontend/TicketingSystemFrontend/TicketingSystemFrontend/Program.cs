using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using TicketingSystemFrontend.Client.Auth;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Pages;
using TicketingSystemFrontend.Client.Requests.Auth;
using TicketingSystemFrontend.Client.Services;
using TicketingSystemFrontend.Client.Services.Auth;
using TicketingSystemFrontend.Client.Services.Interfaces;
using TicketingSystemFrontend.Client.Services.Interfaces.Auth;
using TicketingSystemFrontend.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims; // <-- needed for [FromServices]


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

//builder.Services.AddAuthorizationCore(); // It works without this line. May be soon will be deleted. The line below needs for <Auth> tags

builder.Services.AddAuthentication("JwtBearer")
    .AddJwtBearer("JwtBearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7299",         // must match token
            ValidAudience = "https://localhost:7299",     // must match token
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("YourSuperSecretKey123456789012345254534534524412432")) // same key your API uses
        };

        // Tell the middleware to pull token from cookie
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = ctx =>
            {
                if (ctx.Request.Cookies.TryGetValue("access_token", out var token))
                {
                    ctx.Token = token;
                }
                return Task.CompletedTask;
            }
        };
    });




//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient();


builder.Services.AddScoped<CustomAuthProvider>();
//builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthProvider>());
//builder.Services.AddSingleton<IAuthorizationPolicyProvider, DefaultAuthorizationPolicyProvider>();

builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
builder.Services.AddScoped<ITicketCategoryService, TicketCategoryService>();
builder.Services.AddScoped<ITicketCommentService, TicketCommentService>();



builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]!)
});
builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddScoped<ITokenStorage, LocalStorageTokenStorage>();
builder.Services.AddScoped<ITokenStorage, CookieTokenStorage>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFileService, FileService>();
//builder.Services.AddScoped<ITokenRefresher, TokenRefresher>();

builder.Services.AddMudServices();

builder.Services.AddAuthorization();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(TicketingSystemFrontend.Client._Imports).Assembly);

app.MapPost("/server-login", async (LoginRequest login, HttpResponse response, [FromServices] IHttpClientFactory httpClientFactory) =>
{
    Console.WriteLine($"Login attempt with email: {login.Email}");
    if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
    {
        Console.WriteLine("Login failed: Email or password is empty");
        return Results.BadRequest("Email and password are required.");
    }

    Console.WriteLine($"Login attempt with email: {login.Email} and password: {login.Password}");

    var client = httpClientFactory.CreateClient();

    Console.WriteLine("Sending login request to API...");

    // Call your real API
    var apiResponse = await client.PostAsJsonAsync("https://localhost:7299/api/auth/login", login);

    Console.WriteLine($"API response status: {apiResponse.StatusCode}");

    if (!apiResponse.IsSuccessStatusCode)
    {
        Console.WriteLine($"API login failed with status: {apiResponse.StatusCode}");
        var errorText = await apiResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"API login failed: {apiResponse.StatusCode} - {errorText}");
        return Results.Unauthorized();
        Console.WriteLine("Login failed: " + errorText);
    }
    Console.WriteLine("Login successful, reading tokens...");
    var tokens = await apiResponse.Content.ReadFromJsonAsync<LoginResult>();

    Console.WriteLine("Tokens received, setting cookies...");
    // Set HttpOnly cookies
    response.Cookies.Append("access_token", tokens.AccessToken, new CookieOptions
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.None,
        Expires = DateTimeOffset.UtcNow.AddMinutes(30)
    });

    Console.WriteLine("Access token cookie set");
    response.Cookies.Append("refresh_token", tokens.RefreshToken, new CookieOptions
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.None,
        Expires = DateTimeOffset.UtcNow.AddDays(30)
    });

    Console.WriteLine("Refresh token cookie set");
    return Results.Ok();
}).AllowAnonymous();


app.MapGet("/server-auth-state", (HttpContext ctx) =>
{
    Console.WriteLine("Received request to /server-auth-state");
    if (!ctx.User.Identity?.IsAuthenticated ?? true)
        return Results.Unauthorized();

    Console.WriteLine("Checking authentication state...");
    if (!ctx.User.Identity?.IsAuthenticated ?? true)
    {
        Console.WriteLine("User is not authenticated");
        return Results.Ok(null);
    }

    Console.WriteLine("User is authenticated");

    // Extract claims safely
    var userId = ctx.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var email = ctx.User.FindFirst(ClaimTypes.Email)?.Value;
    var name = ctx.User.FindFirst("name")?.Value;


    Console.WriteLine($"User ID from claims: {userId}");
    Console.WriteLine($"User email from claims: {email}");
    Console.WriteLine($"User name from claims: {name}");

    // Return all claims that Blazor uses
    return Results.Ok(new
    {
        Id = userId,
        Email = email,
        Name = name
    });
});


app.MapPost("/server-register", async (RegisterRequest register, HttpResponse response, [FromServices] IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient();
    var apiResponse = await client.PostAsJsonAsync("https://localhost:7299/api/auth/register", register);

    if (!apiResponse.IsSuccessStatusCode)
        return Results.BadRequest(await apiResponse.Content.ReadAsStringAsync());

    return Results.Ok();
});



app.Run();

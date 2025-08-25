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
using System.Security.Claims;
using TicketingSystemFrontend.Client.Requests; // <-- needed for [FromServices]


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

app.MapPost("/server-logout", (HttpResponse response) =>
{
    response.Cookies.Delete("access_token");
    response.Cookies.Delete("refresh_token");
    return Results.Ok();
});

// In your Program.cs or a separate minimal API file

app.MapGet("/server-articles/all", async ([FromServices] IHttpClientFactory httpClientFactory) =>
{
    Console.WriteLine("Fetching all articles from the server...");
    var client = httpClientFactory.CreateClient();
    Console.WriteLine("Created HTTP client for articles");
    var response = await client.GetAsync("https://localhost:7299/api/articles");
    Console.WriteLine($"Received response with status code: {response.StatusCode}");
    response.EnsureSuccessStatusCode();
    Console.WriteLine("Response is successful, reading articles...");
    var articles = await response.Content.ReadFromJsonAsync<List<ArticleDto>>();
    Console.WriteLine($"Fetched {articles?.Count ?? 0} articles");
    return Results.Ok(articles);
});

app.MapGet("/server-articles/get/{id:int}", async (int id, [FromServices] IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient();
    var response = await client.GetAsync($"https://localhost:7299/api/articles/{id}");
    if (!response.IsSuccessStatusCode) return Results.NotFound();
    var article = await response.Content.ReadFromJsonAsync<ArticleDto>();
    return Results.Ok(article);
});

app.MapPost("/server-articles/create", async (ArticleCreateRequest request, HttpResponse response, [FromServices] IHttpClientFactory httpClientFactory, [FromServices] CustomAuthProvider authProvider) =>
{
    var authState = await authProvider.GetAuthenticationStateAsync();
    var userIdClaim = authState.User.FindFirst("sub");
    if (userIdClaim == null) return Results.Unauthorized();

    request.UserId = Guid.Parse(userIdClaim.Value);

    var client = httpClientFactory.CreateClient();
    var apiResponse = await client.PostAsJsonAsync("https://localhost:7299/api/articles", request);
    apiResponse.EnsureSuccessStatusCode();

    var articleId = await apiResponse.Content.ReadFromJsonAsync<int>();
    return Results.Ok(articleId);
});

app.MapPut("/server-articles/update/{id:int}", async (int id, ArticleUpdateRequest request, [FromServices] IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient();
    var apiResponse = await client.PutAsJsonAsync($"https://localhost:7299/api/articles/{id}", request);
    if (!apiResponse.IsSuccessStatusCode) return Results.BadRequest();
    return Results.Ok();
});

app.MapDelete("/server-articles/delete/{id:int}", async (int id, [FromServices] IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient();
    var apiResponse = await client.DeleteAsync($"https://localhost:7299/api/articles/{id}");
    if (!apiResponse.IsSuccessStatusCode) return Results.BadRequest();
    return Results.Ok();
});



app.Run();

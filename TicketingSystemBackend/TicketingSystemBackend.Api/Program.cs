using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Services.Auth;
using TicketingSystemBackend.Infrastructure.Data;
using TicketingSystemBackend.Infrastructure.Repositories;
using TicketingSystemBackend.Infrastructure.Seeds;
using TicketingSystemBackend.Infrastructure.Services.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7044", "http://localhost:5273")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//WARNING: IgnoreCycles adds additional complexity(is it?) and unintensionally exposes other properties
builder.Services.AddControllers()//side effect: properties which cause cycles now contain "null" inside to break the cycle
    .AddJsonOptions(options => 
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
// Add Identity + JWT
builder.Services.AddScoped<IJwtTokenService, CustomJwtTokenService>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthenticatedUser", policy =>
        policy.RequireAuthenticatedUser());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwtOptions =>
{
    jwtOptions.Authority = builder.Configuration["Jwt:Authority"];
    jwtOptions.Audience = builder.Configuration["Jwt:Audience"];
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]
            ?? throw new Exception("Missing jwt key config.")))

    };

    jwtOptions.MapInboundClaims = false;
});


builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketCategoryRepository, TicketCategoryRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITicketCommentRepository, TicketCommentRepository>();
builder.Services.AddHostedService<RefreshTokenCleanupService>();
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(
        typeof(TicketingSystemBackend.Application.AssemblyReference).Assembly)
);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



var app = builder.Build();
app.UseRouting();
// Use the CORS policy
app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

// Seed roles & categories
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    await RoleSeeder.SeedRolesAsync(roleManager, userManager);
    await ArticleCategorySeeder.SeedAsync(context);
}

app.MapControllers();

app.Run();
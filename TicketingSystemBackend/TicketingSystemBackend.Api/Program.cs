using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Reflection;
using System.Text;
using TicketingSystemBackend.Api.Auth;
using TicketingSystemBackend.Infrastructure.Data;
using TicketingSystemBackend.Infrastructure.Repositories;
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


builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
// Add Identity + JWT
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomUserClaimsPrincipalFactory>();
builder.Services.AddScoped<CustomJwtTokenService>();
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
    //jwtOptions.MetadataAddress = builder.Configuration["Api:MetadataAddress"] ?? throw new Exception("Missing metadata");
    // Optional if the MetadataAddress is specified
    //jwtOptions.Authority = builder.Configuration["Jwt:Authority"];
    jwtOptions.Audience = builder.Configuration["Jwt:Audience"];
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidAudiences = builder.Configuration.GetSection("Jwt:Audience").Get<string[]>(),
        //ValidIssuers = builder.Configuration.GetSection("Jwt:Issuer").Get<string[]>(),                
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]
    ?? throw new Exception("Missing jwt key config.")))

    };

    jwtOptions.MapInboundClaims = false;
});
//.AddBearerToken(IdentityConstants.BearerScheme, options =>
//{
//    options.BearerTokenExpiration = TimeSpan.FromHours(1);
//});


builder.Services.AddScoped<ArticleRepository>();
builder.Services.AddScoped<TicketRepository>();
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



app.MapControllers();

app.Run();
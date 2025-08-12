using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Scalar.AspNetCore;
using System.Reflection;
using System.Text.Json.Serialization;
using TicketingSystemBackend.Application.Interfaces;
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

//WARNING: IgnoreCycles adds additional complexity(is it?) and unintensionally exposes other properties
builder.Services.AddControllers()//side effect: properties which cause cycles now contain "null" inside to break the cycle
    .AddJsonOptions(options => 
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketCategoryRepository, TicketCategoryRepository>();
builder.Services.AddScoped<ITicketCommentRepository, TicketCommentRepository>();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        typeof(TicketingSystemBackend.Application.AssemblyReference).Assembly)
);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



var app = builder.Build();

// Use the CORS policy
app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.MapControllers();

app.Run();
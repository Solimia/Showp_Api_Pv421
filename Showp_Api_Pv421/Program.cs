using BusinessLogic.Configurations;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Showp_Api_Pv421;
using Showp_Api_Pv421.Extensions;
using Showp_Api_PV421;


var builder = WebApplication.CreateBuilder(args);

string connStr = builder.Configuration.GetConnectionString("RemoteDb")
        ?? throw new Exception("No Connection String found.");
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg => { }, typeof(MapperProfile));

builder.Services.AddDbContext<ShopDbContext>(options =>
        options.UseSqlServer(connStr));

builder.Services.AddIdentity<User, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ShopDbContext>();
   //.AddRoles<IdentityRole>()

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IAccountsService, AccountsService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


builder.Services.AddSingleton(_ => builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>()!);
var jwtOpts = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>()!; 

builder.Services.AddJwtSettings(jwtOpts);
builder.Services.AddSwaggerWithJWT();


var app = builder.Build();

app.SeedRolesAndInitialAdmin();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{;
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseMiddleware<ErrorHandlerMiddleware>();
}
if (app.Environment.IsProduction()) {
    app.UseMiddleware<ErrorHandlerMiddleware>();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

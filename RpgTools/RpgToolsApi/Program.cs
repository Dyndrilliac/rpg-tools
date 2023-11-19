using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RpgToolsApi.Controllers.Dice;
using RpgToolsApi.Models;
using RpgToolsApi.Models.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite("DataSource=app.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIdentityApi<AppUser>();

DiceController.Configure(app);

app.Run();

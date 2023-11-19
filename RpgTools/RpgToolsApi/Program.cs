using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RpgToolsApi.Models;
using RpgToolsApi.Models.Auth;
using System.Security.Claims;

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

app.MapGet("/roll", (ClaimsPrincipal user, int Dice, int Sides) => $"Rolling {Dice}d{Sides} for {user.Identity!.Name}...")
    .WithOpenApi(generatedOperation =>
    {
        generatedOperation.OperationId = "RollDice";
        generatedOperation.Description = "This endpoint allows a user to roll an arbitrary amount of dice, with each die having an arbitrary number of sides.";
        generatedOperation.Parameters[0].Description = "The number of dice to roll.";
        generatedOperation.Parameters[1].Description = "The number of sides on each die.";
        return generatedOperation;
    })
    .RequireAuthorization();

app.Run();

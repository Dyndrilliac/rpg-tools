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

app.MapGet("/", (ClaimsPrincipal user) => $"Hello {user.Identity!.Name}!")
    .RequireAuthorization();

app.MapGet("/roll", (ClaimsPrincipal user, int Dice, int Sides) => $"Rolling {Dice}d{Sides}...")
    .WithName("RollDice")
    .WithOpenApi(generatedOperation =>
    {
        var numDiceParam = generatedOperation.Parameters[1];
        numDiceParam.Description = "The number of dice to roll.";

        var numSidesParam = generatedOperation.Parameters[2];
        numSidesParam.Description = "The number of sides on each die.";

        return generatedOperation;
    })
    .RequireAuthorization();

app.Run();

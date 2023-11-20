using RpgToolsApi.Models.Dice;
using System.Security.Claims;

namespace RpgToolsApi.Controllers.Dice
{
    public class DiceController
    {
        public static void Configure(WebApplication app)
        {
            RollDice(app);
        }

        public static void RollDice(WebApplication app)
        {
            app.MapGet("/roll", (ClaimsPrincipal user, uint Dice, uint Sides) =>
            {
                var playerName = user.Identity?.Name ?? string.Empty;
                return new RollResults(playerName, Dice, Sides);
            })
                .WithName("RollDice")
                .WithOpenApi(generatedOperation =>
                {
                    generatedOperation.Description = "This endpoint allows a user to roll an arbitrary amount of dice, with each die having an arbitrary number of sides.";
                    generatedOperation.Parameters[0].Description = "The number of dice to roll.";
                    generatedOperation.Parameters[1].Description = "The number of sides on each die.";
                    return generatedOperation;
                })
                .RequireAuthorization();
        }
    }
}

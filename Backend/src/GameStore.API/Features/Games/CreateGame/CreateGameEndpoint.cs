using System;
using GameStore.API.Data;
using GameStore.API.Features.Games.Constants;
using GameStore.API.Models;

namespace GameStore.API.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    public static void MapCreateGame(this IEndpointRouteBuilder? app)
    {
        app?.MapPost("/", async (
                    CreateGameDTO newGame,
                    GameStoreContext dbContext,
                    ILogger<Program> logger) =>
        {
            var game = new Game
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate,
                Description = newGame.Description
            };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            logger.LogTrace("Created game {GameName} with price {GamePrice}",
                                  game.Name, game.Price);

            return Results.CreatedAtRoute(
                    EndpointNames.GetGameEndpointName,
                    new { id = game.Id },
                    new GameDetailsDTO
                    (
                        game.Id,
                        game.Name,
                        game.GenreId,
                        game.Price,
                        game.ReleaseDate,
                        game.Description
                    ));

        }).WithParameterValidation();
    }
}

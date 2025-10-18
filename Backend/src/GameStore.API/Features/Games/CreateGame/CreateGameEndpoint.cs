using System;
using GameStore.API.Data;
using GameStore.API.Features.Games.Constants;
using GameStore.API.Models;

namespace GameStore.API.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    public static void MapCreateGame(this IEndpointRouteBuilder? app)
    {
        app?.MapPost("/", (CreateGameDTO newGame, GameStoreData data, GameDataLogger logger) =>
        {
            var genre = data.GetGenre(newGame.GenreId);

            if (genre is null)
            {
                return Results.BadRequest($"Genre with Id {newGame.GenreId} does not exist.");
            }
            var game = new Game
            {
                Name = newGame.Name,
                Genre = genre,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate,
                Description = newGame.Description
            };

            data.AddGame(game);

            logger.PrintGames();

            return Results.CreatedAtRoute(
                    EndpointNames.GetGameEndpointName,
                    new { id = game.Id },
                    new GameDetailsDTO
                    (
                        game.Id,
                        game.Name,
                        game.Genre.Id,
                        game.Price,
                        game.ReleaseDate,
                        game.Description
                    ));

        }).WithParameterValidation();
    }
}

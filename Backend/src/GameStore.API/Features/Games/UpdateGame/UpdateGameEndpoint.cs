using System;
using GameStore.API.Data;

namespace GameStore.API.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static void MapUpdateGame(this WebApplication? app, GameStoreData data)
    {
        //PUT /games/{id}
        app?.MapPut("/games/{id}", (Guid id, UpdateGameDTO updatedGame) =>
        {
            var existingGame = data.GetGame(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            var genre = data.GetGenre(updatedGame.GenreId);
            if (genre is null)
            {
                return Results.BadRequest($"Genre with Id {updatedGame.GenreId} does not exist.");
            }

            existingGame.Name = updatedGame.Name;
            existingGame.Genre = genre;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;

            return Results.NoContent();

        }).WithParameterValidation();
    }
}

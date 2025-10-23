using GameStore.Api.Data;

namespace GameStore.API.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static void MapUpdateGame(this IEndpointRouteBuilder? app)
    {
        //PUT /games/{id}
        app?.MapPut("/{id}", async (Guid id, UpdateGameDTO updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = dbContext.Games.Find(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            existingGame.Name = updatedGame.Name;
            existingGame.GenreId = updatedGame.GenreId;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;
            existingGame.Description = updatedGame.Description;

            await dbContext.SaveChangesAsync();
            
            return Results.NoContent();

        }).WithParameterValidation();
    }
}

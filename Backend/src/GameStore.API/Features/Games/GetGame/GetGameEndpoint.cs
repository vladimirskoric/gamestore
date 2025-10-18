using GameStore.API.Data;
using GameStore.API.Features.Games.Constants;

namespace GameStore.API.Features.Games.GetGame;

public static class GetGameEndpoint
{
    public static void MapGetGame(this IEndpointRouteBuilder? app)
    {
        app?.MapGet("/{id}", (Guid id, GameStoreContext dbContext) =>
        {
            var game = dbContext.Games.Find(id);
            return game is not null ? Results.Ok(new GameDetailsDTO
            (
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate,
                game.Description
            )) : Results.NotFound();
        })
        .WithName(EndpointNames.GetGameEndpointName);
    }
}

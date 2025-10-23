using GameStore.Api.Data;
using GameStore.Api.Features.Games.GetGames;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Features.Games.GetGames;

public static class GetGamesEndpoint
{
    public static void MapGetGames(this IEndpointRouteBuilder? app)
    {
        app?.MapGet("/", async (GameStoreContext dbContext) =>
        await dbContext.Games.Include(game => game.Genre)
            .Select(x => new GameSummaryDto
            (
                x.Id,
                x.Name,
                x.Genre!.Name,
                x.Price,
                x.ReleaseDate,
                x.ImageUri
            )).AsNoTracking()
              .ToListAsync());
    }
}

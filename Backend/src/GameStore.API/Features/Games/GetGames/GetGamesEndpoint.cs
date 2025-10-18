using System;
using GameStore.API.Data;

namespace GameStore.API.Features.Games.GetGames;

public static class GetGamesEndpoint
{
    public static void MapGetGames(this IEndpointRouteBuilder? app)
    {
        app?.MapGet("/", (GameStoreData data) => data.GetGames().Select(x=> new GameSummaryDTO
        (
            x.Id,
            x.Name,
            x.Genre!.Name,
            x.Price,
            x.ReleaseDate
        )));
    }
}

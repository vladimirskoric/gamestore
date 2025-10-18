using System;
using GameStore.API.Data;

namespace GameStore.API.Features.Genre.GetGenres;

public static class GetGenres
{
    public static void MapGetGenres(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapGet("/", () => data.GetGenres().Select(x => new GenreDTO
        (
            x.Id,
            x.Name
        )));
    }
}

using System;
using GameStore.API.Data;

namespace GameStore.API.Features.Genre.GetGenres;

public static class GetGenres
{
    public static void MapGetGenres(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", (GameStoreData data) => data.GetGenres().Select(x => new GenreDTO
        (
            x.Id,
            x.Name
        )));
    }
}

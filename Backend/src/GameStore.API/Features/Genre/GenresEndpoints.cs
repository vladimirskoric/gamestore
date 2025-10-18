using System;
using GameStore.API.Data;
using GameStore.API.Features.Genre.GetGenres;

namespace GameStore.API.Features.Genres;

public static class GenresEndpoints
{
    public static void MapGenres(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/genres");
        group.MapGetGenres();
    }
}

using System;
using GameStore.API.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Features.Genre.GetGenres;

public static class GetGenres
{
    public static void MapGetGenres(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Genres.Select(x => new GenreDTO
            (
                x.Id,
                x.Name
            )).AsNoTracking()
              .ToListAsync());
    }
}

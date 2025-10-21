using System;
using GameStore.API.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Features.Games.DeleteGame;

public static class DeleteGameEndpoint
{
    public static void MapDeleteGame(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (Guid id, GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
            
            return Results.NoContent();
        });
    }
}

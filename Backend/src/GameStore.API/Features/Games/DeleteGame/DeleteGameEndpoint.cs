using System;
using GameStore.API.Data;

namespace GameStore.API.Features.Games.DeleteGame;

public static class DeleteGameEndpoint
{
    public static void MapDeleteGame(this WebApplication app, GameStoreData data)
    {
        app.MapDelete("/games/{id}", (Guid id) =>
        {
            data.RemoveGame(id);
            return Results.NoContent();
        });
    }
}

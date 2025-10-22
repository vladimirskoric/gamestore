using System.Diagnostics;
using System.Xml.Linq;
using GameStore.API.Data;
using GameStore.API.Features.Games.Constants;
using GameStore.API.Models;
using Microsoft.Data.Sqlite;

namespace GameStore.API.Features.Games.GetGame;

public static class GetGameEndpoint
{
    public static void MapGetGame(this IEndpointRouteBuilder? app)
    {
        app?.MapGet("/{id}", async (Guid id, GameStoreContext dbContext, ILogger logger) =>
        {
            try
            {
                Game? game = await FindGameAsync(id, dbContext);

                return game is null ? Results.NotFound() :
                                        Results.Ok(
                                            new GameDetailsDTO
                                            (
                                                game.Id,
                                                game.Name,
                                                game.GenreId,
                                                game.Price,
                                                game.ReleaseDate,
                                                game.Description
                                            ));
            }
            catch (Exception ex)
            {
                var traceid = Activity.Current?.TraceId;

                logger.LogError(ex, "Could not process a request {Machine}. TraceId: {TraceId}",
                                    Environment.MachineName, traceid);

                return Results.Problem(
                    title: "An error occure while process your request.",
                    statusCode: StatusCodes.Status500InternalServerError,
                    extensions: new Dictionary<string, object?>
                    {
                        {"traceid", traceid.ToString()}
                    }
                );
            }

        }).WithName(EndpointNames.GetGameEndpointName);
    }

    private static async Task<Game?> FindGameAsync(Guid id, GameStoreContext dbContext)
    {
        throw new SqliteException("The database is not available", 14);
        return await dbContext.Games.FindAsync(id);
    }
}

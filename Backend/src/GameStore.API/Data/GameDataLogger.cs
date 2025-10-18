using System;

namespace GameStore.API.Data;

public class GameDataLogger(GameStoreData data, ILogger<GameDataLogger> logger)
{
    public void PrintGames()
    {
        var games = data.GetGames();

        foreach (var game in games)
        {
            logger.LogInformation("Game: {GameId} | Name: {GameName} | GenreId: {GenreId} | Price: {Price} | ReleaseDate: {ReleaseDate}, Description: {Description}",
                game.Id,
                game.Name,
                game.Genre.Name,
                game.Price,
                game.ReleaseDate.ToShortDateString(),
                game.Description);
        }
    }
}

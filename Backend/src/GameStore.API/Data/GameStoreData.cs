using System;
using GameStore.API.Models;

namespace GameStore.API.Data;

public class GameStoreData
{
    private readonly List<Genre> genres =
    [
        new Genre { Id = new Guid("47808bf3-b75e-4379-ad2d-41d5e9da516d"), Name = "Adventure" },
        new Genre { Id = new Guid("76b7e1a2-fe0a-4200-baa5-7fa0608cbdab"), Name = "Action" },
        new Genre { Id = new Guid("a8e34460-c050-47c8-9cfe-828e1ef1b560"), Name = "Strategy" },
    ];

    private readonly List<Game> games;

    public GameStoreData()
    {
        games =
        [
            new Game
            {
                Id = Guid.NewGuid(),
                Name = "The Legend of Zelda: Breath of the Wild",
                Genre = genres.Find(x=> x.Name == "Adventure")!,
                Description = "An open-world adventure game set in the land of Hyrule.",
                Price = 59.99m,
                ReleaseDate = new DateOnly(2017, 3, 3)
            },
            new Game
            {
                Id = Guid.NewGuid(),
                Name = "God of War",
                Genre = genres.Find(x=> x.Name == "Strategy")!,
                Description = "A story-driven game following Kratos and his son Atreus on a journey through Norse mythology.",
                Price = 49.99m,
                ReleaseDate = new DateOnly(2018, 4, 20)
            },
            new Game
            {
                Id = Guid.NewGuid(),
                Name = "Red Dead Redemption 2",
                Genre = genres.Find(x=> x.Name == "Action")!,
                Description = "An epic tale of life in America's unforgiving heartland.",
                Price = 39.99m,
                ReleaseDate = new DateOnly(2018, 10, 26)
            }
        ];
    }

    public IEnumerable<Game> GetGames() => games;
    public IEnumerable<Genre> GetGenres() => genres;

    public Game? GetGame(Guid id) => games.Find(g => g.Id == id);

    public void AddGame(Game game)
    {
        game.Id = Guid.NewGuid();
        games.Add(game);
    }

    public void RemoveGame(Guid id)
    {
        games.RemoveAll(game => game.Id == id);
    }
    
    public Genre? GetGenre(Guid id) => genres.Find(g => g.Id == id);
}

using GameStore.API.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Game> games =
[
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "The Legend of Zelda: Breath of the Wild",
        Genre = "Action-adventure",
        Price = 59.99m,
        ReleaseDate = new DateOnly(2017, 3, 3)
    },
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "God of War",
        Genre = "Action-adventure",
        Price = 49.99m,
        ReleaseDate = new DateOnly(2018, 4, 20)
    },
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "Red Dead Redemption 2",
        Genre = "Action-adventure",
        Price = 39.99m,
        ReleaseDate = new DateOnly(2018, 10, 26)
    }
];

app.MapGet("/", () => "Hello World!");

app.Run();

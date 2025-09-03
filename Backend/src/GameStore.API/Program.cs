using System.Reflection.Metadata.Ecma335;
using GameStore.API.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEnpointName = "GetGame";

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

// GET  /games
app.MapGet("/games", () => games);

//GET /games/{id}
app.MapGet("/games/{id}", (Guid id) =>
{
    var game = games.FirstOrDefault(g => g.Id == id);
    return game is not null ? Results.Ok(game) : Results.NotFound();
})
.WithName(GetGameEnpointName);

//POST /games
app.MapPost("/games", (Game newGame) =>
{
    newGame.Id = Guid.NewGuid();
    games.Add(newGame);
    return Results.CreatedAtRoute(
            GetGameEnpointName,
            new { id = newGame.Id },
            newGame);
}).WithParameterValidation();

//PUT /games/{id}
app.MapPut("/games/{id}", (Guid id, Game updatedGame) =>
{
    var existingGame = games.Find(game => game.Id == id);

    if (existingGame is null)
    {
        return Results.NotFound();
    }

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();

}).WithParameterValidation();

app.MapDelete("/games/{id}", (Guid id) =>
{
    var game = games.RemoveAll(g => g.Id == id);
   
    return Results.NoContent();
});

app.Run();

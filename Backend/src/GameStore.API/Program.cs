using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using GameStore.API.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEnpointName = "GetGame";

List<Genre> genres =
[
    new Genre { Id = Guid.NewGuid(), Name = "Adventure" },
    new Genre { Id = Guid.NewGuid(), Name = "Action" },
    new Genre { Id = Guid.NewGuid(), Name = "Strategy" },
];

List<Game> games =

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

// Get /genres
app.MapGet("/genres", () => genres);

// GET  /games
app.MapGet("/games", () => games.Select(x=> new GameSummaryDTO
(
    x.Id,
    x.Name,
    x.Genre is not null ? genres.First(g => g.Name == x.Genre.Name).Id : Guid.Empty,
    x.Price
)).ToList());

//GET /games/{id}
app.MapGet("/games/{id}", (Guid id) =>
{
    var game = games.FirstOrDefault(g => g.Id == id);
    return game is not null ? Results.Ok(new GameDetailsDTO
    (
        game.Id,
        game.Name,
        game.Genre.Id,
        game.Price,
        game.ReleaseDate,
        game.Description
    )) : Results.NotFound();
})
.WithName(GetGameEnpointName);

//POST /games
app.MapPost("/games", (CreateGameDTO newGame) =>
{
    var genre = genres.FirstOrDefault(g => g.Id == newGame.GenreId);

    if (genre is null)
    {
        return Results.BadRequest($"Genre with Id {newGame.GenreId} does not exist.");
    }
    var game = new Game
    {
        Id = Guid.NewGuid(),
        Name = newGame.Name,
        Genre = genre,
        Price = newGame.Price,
        ReleaseDate = newGame.ReleaseDate,
        Description = newGame.Description
    };

    games.Add(game);
    return Results.CreatedAtRoute(
            GetGameEnpointName,
            new { id = game.Id },
            new GameDetailsDTO
            (
                game.Id,
                game.Name,
                game.Genre.Id,
                game.Price,
                game.ReleaseDate,
                game.Description
            ));

}).WithParameterValidation();

//PUT /games/{id}
app.MapPut("/games/{id}", (Guid id, UpdateGameDTO updatedGame) =>
{
    var existingGame = games.Find(game => game.Id == id);

    if (existingGame is null)
    {
        return Results.NotFound();
    }

    var genre = genres.FirstOrDefault(g => g.Id == updatedGame.GenreId);
    if (genre is null)
    {
        return Results.BadRequest($"Genre with Id {updatedGame.GenreId} does not exist.");
    }

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = genre;
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


public record GameDetailsDTO(Guid Id, string Name, Guid GenreId, decimal Price, DateOnly ReleaseDate, string Description);

public record GameSummaryDTO(Guid Id, string Name, Guid GenreId, decimal Price);    

public record CreateGameDTO(
    [Required][StringLength(50)] string Name,
    Guid GenreId,
    [Range(1,100)] decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(500)] string Description);

public record UpdateGameDTO(
    [Required][StringLength(50)] string Name,
    Guid GenreId,
    [Range(1,100)] decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(500)] string Description);
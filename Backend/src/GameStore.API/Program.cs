using System.ComponentModel.DataAnnotations;
using GameStore.API.Data;
using GameStore.API.Features.Games.CreateGame;
using GameStore.API.Features.Games.DeleteGame;
using GameStore.API.Features.Games.GetGame;
using GameStore.API.Features.Games.GetGames;
using GameStore.API.Features.Games.UpdateGame;
using GameStore.API.Features.Genre.GetGenres;

GameStoreData data = new();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGetGames(data);
app.MapGetGame(data);
app.MapCreateGame(data);
app.MapUpdateGame(data);
app.MapDeleteGame(data);
app.MapGetGenres(data);
app.Run();



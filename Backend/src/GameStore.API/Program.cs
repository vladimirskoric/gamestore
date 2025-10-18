using System.ComponentModel.DataAnnotations;
using GameStore.API.Data;
using GameStore.API.Features.Games;
using GameStore.API.Features.Genres;

GameStoreData data = new();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGames(data);
app.MapGenres(data);
app.Run();



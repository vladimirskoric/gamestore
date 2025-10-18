using GameStore.API.Data;
using GameStore.API.Features.Games;
using GameStore.API.Features.Genres;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GameDataLogger>();
builder.Services.AddSingleton<GameStoreData>();

var app = builder.Build();

app.MapGames();
app.MapGenres();
app.Run();



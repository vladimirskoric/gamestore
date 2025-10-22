using GameStore.API.Data;
using GameStore.API.Features.Games;
using GameStore.API.Features.Genres;
using GameStore.API.Shared.Timing;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddProblemDetails();
builder.Services.AddSqlite<GameStoreContext>(connString);
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestMethod |
                            HttpLoggingFields.RequestPath |
                            HttpLoggingFields.ResponseStatusCode |
                            HttpLoggingFields.Duration;
    options.CombineLogs = true;
});

var app = builder.Build();

app.MapGames();
app.MapGenres();

app.UseMiddleware<RequestTimingMiddleware>();
app.UseHttpLogging();

if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.UseStatusCodePages();

await app.InitializeDbAsync();

app.Run();



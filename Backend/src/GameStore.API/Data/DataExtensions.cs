using System;
using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public static class DataExtensions
{
    public static void MigratedDB(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        dbContext.Database.Migrate();
    }

    public static void SeedDB(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        if (!dbContext.Genres.Any())
        {
            dbContext.Genres.AddRange(
                new Genre { Name = "Adventure" },
                new Genre { Name = "Action" },
                new Genre { Name = "Strategy" },
                new Genre { Name = "Racing" },
                new Genre { Name = "Sports" },
                new Genre { Name = "Kids and Family" }
            );
        }

        dbContext.SaveChanges();
    }

    public static void InitializeDb(this WebApplication app)
    {
        app.MigratedDB();
        app.SeedDB();
    }
}

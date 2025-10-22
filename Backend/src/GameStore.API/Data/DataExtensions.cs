using System;
using System.Threading.Tasks;
using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public static class DataExtensions
{
    public static async Task MigrateAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        await dbContext.Database.MigrateAsync();
    }

    public static async Task SeedDBAsync(this WebApplication app)
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

       await dbContext.SaveChangesAsync();
    }

    public static async Task InitializeDbAsync(this WebApplication app)
    {
        await app.MigrateAsync();
        await app.SeedDBAsync();

        app.Logger.LogInformation(02, "Database is ready.");      
    }
}

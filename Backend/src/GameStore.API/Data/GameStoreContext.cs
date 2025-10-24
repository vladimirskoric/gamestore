using GameStore.Api.Models;
using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) 
    : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

    public DbSet<CustomerBasket> CustomerBaskets => Set<CustomerBasket>();

    public DbSet<BasketItem> BasketItems => Set<BasketItem>();
}

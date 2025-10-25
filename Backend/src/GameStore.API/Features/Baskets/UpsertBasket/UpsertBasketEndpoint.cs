using GameStore.Api.Data;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Baskets.UpsertBasket;

public static class UpsertBasketEndpoint
{
    public static void MapUpsertBasket(this IEndpointRouteBuilder app)
    {
        // PUT /baskets/b3f5c1d2-4e2b-4a5e-9b8e-1f2d3c4b5a6f
        app.MapPut("/{userId}", async (
            Guid userId,
            UpsertBasketDto upsertBasketDto,
            GameStoreContext dbContext
        ) =>
        {
            var basket = await dbContext.Baskets
                                        .Include(basket => basket.Items)
                                        .FirstOrDefaultAsync(
                                            basket => basket.Id == userId);

            if (basket is null)
            {
                basket = new CustomerBasket
                {
                    Id = userId,
                    Items = upsertBasketDto.Items.Select(item => new BasketItem
                    {
                        GameId = item.Id,
                        Quantity = item.Quantity
                    }).ToList()
                };

                dbContext.Baskets.Add(basket);
            }
            else
            {
                basket.Items = upsertBasketDto.Items.Select(item => new BasketItem
                {
                    GameId = item.Id,
                    Quantity = item.Quantity
                }).ToList();
            }

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}

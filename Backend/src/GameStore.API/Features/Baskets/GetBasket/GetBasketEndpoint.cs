using System;
using GameStore.Api.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Baskets.GetBasket;

public static class GetBasketEndpoint
{
    public static void MapGetBasket(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{userId}", async
        (
             Guid userId,
             GameStoreContext dbContext
        ) =>
            {
            if (userId == Guid.Empty)
            {
                return Results.BadRequest("UserId is required.");
            }

                var basket = await dbContext.Baskets
                    .Include(basket => basket.Items)
                    .ThenInclude(item => item.Game)
                    .FirstOrDefaultAsync(basket => basket.Id == userId)
                    ?? new() { Id = userId };

                var basketDto = new BasketDto(
                    CustomerId: basket.Id,
                    Items: basket.Items.Select(item => new BasketItemDto(
                        item.GameId,
                        item.Game!.Name,
                        item.Game.Price,
                        item.Quantity,
                        item.Game.ImageUri
                    )).OrderBy(item => item.Name)
                );
                
                return Results.Ok(basketDto);
            }
        );
    }
}

using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.Shared.FileUpload;
using GameStore.API.Features.Games.UpdateGame;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static void MapUpdateGame(this IEndpointRouteBuilder app)
    {
        // PUT /games/122233-434d-43434....
        app.MapPut("/{id}", async (
            Guid id,
            [FromForm] UpdateGameDTO gameDto,
            GameStoreContext dbContext,
            FileUploader fileUploader) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            if (gameDto.ImageFile is not null)
            {
                var fileUploadResult = await fileUploader.UploadFileAsync(
                    gameDto.ImageFile,
                    StorageNames.GameImagesFolder
                );

                if (!fileUploadResult.IsSucess)
                {
                    return Results.BadRequest(new { message = fileUploadResult.ErrorMessage });
                }

                existingGame.ImageUri = fileUploadResult.FileUrl!;
            }

            existingGame.Name = gameDto.Name;
            existingGame.GenreId = gameDto.GenreId;
            existingGame.Price = gameDto.Price;
            existingGame.ReleaseDate = gameDto.ReleaseDate;
            existingGame.Description = gameDto.Description;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithParameterValidation()
        .DisableAntiforgery();
    }
}

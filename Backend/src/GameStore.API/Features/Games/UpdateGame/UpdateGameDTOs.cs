using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Features.Games.UpdateGame;

public record UpdateGameDTO(
    [Required][StringLength(50)] string Name,
    Guid GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(500)] string Description);

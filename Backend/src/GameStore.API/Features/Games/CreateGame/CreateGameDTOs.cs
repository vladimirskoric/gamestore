using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Features.Games.CreateGame;

public record CreateGameDTO(
    [Required][StringLength(50)] string Name,
    Guid GenreId,
    [Range(1,100)] decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(500)] string Description);

public record GameDetailsDTO(
    Guid Id,
    string Name,
    Guid GenreId,
    decimal Price,
    DateOnly ReleaseDate,
    string Description);

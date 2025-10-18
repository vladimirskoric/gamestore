using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Features.Games.GetGame;

public record GameDetailsDTO(
    Guid Id,
    string Name,
    Guid GenreId,
    decimal Price,
    DateOnly ReleaseDate,
    string Description);


namespace GameStore.API.Features.Games.GetGames;

public record GameSummaryDTO(
    Guid Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);    

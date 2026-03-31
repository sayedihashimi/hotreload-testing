namespace LinkVault.MinimalApi.Models;

public record CreateLinkRequest(string Url, string Title, string? Description, int CollectionId, List<string>? Tags);
public record UpdateLinkRequest(string? Title, string? Description, bool? IsFavorite, int? CollectionId);
public record LinkResponse(int Id, string Url, string Title, string? Description, bool IsFavorite, int ClickCount, DateTime CreatedAt, string CollectionName, List<string> Tags);

public record CreateCollectionRequest(string Name, string? Description, string Color, bool IsPublic);
public record UpdateCollectionRequest(string? Name, string? Description, string? Color, bool? IsPublic);
public record CollectionResponse(int Id, string Name, string? Description, string Color, bool IsPublic, int LinkCount);
public record CollectionDetailResponse(int Id, string Name, string? Description, string Color, bool IsPublic, List<LinkResponse> Links);

public record CreateTagRequest(string Name);
public record TagResponse(int Id, string Name, int LinkCount);

public record StatsResponse(int TotalLinks, int TotalCollections, int TotalClicks, int FavoriteCount);
public record TopClickedResponse(int Id, string Url, string Title, int ClickCount, string CollectionName);

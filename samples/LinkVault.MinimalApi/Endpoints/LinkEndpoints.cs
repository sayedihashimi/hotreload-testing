using LinkVault.MinimalApi.Models;
using LinkVault.MinimalApi.Services;

namespace LinkVault.MinimalApi.Endpoints;

public static class LinkEndpoints
{
    public static RouteGroupBuilder MapLinkEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/links")
            .WithTags("Links");

        group.MapGet("/", async (ILinkService linkService, string? search, int? collectionId, bool? favoritesOnly) =>
            TypedResults.Ok(await linkService.GetAllAsync(search, collectionId, favoritesOnly)))
            .WithName("GetLinks")
            .WithSummary("Get all links with optional filtering");

        group.MapGet("/{id:int}", async Task<IResult> (int id, ILinkService linkService) =>
            await linkService.GetByIdAsync(id) is { } link
                ? TypedResults.Ok(link)
                : TypedResults.NotFound())
            .WithName("GetLinkById")
            .WithSummary("Get a link by ID");

        group.MapPost("/", async (CreateLinkRequest request, ILinkService linkService) =>
        {
            var link = await linkService.CreateAsync(request);
            return TypedResults.Created($"/api/links/{link.Id}", link);
        })
            .WithName("CreateLink")
            .WithSummary("Create a new link");

        group.MapPut("/{id:int}", async Task<IResult> (int id, UpdateLinkRequest request, ILinkService linkService) =>
            await linkService.UpdateAsync(id, request) is { } link
                ? TypedResults.Ok(link)
                : TypedResults.NotFound())
            .WithName("UpdateLink")
            .WithSummary("Update an existing link");

        group.MapDelete("/{id:int}", async Task<IResult> (int id, ILinkService linkService) =>
            await linkService.DeleteAsync(id)
                ? TypedResults.NoContent()
                : TypedResults.NotFound())
            .WithName("DeleteLink")
            .WithSummary("Delete a link");

        group.MapPost("/{id:int}/click", async Task<IResult> (int id, ILinkService linkService) =>
            await linkService.RecordClickAsync(id) is { } link
                ? TypedResults.Ok(link)
                : TypedResults.NotFound())
            .WithName("RecordClick")
            .WithSummary("Record a click on a link");

        group.MapGet("/favorites", async (ILinkService linkService) =>
            TypedResults.Ok(await linkService.GetFavoritesAsync()))
            .WithName("GetFavoriteLinks")
            .WithSummary("Get all favorite links");

        return group;
    }
}

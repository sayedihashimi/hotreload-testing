using LinkVault.MinimalApi.Models;
using LinkVault.MinimalApi.Services;

namespace LinkVault.MinimalApi.Endpoints;

public static class CollectionEndpoints
{
    public static RouteGroupBuilder MapCollectionEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/collections")
            .WithTags("Collections");

        group.MapGet("/", async (ICollectionService collectionService) =>
            TypedResults.Ok(await collectionService.GetAllAsync()))
            .WithName("GetCollections")
            .WithSummary("Get all collections");

        group.MapGet("/{id:int}", async Task<IResult> (int id, ICollectionService collectionService) =>
            await collectionService.GetByIdAsync(id) is { } collection
                ? TypedResults.Ok(collection)
                : TypedResults.NotFound())
            .WithName("GetCollectionById")
            .WithSummary("Get a collection with its links");

        group.MapPost("/", async (CreateCollectionRequest request, ICollectionService collectionService) =>
        {
            var collection = await collectionService.CreateAsync(request);
            return TypedResults.Created($"/api/collections/{collection.Id}", collection);
        })
            .WithName("CreateCollection")
            .WithSummary("Create a new collection");

        group.MapPut("/{id:int}", async Task<IResult> (int id, UpdateCollectionRequest request, ICollectionService collectionService) =>
            await collectionService.UpdateAsync(id, request) is { } collection
                ? TypedResults.Ok(collection)
                : TypedResults.NotFound())
            .WithName("UpdateCollection")
            .WithSummary("Update an existing collection");

        group.MapDelete("/{id:int}", async Task<IResult> (int id, ICollectionService collectionService) =>
            await collectionService.DeleteAsync(id)
                ? TypedResults.NoContent()
                : TypedResults.NotFound())
            .WithName("DeleteCollection")
            .WithSummary("Delete a collection");

        group.MapGet("/{id:int}/links", async (int id, ICollectionService collectionService) =>
            TypedResults.Ok(await collectionService.GetLinksInCollectionAsync(id)))
            .WithName("GetLinksInCollection")
            .WithSummary("Get all links in a collection");

        return group;
    }
}

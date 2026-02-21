using LinkVault.MinimalApi.Models;
using LinkVault.MinimalApi.Services;

namespace LinkVault.MinimalApi.Endpoints;

public static class TagEndpoints
{
    public static RouteGroupBuilder MapTagEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/tags")
            .WithTags("Tags");

        group.MapGet("/", async (ITagService tagService) =>
            TypedResults.Ok(await tagService.GetAllAsync()))
            .WithName("GetTags")
            .WithSummary("Get all tags with usage counts");

        group.MapPost("/", async (CreateTagRequest request, ITagService tagService) =>
        {
            var tag = await tagService.CreateAsync(request);
            return TypedResults.Created($"/api/tags/{tag.Id}", tag);
        })
            .WithName("CreateTag")
            .WithSummary("Create a new tag");

        group.MapDelete("/{id:int}", async Task<IResult> (int id, ITagService tagService) =>
            await tagService.DeleteAsync(id)
                ? TypedResults.NoContent()
                : TypedResults.NotFound())
            .WithName("DeleteTag")
            .WithSummary("Delete a tag");

        return group;
    }
}

using LinkVault.MinimalApi.Services;

namespace LinkVault.MinimalApi.Endpoints;

public static class StatsEndpoints
{
    public static RouteGroupBuilder MapStatsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/stats")
            .WithTags("Stats");

        group.MapGet("/", async (IStatsService statsService) =>
            TypedResults.Ok(await statsService.GetStatsAsync()))
            .WithName("GetStats")
            .WithSummary("Get overall statistics");

        group.MapGet("/top-clicked", async (IStatsService statsService, int? count) =>
            TypedResults.Ok(await statsService.GetTopClickedAsync(count ?? 10)))
            .WithName("GetTopClicked")
            .WithSummary("Get the most clicked links");

        return group;
    }
}

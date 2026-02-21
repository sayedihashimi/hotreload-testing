using Microsoft.AspNetCore.Mvc;
using FitLog.Api.Models;
using FitLog.Api.Services;

namespace FitLog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatsController(IStatsService statsService) : ControllerBase
{
    [HttpGet("weekly-summary")]
    public async Task<ActionResult<WeeklySummaryResponse>> GetWeeklySummary()
    {
        var summary = await statsService.GetWeeklySummaryAsync();
        return Ok(summary);
    }

    [HttpGet("monthly-summary")]
    public async Task<ActionResult<MonthlySummaryResponse>> GetMonthlySummary()
    {
        var summary = await statsService.GetMonthlySummaryAsync();
        return Ok(summary);
    }

    [HttpGet("muscle-group-breakdown")]
    public async Task<ActionResult<List<MuscleGroupBreakdownResponse>>> GetMuscleGroupBreakdown()
    {
        var breakdown = await statsService.GetMuscleGroupBreakdownAsync();
        return Ok(breakdown);
    }

    [HttpGet("progress/{exerciseDefinitionId:int}")]
    public async Task<ActionResult<ProgressResponse>> GetProgress(int exerciseDefinitionId)
    {
        var progress = await statsService.GetProgressAsync(exerciseDefinitionId);
        if (progress == null) return NotFound();
        return Ok(progress);
    }
}

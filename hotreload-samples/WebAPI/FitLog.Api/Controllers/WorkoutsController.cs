using Microsoft.AspNetCore.Mvc;
using FitLog.Api.Models;
using FitLog.Api.Services;

namespace FitLog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutsController(IWorkoutService workoutService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<WorkoutSummaryResponse>>> GetAll(
        [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] WorkoutType? type)
    {
        var workouts = await workoutService.GetAllAsync(fromDate, toDate, type);
        return Ok(workouts);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<WorkoutResponse>> GetById(int id)
    {
        var workout = await workoutService.GetByIdAsync(id);
        if (workout == null) return NotFound();
        return Ok(workout);
    }

    [HttpPost]
    public async Task<ActionResult<WorkoutResponse>> Create(CreateWorkoutRequest request)
    {
        var workout = await workoutService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = workout.Id }, workout);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<WorkoutResponse>> Update(int id, UpdateWorkoutRequest request)
    {
        var workout = await workoutService.UpdateAsync(id, request);
        if (workout == null) return NotFound();
        return Ok(workout);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await workoutService.DeleteAsync(id)) return NotFound();
        return NoContent();
    }
}

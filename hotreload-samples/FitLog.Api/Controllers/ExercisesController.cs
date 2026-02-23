using Microsoft.AspNetCore.Mvc;
using FitLog.Api.Models;
using FitLog.Api.Services;

namespace FitLog.Api.Controllers;

[ApiController]
[Route("api/workouts/{workoutId:int}/[controller]")]
public class ExercisesController(IExerciseService exerciseService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ExerciseResponse>> AddToWorkout(int workoutId, CreateExerciseRequest request)
    {
        var exercise = await exerciseService.AddToWorkoutAsync(workoutId, request);
        return Created($"/api/workouts/{workoutId}/exercises/{exercise.Id}", exercise);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ExerciseResponse>> Update(int workoutId, int id, UpdateExerciseRequest request)
    {
        var exercise = await exerciseService.UpdateAsync(workoutId, id, request);
        if (exercise == null) return NotFound();
        return Ok(exercise);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int workoutId, int id)
    {
        if (!await exerciseService.DeleteAsync(workoutId, id)) return NotFound();
        return NoContent();
    }
}

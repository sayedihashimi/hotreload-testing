using Microsoft.AspNetCore.Mvc;
using FitLog.Api.Models;
using FitLog.Api.Services;

namespace FitLog.Api.Controllers;

[ApiController]
[Route("api/exercise-definitions")]
public class ExerciseDefinitionsController(IExerciseDefinitionService definitionService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ExerciseDefinitionResponse>>> GetAll()
    {
        var definitions = await definitionService.GetAllAsync();
        return Ok(definitions);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ExerciseDefinitionResponse>> GetById(int id)
    {
        var definition = await definitionService.GetByIdAsync(id);
        if (definition == null) return NotFound();
        return Ok(definition);
    }

    [HttpPost]
    public async Task<ActionResult<ExerciseDefinitionResponse>> Create(CreateExerciseDefinitionRequest request)
    {
        var definition = await definitionService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = definition.Id }, definition);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ExerciseDefinitionResponse>> Update(int id, UpdateExerciseDefinitionRequest request)
    {
        var definition = await definitionService.UpdateAsync(id, request);
        if (definition == null) return NotFound();
        return Ok(definition);
    }
}

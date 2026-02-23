using Microsoft.AspNetCore.Mvc;
using FitLog.Api.Models;
using FitLog.Api.Services;

namespace FitLog.Api.Controllers;

[ApiController]
[Route("api/personal-records")]
public class PersonalRecordsController(IPersonalRecordService prService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PersonalRecordResponse>>> GetAll()
    {
        var records = await prService.GetAllAsync();
        return Ok(records);
    }

    [HttpGet("exercise/{exerciseDefinitionId:int}")]
    public async Task<ActionResult<List<PersonalRecordResponse>>> GetByExercise(int exerciseDefinitionId)
    {
        var records = await prService.GetByExerciseAsync(exerciseDefinitionId);
        return Ok(records);
    }

    [HttpPost]
    public async Task<ActionResult<PersonalRecordResponse>> Create(CreatePersonalRecordRequest request)
    {
        var record = await prService.CreateAsync(request);
        return Created($"/api/personal-records/{record.Id}", record);
    }
}

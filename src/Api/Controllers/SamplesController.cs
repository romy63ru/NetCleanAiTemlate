using Application.Common;
using Application.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SamplesController : ControllerBase
{
    private readonly ISampleService _service;

    public SamplesController(ISampleService service)
    {
        _service = service;
    }

    public record SampleDto(Guid Id, string Name, string? Description);
    public record CreateSampleRequest(string Name, string? Description);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSampleRequest request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request.Name, request.Description, ct);
        if (!result.IsSuccess)
            return Problem(result.Error, statusCode: result.Code == "validation" ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError);

        var dto = ToDto(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        var result = await _service.GetAsync(id, ct);
        if (!result.IsSuccess)
            return Problem(result.Error, statusCode: result.Code == "not_found" ? StatusCodes.Status404NotFound : StatusCodes.Status500InternalServerError);

        return Ok(ToDto(result.Value!));
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken ct)
    {
        var result = await _service.ListAsync(ct);
        return Ok(result.Value!.Select(ToDto));
    }

    private static SampleDto ToDto(SampleEntity e) => new(e.Id, e.Name, e.Description);
}

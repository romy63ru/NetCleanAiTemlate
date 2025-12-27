using Api.Controllers;
using Application.Common;
using Application.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Api.Tests;

public class SamplesControllerTests
{
    private sealed class FakeService : ISampleService
    {
        public Task<Result<SampleEntity>> CreateAsync(string name, string? description, CancellationToken ct)
            => string.IsNullOrWhiteSpace(name)
                ? Task.FromResult(Result<SampleEntity>.Failure("Name is required", code: "validation"))
                : Task.FromResult(Result<SampleEntity>.Success(SampleEntity.Create(Guid.NewGuid(), name, description)));

        public Task<Result<SampleEntity>> GetAsync(Guid id, CancellationToken ct)
            => Task.FromResult(Result<SampleEntity>.Failure("Not found", code: "not_found"));

        public Task<Result<IReadOnlyList<SampleEntity>>> ListAsync(CancellationToken ct)
            => Task.FromResult(Result<IReadOnlyList<SampleEntity>>.Success(Array.Empty<SampleEntity>()));
    }

    [Fact]
    public async Task Create_Valid_ReturnsCreated()
    {
        var controller = new SamplesController(new FakeService());
        var request = new SamplesController.CreateSampleRequest("A", null);
        var result = await controller.Create(request, CancellationToken.None);
        var created = Assert.IsType<CreatedAtActionResult>(result);
        var dto = Assert.IsType<SamplesController.SampleDto>(created.Value);
        Assert.Equal("A", dto.Name);
    }

    [Fact]
    public async Task Create_Invalid_ReturnsProblem_400()
    {
        var controller = new SamplesController(new FakeService());
        var request = new SamplesController.CreateSampleRequest(" ", null);
        var result = await controller.Create(request, CancellationToken.None);
        var problem = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, problem.StatusCode);
    }
}

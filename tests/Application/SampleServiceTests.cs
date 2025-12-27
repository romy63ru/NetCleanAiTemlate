using Application.Abstractions;
using Application.Common;
using Application.Services;
using Domain;

namespace Application.Tests;

public class SampleServiceTests
{
    private sealed class FakeRepo : ISampleRepository
    {
        private readonly Dictionary<Guid, SampleEntity> _store = new();
        public Task<SampleEntity?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            _store.TryGetValue(id, out var e);
            return Task.FromResult(e);
        }
        public Task<IReadOnlyList<SampleEntity>> ListAsync(CancellationToken ct)
        {
            return Task.FromResult<IReadOnlyList<SampleEntity>>(_store.Values.ToList());
        }
        public Task AddAsync(SampleEntity entity, CancellationToken ct)
        {
            _store[entity.Id] = entity;
            return Task.CompletedTask;
        }
        public Task UpdateAsync(SampleEntity entity, CancellationToken ct)
        {
            _store[entity.Id] = entity;
            return Task.CompletedTask;
        }
    }

    [Fact]
    public async Task Create_Valid_ReturnsSuccess()
    {
        var service = new SampleService(new FakeRepo());
        var result = await service.CreateAsync("A", "B", CancellationToken.None);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("A", result.Value!.Name);
    }

    [Fact]
    public async Task Create_InvalidName_ReturnsValidationFailure()
    {
        var service = new SampleService(new FakeRepo());
        var result = await service.CreateAsync(" ", null, CancellationToken.None);
        Assert.False(result.IsSuccess);
        Assert.Equal("validation", result.Code);
    }

    [Fact]
    public async Task Get_NotFound_ReturnsFailure()
    {
        var service = new SampleService(new FakeRepo());
        var result = await service.GetAsync(Guid.NewGuid(), CancellationToken.None);
        Assert.False(result.IsSuccess);
        Assert.Equal("not_found", result.Code);
    }
}

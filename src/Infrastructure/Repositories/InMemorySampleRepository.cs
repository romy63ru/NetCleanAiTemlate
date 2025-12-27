using Application.Abstractions;
using Domain;
using System.Collections.Concurrent;

namespace Infrastructure.Repositories;

public class InMemorySampleRepository : ISampleRepository
{
    private readonly ConcurrentDictionary<Guid, SampleEntity> _store = new();

    public Task<SampleEntity?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        _store.TryGetValue(id, out var entity);
        return Task.FromResult(entity);
    }

    public Task<IReadOnlyList<SampleEntity>> ListAsync(CancellationToken ct)
    {
        var list = _store.Values.ToList();
        return Task.FromResult<IReadOnlyList<SampleEntity>>(list);
    }

    public Task AddAsync(SampleEntity entity, CancellationToken ct)
    {
        _store[entity.Id] = entity;
        return Task.CompletedTask;
    }
}

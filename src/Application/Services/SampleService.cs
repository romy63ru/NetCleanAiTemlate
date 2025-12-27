using Application.Abstractions;
using Application.Common;
using Domain;

namespace Application.Services;

public class SampleService : ISampleService
{
    private readonly ISampleRepository _repo;

    public SampleService(ISampleRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<SampleEntity>> CreateAsync(string name, string? description, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<SampleEntity>.Failure("Name is required", code: "validation");

        var entity = SampleEntity.Create(Guid.NewGuid(), name, description);
        await _repo.AddAsync(entity, ct);
        return Result<SampleEntity>.Success(entity);
    }

    public async Task<Result<SampleEntity>> GetAsync(Guid id, CancellationToken ct)
    {
        var found = await _repo.GetByIdAsync(id, ct);
        return found is null
            ? Result<SampleEntity>.Failure("Not found", code: "not_found")
            : Result<SampleEntity>.Success(found);
    }

    public async Task<Result<IReadOnlyList<SampleEntity>>> ListAsync(CancellationToken ct)
    {
        var list = await _repo.ListAsync(ct);
        return Result<IReadOnlyList<SampleEntity>>.Success(list);
    }
}

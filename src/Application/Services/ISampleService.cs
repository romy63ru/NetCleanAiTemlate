using Application.Common;
using Domain;

namespace Application.Services;

public interface ISampleService
{
    Task<Result<SampleEntity>> CreateAsync(string name, string? description, CancellationToken ct);
    Task<Result<SampleEntity>> GetAsync(Guid id, CancellationToken ct);
    Task<Result<IReadOnlyList<SampleEntity>>> ListAsync(CancellationToken ct);
}

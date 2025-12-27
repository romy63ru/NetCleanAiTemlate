using Domain;

namespace Application.Abstractions;

public interface ISampleRepository
{
    Task<SampleEntity?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<SampleEntity>> ListAsync(CancellationToken ct);
    Task AddAsync(SampleEntity entity, CancellationToken ct);
}

using Infrastructure.Repositories;
using Domain;

namespace Infrastructure.Tests;

public class InMemorySampleRepositoryTests
{
    [Fact]
    public async Task AddAndGet_Works()
    {
        var repo = new InMemorySampleRepository();
        var e = SampleEntity.Create(Guid.NewGuid(), "A", null);
        await repo.AddAsync(e, CancellationToken.None);
        var found = await repo.GetByIdAsync(e.Id, CancellationToken.None);
        Assert.NotNull(found);
        Assert.Equal("A", found!.Name);
    }

    [Fact]
    public async Task List_ReturnsItems()
    {
        var repo = new InMemorySampleRepository();
        await repo.AddAsync(SampleEntity.Create(Guid.NewGuid(), "A", null), CancellationToken.None);
        await repo.AddAsync(SampleEntity.Create(Guid.NewGuid(), "B", null), CancellationToken.None);
        var list = await repo.ListAsync(CancellationToken.None);
        Assert.True(list.Count >= 2);
    }
}

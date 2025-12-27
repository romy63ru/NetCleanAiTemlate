using Domain;

namespace Domain.Tests;

public class SampleEntityTests
{
    [Fact]
    public void Create_Valid_ShouldSucceed()
    {
        var id = Guid.NewGuid();
        var e = SampleEntity.Create(id, "Name", "Desc");
        Assert.Equal(id, e.Id);
        Assert.Equal("Name", e.Name);
        Assert.Equal("Desc", e.Description);
    }

    [Fact]
    public void Create_EmptyId_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() => SampleEntity.Create(Guid.Empty, "Name", null));
    }

    [Fact]
    public void Create_EmptyName_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() => SampleEntity.Create(Guid.NewGuid(), " ", null));
    }
}

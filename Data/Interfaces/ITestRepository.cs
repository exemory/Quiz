using Data.Entities;

namespace Data.Interfaces;

/// <summary>
/// Repository of <see cref="Test"/> entities
/// </summary>
public interface ITestRepository : IRepository<Test>
{
    public Task<IEnumerable<Test>> GetAllowedTestsForUserAsync(Guid userId);
}
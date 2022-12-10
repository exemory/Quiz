using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

/// <inheritdoc cref="ITestRepository" />
public class TestRepository : Repository<Test>, ITestRepository
{
    public TestRepository(QuizContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Test>> GetAllowedTestsForUserAsync(Guid userId)
    {
        return await Entities.Where(t => t.AllowedUsers.Select(u => u.Id).Contains(userId))
            .AsNoTracking()
            .ToListAsync();
    }
}
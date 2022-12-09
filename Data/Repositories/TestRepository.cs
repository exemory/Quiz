using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

/// <inheritdoc cref="ITestRepository" />
public class TestRepository : Repository<Test>, ITestRepository
{
    public TestRepository(QuizContext context) : base(context)
    {
    }
}
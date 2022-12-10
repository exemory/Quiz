using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

/// <inheritdoc cref="IQuestionRepository" />
public class QuestionRepository : Repository<Question>, IQuestionRepository
{
    public QuestionRepository(QuizContext context) : base(context)
    {
    }

    public async Task<ICollection<Question>> GetAllByTestIdAsync(Guid testId)
    {
        return await Entities.Include(q => q.Answers)
            .Where(q => q.TestId == testId)
            .AsNoTracking()
            .ToListAsync();
    }
}
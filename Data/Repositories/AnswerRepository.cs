using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

/// <inheritdoc cref="IAnswerRepository" />
public class AnswerRepository : Repository<Answer>, IAnswerRepository
{
    public AnswerRepository(QuizContext context) : base(context)
    {
    }
}
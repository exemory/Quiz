using Data.Interfaces;

namespace Data;

/// <inheritdoc />
public class UnitOfWork : IUnitOfWork
{
    private readonly QuizContext _context;

    /// <summary>
    /// Constructor for initializing a <see cref="UnitOfWork"/> class instance
    /// </summary>
    /// <param name="context">Context of the database</param>
    /// <param name="answerRepository">Answer repository</param>
    /// <param name="questionRepository">Question repository</param>
    /// <param name="testRepository">Test repository</param>
    public UnitOfWork(QuizContext context, IAnswerRepository answerRepository, IQuestionRepository questionRepository, ITestRepository testRepository)
    {
        _context = context;
        AnswerRepository = answerRepository;
        QuestionRepository = questionRepository;
        TestRepository = testRepository;
    }

    public IAnswerRepository AnswerRepository { get; }
    public IQuestionRepository QuestionRepository { get; }
    public ITestRepository TestRepository { get; }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
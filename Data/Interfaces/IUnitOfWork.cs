namespace Data.Interfaces;

/// <summary>
/// Unit of work
/// </summary>
public interface IUnitOfWork
{
    public IAnswerRepository AnswerRepository { get; }
    public IQuestionRepository QuestionRepository { get; }
    public ITestRepository TestRepository { get; }

    /// <summary>
    /// Save all changes made through the repositories in the context to the database
    /// </summary>
    public Task SaveAsync();
}
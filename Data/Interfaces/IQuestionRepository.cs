using Data.Entities;

namespace Data.Interfaces;

/// <summary>
/// Repository of <see cref="Question"/> entities
/// </summary>
public interface IQuestionRepository : IRepository<Question>
{
    /// <summary>
    /// Get all test's questions by specified test id including answers
    /// </summary>
    /// <param name="testId">Guid of the test which questions need to be retrieved</param>
    /// <returns>Questions of specified test including answers</returns>
    public Task<ICollection<Question>> GetAllByTestIdAsync(Guid testId);
}
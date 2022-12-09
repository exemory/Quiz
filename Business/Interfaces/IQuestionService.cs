using Business.DataTransferObjects;

namespace Business.Interfaces;

/// <summary>
/// Service for questions
/// </summary>
public interface IQuestionService
{
    /// <summary>
    /// Get all test's questions by test id
    /// </summary>
    /// <param name="testId">Guid of the test which questions need to be retrieved</param>
    /// <returns>Questions mapped into <see cref="QuestionDto"/></returns>
    public Task<IEnumerable<QuestionDto>> GetAllByTestIdAsync(Guid testId);
}
using Business.DataTransferObjects;
using Business.Exceptions;

namespace Business.Interfaces;

/// <summary>
/// Service for tests
/// </summary>
public interface ITestService
{
    /// <summary>
    /// Get all tests
    /// </summary>
    /// <returns>The list of tests mapped into <see cref="TestDto"/></returns>
    public Task<IEnumerable<TestDto>> GetAllAsync();

    /// <summary>
    /// Get result of the completed test
    /// </summary>
    /// <param name="testId">Guid of the completed test</param>
    /// <param name="completedTestDto">Completed test data</param>
    /// <returns>Test result mapped into <see cref="TestResultDto"/></returns>
    /// <exception cref="NotFoundException">
    /// Thrown when:
    /// <list type="bullet">
    /// <item><description>The test specified by <paramref name="testId"/> does not exist</description></item>
    /// <item><description>The question does not exist in the test</description></item>
    /// <item><description>The answer does not exist in the question</description></item>
    /// </list>
    /// </exception>
    public Task<TestResultDto> GetTestResultAsync(Guid testId, CompletedTestDto completedTestDto);
}
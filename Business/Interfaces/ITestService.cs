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
    /// Get results of the completed test
    /// </summary>
    /// <param name="testId">Guid of the completed test</param>
    /// <param name="completedTestDto">Completed test data</param>
    /// <returns>Test results mapped into <see cref="TestResultsDto"/></returns>
    /// <exception cref="NotFoundException">
    /// Thrown when one of the questions does not exist in test specified by <paramref name="testId"/>
    /// </exception>
    public Task<TestResultsDto> GetTestResults(Guid testId, CompletedTestDto completedTestDto);
}
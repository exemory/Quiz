using Business.DataTransferObjects;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

/// <summary>
/// Tests controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TestsController : ControllerBase
{
    private readonly ITestService _testService;

    /// <summary>
    /// Constructor for initializing a <see cref="TestsController"/> class instance
    /// </summary>
    /// <param name="testService">Test service</param>
    public TestsController(ITestService testService)
    {
        _testService = testService;
    }

    /// <summary>
    /// Get all tests
    /// </summary>
    /// <returns>Array of tests</returns>
    /// <response code="200">Returns the array of tests</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TestDto>>> GetAll()
    {
        var result = await _testService.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    /// Finish the test and get the result
    /// </summary>
    /// <returns>Test result</returns>
    /// <response code="200">Returns the test result</response>
    /// <response code="404">
    /// The test specified by <paramref name="testId"/> not found /
    /// The question does not exist in the test /
    /// The answer does not exist in the question
    /// </response>
    [HttpPost("{testId:guid}/result")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TestResultDto>> CompleteTest(Guid testId, CompletedTestDto completedTestDto)
    {
        var result = await _testService.GetTestResultAsync(testId, completedTestDto);
        return Ok(result);
    }
}
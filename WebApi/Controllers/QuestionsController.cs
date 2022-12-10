using Business.DataTransferObjects;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _questionService;

    /// <summary>
    /// Constructor for initializing a <see cref="QuestionsController"/> class instance
    /// </summary>
    /// <param name="questionService">Question service</param>
    public QuestionsController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    /// <summary>
    /// Get all test's questions by test id
    /// </summary>
    /// <param name="testId">Guid of the test which questions need to be retrieved</param>
    /// <returns>Array of test's questions</returns>
    /// <response code="200">Returns the array of test's questions</response>
    /// <response code="404">The test not found</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<QuestionDto>>> GetAllByTestId(Guid testId)
    {
        var result = await _questionService.GetAllByTestIdAsync(testId);
        return Ok(result);
    }
}
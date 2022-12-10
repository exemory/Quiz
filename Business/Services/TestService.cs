using AutoMapper;
using Business.DataTransferObjects;
using Business.Exceptions;
using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

/// <inheritdoc />
public class TestService : ITestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISession _session;

    public TestService(IUnitOfWork unitOfWork, IMapper mapper, ISession session)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _session = session;
    }

    public async Task<IEnumerable<TestDto>> GetAllAsync()
    {
        var tests = await _unitOfWork.TestRepository.GetAllowedTestsForUserAsync(_session.UserId!.Value);
        return _mapper.Map<IEnumerable<TestDto>>(tests);
    }

    public async Task<TestResultDto> GetTestResultAsync(Guid testId, CompletedTestDto completedTestDto)
    {
        await CheckIfTestExistsAsync(testId);

        var questions = await _unitOfWork.QuestionRepository.GetAllByTestIdAsync(testId);

        var correctAnswersCount = CalculateCorrectAnswersCount(testId, completedTestDto, questions);

        return new TestResultDto
        {
            QuestionsCount = questions.Count,
            CorrectAnswersCount = correctAnswersCount
        };
    }

    private async Task CheckIfTestExistsAsync(Guid testId)
    {
        var test = await _unitOfWork.TestRepository.GetByIdAsync(testId);

        if (test == null)
        {
            throw new NotFoundException($"Test with id '{testId}' not found");
        }
    }

    private static int CalculateCorrectAnswersCount(Guid testId, CompletedTestDto completedTestDto,
        ICollection<Question> testQuestions)
    {
        var correctAnswersCount = 0;

        foreach (var completedQuestion in completedTestDto.CompletedQuestions)
        {
            var question = testQuestions.FirstOrDefault(q => q.Id == completedQuestion.QuestionId);

            if (question == null)
            {
                throw new NotFoundException(
                    $"Question with id '{completedQuestion.QuestionId}' not found in test with id '{testId}'");
            }

            var answer = question.Answers.FirstOrDefault(a => a.Id == completedQuestion.SelectedAnswerId);

            if (answer == null)
            {
                throw new NotFoundException(
                    $"Answer with id '{completedQuestion.SelectedAnswerId}' not found in question with id '{question.Id}'");
            }

            if (answer.IsCorrect)
            {
                correctAnswersCount++;
            }
        }

        return correctAnswersCount;
    }
}
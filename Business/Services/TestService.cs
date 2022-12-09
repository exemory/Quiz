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

    public TestService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TestDto>> GetAllAsync()
    {
        var tests = await _unitOfWork.TestRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TestDto>>(tests);
    }

    public async Task<TestResultsDto> GetTestResults(Guid testId, CompletedTestDto completedTestDto)
    {
        var questions = await _unitOfWork.QuestionRepository.GetAllByTestIdAsync(testId);

        var correctAnswersCount = CorrectAnswersCount(testId, completedTestDto, questions);
        var mark = questions.Count != 0 ? (float) correctAnswersCount / questions.Count : 0;

        return new TestResultsDto
        {
            QuestionsCount = questions.Count,
            CorrectAnswersCount = correctAnswersCount,
            Mark = mark
        };
    }

    private static int CorrectAnswersCount(Guid testId, CompletedTestDto completedTestDto, ICollection<Question> questions)
    {
        var correctAnswersCount = 0;
        
        foreach (var completedQuestion in completedTestDto.CompletedQuestions)
        {
            var question = questions.FirstOrDefault(q => q.Id == completedQuestion.QuestionId);

            if (question == null)
            {
                throw new NotFoundException(
                    $"Question with id '{completedQuestion.QuestionId}' not found in test with id '{testId}'");
            }

            var correctAnswer = question.Answers.FirstOrDefault(a => a.IsCorrect);

            if (correctAnswer?.Id == completedQuestion.SelectedAnswerId)
            {
                correctAnswersCount++;
            }
        }

        return correctAnswersCount;
    }
}
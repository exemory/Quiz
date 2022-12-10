using AutoFixture;
using AutoMapper;
using Business.DataTransferObjects;
using Business.Exceptions;
using Business.Interfaces;
using Business.Services;
using Data.Entities;
using Data.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Business.Tests.ServiceTests;

public class TestServiceTests : TestBase
{
    private readonly TestService _sut;

    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IMapper _mapper = UnitTestHelper.CreateMapper();
    private readonly ISession _session = Substitute.For<ISession>();

    public TestServiceTests()
    {
        _sut = new TestService(_unitOfWork, _mapper, _session);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTestsThatAllowedForUser()
    {
        // Arrange
        var userId = Fixture.Create<Guid>();
        var tests = Fixture.Build<Test>()
            .Without(t => t.Questions)
            .Without(t => t.AllowedUsers)
            .CreateMany();
        var expected = _mapper.Map<IEnumerable<TestDto>>(tests);

        _session.UserId.Returns(userId);
        _unitOfWork.TestRepository.GetAllowedTestsForUserAsync(userId).Returns(tests);

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetTestResultAsync_ShouldReturnResult()
    {
        // Arrange
        var answersFactory = Fixture.Build<Answer>()
            .With(a => a.IsCorrect, true);
        var questionsFactory = Fixture.Build<Question>()
            .With(q => q.Answers, answersFactory.CreateMany);
        var test = Fixture.Build<Test>()
            .With(t => t.Questions, questionsFactory.CreateMany)
            .Without(t => t.AllowedUsers)
            .Create();
        
        var completedQuestions = test.Questions
            .Select(q => new CompletedQuestionDto
            {
                QuestionId = q.Id,
                SelectedAnswerId = q.Answers.First(a => a.IsCorrect).Id
            })
            .ToList();
        var completedTestDto = Fixture.Build<CompletedTestDto>()
            .With(d => d.CompletedQuestions, completedQuestions)
            .Create();

        var expected = Fixture.Build<TestResultDto>()
            .With(d => d.QuestionsCount, test.Questions.Count)
            .With(d => d.CorrectAnswersCount, test.Questions.Count)
            .Create();

        _unitOfWork.TestRepository.GetByIdAsync(test.Id).Returns(test);
        _unitOfWork.QuestionRepository.GetAllByTestIdAsync(test.Id).Returns(test.Questions.ToList());

        // Act
        var result = await _sut.GetTestResultAsync(test.Id, completedTestDto);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetTestResultAsync_ShouldFail_WhenTestDoesNotExist()
    {
        // Arrange
        var nonexistentTestId = Fixture.Create<Guid>();
        var completedTestDto = Fixture.Create<CompletedTestDto>();
        var expectedExceptionMessage = $"Test with id '{nonexistentTestId}' not found";

        // Act
        var result = () => _sut.GetTestResultAsync(nonexistentTestId, completedTestDto);

        // Assert
        await result.Should().ThrowExactlyAsync<NotFoundException>()
            .WithMessage(expectedExceptionMessage);
    }

    [Fact]
    public async Task GetTestResultAsync_ShouldFail_WhenTestDoesNotContainQuestion()
    {
        // Arrange
        var test = Fixture.Build<Test>()
            .Without(t => t.AllowedUsers)
            .Create();
        var completedTestDto = Fixture.Create<CompletedTestDto>();
        var firstQuestionId = completedTestDto.CompletedQuestions.First().QuestionId;
        var expectedExceptionMessage = $"Question with id '{firstQuestionId}' not found in test with id '{test.Id}'";

        _unitOfWork.TestRepository.GetByIdAsync(test.Id).Returns(test);
        _unitOfWork.QuestionRepository.GetAllByTestIdAsync(test.Id).Returns(test.Questions.ToList());

        // Act
        var result = () => _sut.GetTestResultAsync(test.Id, completedTestDto);

        // Assert
        await result.Should().ThrowExactlyAsync<NotFoundException>()
            .WithMessage(expectedExceptionMessage);
    }

    [Fact]
    public async Task GetTestResultAsync_ShouldFail_WhenQuestionDoesNotContainAnswer()
    {
        // Arrange
        var test = Fixture.Build<Test>()
            .Without(t => t.AllowedUsers)
            .Create();
        var completedQuestions = test.Questions
            .Select(q => new CompletedQuestionDto
            {
                QuestionId = q.Id,
                SelectedAnswerId = Fixture.Create<Guid>()
            })
            .ToList();
        var completedTestDto = Fixture.Build<CompletedTestDto>()
            .With(d => d.CompletedQuestions, completedQuestions)
            .Create();
        
        var firstQuestionId = completedTestDto.CompletedQuestions.First().QuestionId;
        var firstAnswerId = completedQuestions.First().SelectedAnswerId;
        var expectedExceptionMessage =
            $"Answer with id '{firstAnswerId}' not found in question with id '{firstQuestionId}'";

        _unitOfWork.TestRepository.GetByIdAsync(test.Id).Returns(test);
        _unitOfWork.QuestionRepository.GetAllByTestIdAsync(test.Id).Returns(test.Questions.ToList());

        // Act
        var result = () => _sut.GetTestResultAsync(test.Id, completedTestDto);

        // Assert
        await result.Should().ThrowExactlyAsync<NotFoundException>()
            .WithMessage(expectedExceptionMessage);
    }
}
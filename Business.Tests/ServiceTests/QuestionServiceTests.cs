using AutoFixture;
using AutoMapper;
using Business.DataTransferObjects;
using Business.Exceptions;
using Business.Services;
using Data.Entities;
using Data.Interfaces;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Business.Tests.ServiceTests;

public class QuestionServiceTests : TestBase
{
    private readonly QuestionService _sut;

    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IMapper _mapper = UnitTestHelper.CreateMapper();

    public QuestionServiceTests()
    {
        _sut = new QuestionService(_unitOfWork, _mapper);
    }

    [Fact]
    public async Task GetAllByTestIdAsync_ShouldReturnTestQuestions()
    {
        // Arrange
        var test = Fixture.Build<Test>()
            .Without(t => t.AllowedUsers)
            .Create();
        var testQuestions = test.Questions.ToList();
        var expected = _mapper.Map<IEnumerable<QuestionDto>>(testQuestions);

        _unitOfWork.TestRepository.GetByIdAsync(test.Id).Returns(test);
        _unitOfWork.QuestionRepository.GetAllByTestIdAsync(test.Id).Returns(testQuestions);

        // Act
        var result = await _sut.GetAllByTestIdAsync(test.Id);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public async Task GetAllByTestIdAsync_ShouldFail_WhenTestDoesNotExist()
    {
        // Arrange
        var nonexistentTestId = Fixture.Create<Guid>();

        _unitOfWork.TestRepository.GetByIdAsync(nonexistentTestId).ReturnsNull();

        // Act
        var result = () => _sut.GetAllByTestIdAsync(nonexistentTestId);

        // Assert
        await result.Should().ThrowExactlyAsync<NotFoundException>();
    }
}
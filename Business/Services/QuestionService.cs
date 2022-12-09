using AutoMapper;
using Business.DataTransferObjects;
using Business.Interfaces;
using Data.Interfaces;

namespace Business.Services;

/// <inheritdoc />
public class QuestionService : IQuestionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public QuestionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<QuestionDto>> GetAllByTestIdAsync(Guid testId)
    {
        var questions = await _unitOfWork.QuestionRepository.GetAllByTestIdAsync(testId);
        return _mapper.Map<IEnumerable<QuestionDto>>(questions);
    }
}
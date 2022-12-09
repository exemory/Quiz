namespace Business.DataTransferObjects;

public class QuestionDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = default!;

    public IEnumerable<AnswerDto> Answers { get; set; } = new List<AnswerDto>();
}
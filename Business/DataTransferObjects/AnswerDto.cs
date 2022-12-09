namespace Business.DataTransferObjects;

public class AnswerDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = default!;
}
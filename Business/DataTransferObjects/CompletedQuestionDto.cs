namespace Business.DataTransferObjects;

public class CompletedQuestionDto
{
    public Guid QuestionId { get; set; }
    public Guid SelectedAnswerId { get; set; }
}
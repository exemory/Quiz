namespace Business.DataTransferObjects;

public class CompletedQuestion
{
    public Guid QuestionId { get; set; }
    public Guid SelectedAnswerId { get; set; }
}
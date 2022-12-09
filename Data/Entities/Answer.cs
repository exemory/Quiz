namespace Data.Entities;

public class Answer : EntityBase
{
    public string Content { get; set; } = default!;
    public bool IsCorrect { get; set; }

    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = default!;
}
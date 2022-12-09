namespace Data.Entities;

public class Question : EntityBase
{
    public string Content { get; set; } = default!;

    public Guid TestId { get; set; }
    public Test Test { get; set; } = default!;

    public IEnumerable<Answer> Answers { get; set; } = new List<Answer>();
}
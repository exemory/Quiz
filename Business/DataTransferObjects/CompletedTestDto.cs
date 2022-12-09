namespace Business.DataTransferObjects;

public class CompletedTestDto
{
    public IEnumerable<CompletedQuestion> CompletedQuestions { get; set; } = new List<CompletedQuestion>();
}
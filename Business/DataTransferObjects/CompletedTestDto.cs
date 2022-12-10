namespace Business.DataTransferObjects;

public class CompletedTestDto
{
    public IEnumerable<CompletedQuestionDto> CompletedQuestions { get; set; } = new List<CompletedQuestionDto>();
}
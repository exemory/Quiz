using Business.DataTransferObjects;
using FluentValidation;

namespace Business.Validators;

public class CompletedTestDtoValidator : AbstractValidator<CompletedTestDto>
{
    public CompletedTestDtoValidator()
    {
        RuleForEach(d => d.CompletedQuestions)
            .NotNull()
            .SetValidator(new CompletedQuestionDtoValidator());
    }
}
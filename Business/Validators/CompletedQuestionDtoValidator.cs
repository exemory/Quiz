using Business.DataTransferObjects;
using FluentValidation;

namespace Business.Validators;

public class CompletedQuestionDtoValidator : AbstractValidator<CompletedQuestionDto>
{
    public CompletedQuestionDtoValidator()
    {
        RuleFor(d => d.QuestionId)
            .NotEmpty();

        RuleFor(d => d.SelectedAnswerId)
            .NotEmpty();
    }
}
using SSDB.Application.Features.Students.Commands.AddEdit;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SSDB.Application.Validators.Features.Students.Commands.AddEdit
{
    public class AddEditStudentCommandValidator : AbstractValidator<AddEditStudentCommand>
    {
        public AddEditStudentCommandValidator(IStringLocalizer<AddEditStudentCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Barcode is required!"]);
            RuleFor(request => request.Description)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Description is required!"]);
            RuleFor(request => request.UniversityId)
                .GreaterThan(0).WithMessage(x => localizer["University is required!"]);
            RuleFor(request => request.Rate)
                .GreaterThan(0).WithMessage(x => localizer["Rate must be greater than 0"]);
        }
    }
}
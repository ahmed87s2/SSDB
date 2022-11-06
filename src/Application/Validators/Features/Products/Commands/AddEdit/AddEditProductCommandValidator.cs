using SSDB.Application.Features.Students.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SSDB.Application.Validators.Features.Students.Commands.AddEdit
{
    public class AddEditStudentCommandValidator : AbstractValidator<AddEditStudentCommand>
    {
        public AddEditStudentCommandValidator(IStringLocalizer<AddEditStudentCommandValidator> localizer)
        {
            RuleFor(request => request.NameA)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
            RuleFor(request => request.NameA)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Barcode is required!"]);
            RuleFor(request => request.NameE)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Description is required!"]);
            RuleFor(request => request.FucultyId)
                .GreaterThan(0).WithMessage(x => localizer["University is required!"]);
            RuleFor(request => request.MedicalFees)
                .GreaterThan(0).WithMessage(x => localizer["Rate must be greater than 0"]);
        }
    }
}
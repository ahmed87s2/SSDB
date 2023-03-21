using SSDB.Application.Features.Students.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SSDB.Application.Validators.Features.Students.Commands.AddEdit
{
    public class AddEditStudentCommandValidator : AbstractValidator<AddEditStudentCommand>
    {
        public AddEditStudentCommandValidator(IStringLocalizer<AddEditStudentCommandValidator> localizer)
        {
            RuleFor(request => request.FirstNameA)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["FirstName Ar is required!"]);
            RuleFor(request => request.SecondNameA)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["SecondName Ar is required!"]);
            RuleFor(request => request.ThirdNameA)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["ThirdName Ar is required!"]);
            RuleFor(request => request.FourthNameA)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["FourthName Ar is required!"]);

            RuleFor(request => request.FirstNameE)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["FirstName En is required!"]);
            RuleFor(request => request.SecondNameE)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["SecondName En is required!"]);
            RuleFor(request => request.ThirdNameE)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["ThirdName En is required!"]);
            RuleFor(request => request.FourthNameE)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["FourthName En is required!"]);


            RuleFor(request => request.Gender)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Gender is required!"]);
            RuleFor(request => request.AddmissionNo)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["AddmissionNo is required "]);
        }
    }
}
using SSDB.Application.Features.Universities.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SSDB.Application.Validators.Features.Universities.Commands.AddEdit
{
    public class AddEditUniversityCommandValidator : AbstractValidator<AddEditUniversityCommand>
    {
        public AddEditUniversityCommandValidator(IStringLocalizer<AddEditUniversityCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
            RuleFor(request => request.Type)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Description is required!"]);
        }
    }
}
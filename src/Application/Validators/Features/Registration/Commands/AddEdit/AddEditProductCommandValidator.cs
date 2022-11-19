using SSDB.Application.Features.Registrations.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SSDB.Application.Validators.Features.Registrations.Commands.AddEdit
{
    public class AddEditRegistrationCommandValidator : AbstractValidator<AddRegistrationCommand>
    {
        public AddEditRegistrationCommandValidator(IStringLocalizer<AddEditRegistrationCommandValidator> localizer)
        {
            RuleFor(request => request.StudentId)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Student is required!"]);
            RuleFor(request => request.PaymentNo)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["PaymentNo is required!"]);
            RuleFor(request => request.SemesterId)
                .Must(x => x!=0).WithMessage(x => localizer["Semester is required!"]);
            RuleFor(request => request.CurrencyId)
                 .Must(x => x != 0).WithMessage(x => localizer["Currency is required!"]);
            RuleFor(request => request.Fees)
                .Must(x => x != default).WithMessage(x => localizer["Fees is required!"]);
            RuleFor(request => request.StudyFees)
                .Must(x => x != 0).WithMessage(x => localizer["StudyFees is required!"]);
        }
    }
}
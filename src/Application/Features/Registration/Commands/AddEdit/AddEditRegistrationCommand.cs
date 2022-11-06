using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SSDB.Application.Interfaces.Repositories;
using SSDB.Application.Interfaces.Services;
using SSDB.Application.Requests;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace SSDB.Application.Features.Registrations.Commands
{
    public partial class AddEditRegistrationCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string StudentId { get; set; }
        [Required]
        public int SemesterId { get; set; }
        public int Status { get; set; }
        public double Fees { get; set; }
        public double StudyFees { get; set; }
        public bool NoStudyFees { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        [Required]
        public string PaymentNo { get; set; }
        public int BranchId { get; set; }
        public int linkNo { get; set; }
        public string Comments { get; set; }
    }

    internal class AddEditRegistrationCommandHandler : IRequestHandler<AddEditRegistrationCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<AddEditRegistrationCommandHandler> _localizer;

        public AddEditRegistrationCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditRegistrationCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditRegistrationCommand command, CancellationToken cancellationToken)
        {
            
            if (command.Id == default)
            {
                var Registration = _mapper.Map<Registration>(command);
                await _unitOfWork.Repository<Registration>().AddAsync(Registration);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(Registration.Id, _localizer["Registration Added"]);
            }
            else
            {
                var Registration = await _unitOfWork.Repository<Registration>().GetByIdAsync(command.Id);
                if (Registration != null)
                {
                    var mappedRegistration = _mapper.Map(command, Registration);
                    await _unitOfWork.Repository<Registration>().UpdateAsync(Registration);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(Registration.Id, _localizer["Registration Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Registration Not Found!"]);
                }
            }
        }
    }
}
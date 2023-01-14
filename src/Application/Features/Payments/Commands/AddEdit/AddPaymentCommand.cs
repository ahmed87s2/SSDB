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
using static SSDB.Shared.Constants.Permission.Permissions;

namespace SSDB.Application.Features.Payments.Commands
{
    public partial class AddPaymentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public double RegistrationFees { get; set; }
        public string CurrencyName { get; set; }
        public bool NoStudyFees { get; set; }
        public string Note { get; set; }
        public string FucultyName { get; set; }
        public string Semester { get; set; }
        public string PaymentNo { get; set; }
        public string Status { get; set; }
        public int UniversityId { get; set; }
    }

    internal class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUnitOfWork<string> _studentUnitOfWork;
        private readonly IStringLocalizer<AddPaymentCommandHandler> _localizer;

        public AddPaymentCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddPaymentCommandHandler> localizer, IUnitOfWork<string> studentUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _studentUnitOfWork = studentUnitOfWork;
        }

        public async Task<Result<int>> Handle(AddPaymentCommand command, CancellationToken cancellationToken)
        {
            var payment = _mapper.Map<Payment>(command);
            payment.Date = DateTime.Now;
            await _unitOfWork.Repository<Payment>().AddAsync(payment);

            var student = await _studentUnitOfWork.Repository<Student>().Entities
                .FirstOrDefaultAsync(x=>x.Id == command.StudentNumber && x.UniversityId == command.UniversityId);
            student.Status = "D";
            if(student.Panalty > 0)
            {
                student.Comments += $" ,{student.Panalty} Pannelty on [{DateTime.Now}]";
                student.Panalty = 0;
            }

            await _studentUnitOfWork.Repository<Student>().UpdateAsync(student);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(payment.Id, _localizer["Payment Added"]);
        }
    }
}
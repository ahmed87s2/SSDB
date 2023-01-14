﻿using System.ComponentModel.DataAnnotations;
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

namespace SSDB.Application.Features.RegistrationInfo
{
    public partial class AddRegistrationInfoCommand : IRequest<Result<int>>
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

    internal class AddRegistrationInfoCommandHandler : IRequestHandler<AddRegistrationInfoCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUnitOfWork<string> _studentUnitOfWork;
        private readonly IStringLocalizer<AddRegistrationInfoCommandHandler> _localizer;

        public AddRegistrationInfoCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddRegistrationInfoCommandHandler> localizer, IUnitOfWork<string> studentUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _studentUnitOfWork = studentUnitOfWork;
        }

        public async Task<Result<int>> Handle(AddRegistrationInfoCommand command, CancellationToken cancellationToken)
        {

            if (command.Id == default)
            {
                var registrationInfo = _mapper.Map<StudentsRegistrationInfo>(command);
                await _unitOfWork.Repository<StudentsRegistrationInfo>().AddAsync(registrationInfo);


                var student = await _studentUnitOfWork.Repository<Student>().GetByIdAsync(command.StudentId);
                student.Status = "D";
                student.Comments += $" ,{student.Panalty} Pannelty on [{DateTime.Now}]";
                student.Panalty = 0;
                await _studentUnitOfWork.Repository<Student>().UpdateAsync(student);
                
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(registrationInfo.Id, _localizer["Registration Added"]);
            }

            return await Result<int>.FailAsync(_localizer["UnAuthorized to update registration"]);

        }
    }

}
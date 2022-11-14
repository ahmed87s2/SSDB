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

namespace SSDB.Application.Features.Students.Commands
{
    public partial class ChangeStudentStatusCommand : IRequest<Result<string>>
    {
        public string Id { get; set; }
        public string Status { get; set; }
    }

    internal class ChangeStudentStatusCommandHandler : IRequestHandler<ChangeStudentStatusCommand, Result<string>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<string> _unitOfWork;
        private readonly IStringLocalizer<ChangeStudentStatusCommandHandler> _localizer;

        public ChangeStudentStatusCommandHandler(IUnitOfWork<string> unitOfWork, IMapper mapper, IStringLocalizer<ChangeStudentStatusCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ChangeStudentStatusCommand command, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.Repository<Student>().GetByIdAsync(command.Id);
            if (student == null)
            {
                return await Result<string>.FailAsync(_localizer[$"Cant find Student with id: {command.Id}"]);
            }

            student.Status = command.Status;
            await _unitOfWork.Repository<Student>().UpdateAsync(student);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<string>.SuccessAsync(student.Id, _localizer["Student Updated"]);

        }
    }
}
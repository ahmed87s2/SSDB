using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using SSDB.Application.Interfaces.Repositories;

namespace SSDB.Application.Features.Students.Commands
{
    public class DeleteStudentCommand : IRequest<Result<string>>
    {
        public string StudentNumber { get; set; }
    }

    internal class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Result<string>>
    {
        private readonly IUnitOfWork<string> _unitOfWork;
        private readonly IStringLocalizer<DeleteStudentCommandHandler> _localizer;

        public DeleteStudentCommandHandler(IUnitOfWork<string> unitOfWork, IStringLocalizer<DeleteStudentCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(DeleteStudentCommand command, CancellationToken cancellationToken)
        {
            var Student = await _unitOfWork.Repository<Student>().GetByIdAsync(command.StudentNumber);
            if (Student != null)
            {
                await _unitOfWork.Repository<Student>().DeleteAsync(Student);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<string>.SuccessAsync(Student.Id, _localizer["Student Deleted"]);
            }
            else
            {
                return await Result<string>.FailAsync(_localizer["Student Not Found!"]);
            }
        }
    }
}
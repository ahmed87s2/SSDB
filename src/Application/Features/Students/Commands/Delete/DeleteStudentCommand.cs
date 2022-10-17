using SSDB.Application.Interfaces.Repositories;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace SSDB.Application.Features.Students.Commands.Delete
{
    public class DeleteStudentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<DeleteStudentCommandHandler> _localizer;

        public DeleteStudentCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteStudentCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteStudentCommand command, CancellationToken cancellationToken)
        {
            var Student = await _unitOfWork.Repository<Student>().GetByIdAsync(command.Id);
            if (Student != null)
            {
                await _unitOfWork.Repository<Student>().DeleteAsync(Student);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(Student.Id, _localizer["Student Deleted"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Student Not Found!"]);
            }
        }
    }
}
using SSDB.Application.Interfaces.Repositories;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using SSDB.Shared.Constants.Application;

namespace SSDB.Application.Features.Universities.Commands.Delete
{
    public class DeleteUniversityCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteUniversityCommandHandler : IRequestHandler<DeleteUniversityCommand, Result<int>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStringLocalizer<DeleteUniversityCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteUniversityCommandHandler(IUnitOfWork<int> unitOfWork, IStudentRepository studentRepository, IStringLocalizer<DeleteUniversityCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = studentRepository;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteUniversityCommand command, CancellationToken cancellationToken)
        {
            var isUniversityUsed = await _studentRepository.IsUniversityUsed(command.Id);
            if (!isUniversityUsed)
            {
                var University = await _unitOfWork.Repository<University>().GetByIdAsync(command.Id);
                if (University != null)
                {
                    await _unitOfWork.Repository<University>().DeleteAsync(University);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllUniversitiesCacheKey);
                    return await Result<int>.SuccessAsync(University.Id, _localizer["University Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["University Not Found!"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}
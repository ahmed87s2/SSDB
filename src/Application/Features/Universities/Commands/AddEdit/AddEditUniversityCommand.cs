using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SSDB.Application.Interfaces.Repositories;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using SSDB.Shared.Constants.Application;

namespace SSDB.Application.Features.Universities.Commands
{
    public partial class AddEditUniversityCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }

    internal class AddEditUniversityCommandHandler : IRequestHandler<AddEditUniversityCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditUniversityCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditUniversityCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditUniversityCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditUniversityCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var University = _mapper.Map<University>(command);
                await _unitOfWork.Repository<University>().AddAsync(University);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllUniversitiesCacheKey);
                return await Result<int>.SuccessAsync(University.Id, _localizer["University Saved"]);
            }
            else
            {
                var University = await _unitOfWork.Repository<University>().GetByIdAsync(command.Id);
                if (University != null)
                {
                    University.Name = command.Name;
                    University.IsActive = command.IsActive;
                    University.Type = command.Type;
                    await _unitOfWork.Repository<University>().UpdateAsync(University);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllUniversitiesCacheKey);
                    return await Result<int>.SuccessAsync(University.Id, _localizer["University Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["University Not Found!"]);
                }
            }
        }
    }
}
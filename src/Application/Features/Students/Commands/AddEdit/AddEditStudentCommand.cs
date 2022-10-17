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

namespace SSDB.Application.Features.Students.Commands.AddEdit
{
    public partial class AddEditStudentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Barcode { get; set; }
        [Required]
        public string Description { get; set; }
        public string ImageDataURL { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int UniversityId { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }

    internal class AddEditStudentCommandHandler : IRequestHandler<AddEditStudentCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<AddEditStudentCommandHandler> _localizer;

        public AddEditStudentCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IUploadService uploadService, IStringLocalizer<AddEditStudentCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditStudentCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<Student>().Entities.Where(p => p.Id != command.Id)
                .AnyAsync(p => p.Batch.Id == command.Barcode, cancellationToken))
            {
                return await Result<int>.FailAsync(_localizer["Barcode already exists."]);
            }

            var uploadRequest = command.UploadRequest;
            if (uploadRequest != null)
            {
                uploadRequest.FileName = $"P-{command.Barcode}{uploadRequest.Extension}";
            }

            if (command.Id == 0)
            {
                var student = _mapper.Map<Student>(command);
                if (uploadRequest != null)
                {
                    student.Std_Picture = _uploadService.UploadAsync(uploadRequest);
                }
                await _unitOfWork.Repository<Student>().AddAsync(student);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(student.Id, _localizer["Student Saved"]);
            }
            else
            {
                var student = await _unitOfWork.Repository<Student>().GetByIdAsync(command.Id);
                if (student != null)
                {
                    student.NameA = command.Name ?? student.NameA;
                    student.Comments = command.Description ?? student.Comments;
                    if (uploadRequest != null)
                    {
                        student.Std_Picture = _uploadService.UploadAsync(uploadRequest);
                    }
                    student.Registration.StudyFees = (command.Rate == 0) ? student.Registration.StudyFees : command.Rate;
                    student.Addmission.Id = (command.UniversityId == 0) ? student.Addmission.Id : command.UniversityId;
                    await _unitOfWork.Repository<Student>().UpdateAsync(student);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(student.Id, _localizer["Student Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Student Not Found!"]);
                }
            }
        }
    }
}
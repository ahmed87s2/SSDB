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
    public partial class AddEditStudentCommand : IRequest<Result<string>>
    {
        public string Id { get; set; }
        [Required]
        public string NameA { get; set; }
        [Required]
        public string NameE { get; set; }
        [Required]
        public int BatchId { get; set; }
        public string Phone { get; set; }
        [Required]
        public double MedicalFees { get; set; }
        [Required]
        public int FucultyId { get; set; }
        public int DepartmentId { get; set; }
        public int ProgramId { get; set; }
        public int AddmissionId { get; set; }
        public string AddmissionFormNo { get; set; }
        public int First_semster { get; set; }
        public int NationalityId { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string CertificateType { get; set; }
        public string Std_Picture { get; set; }
        public int SpecializationId { get; set; }
        public DateTime GraduationDate { get; set; }
        public int RegistrationId { get; set; }
        public int StudentStatus { get; set; }
        public int StdPassword { get; set; }
        public int NoStudyFees { get; set; }
        public int CurrencyId { get; set; }
        public string Comments { get; set; }
        public decimal AdvisorId { get; set; }
        public string Record_Status { get; set; }
        public string RegType { get; set; }
        public decimal CGPA { get; set; }
        public string Status { get; set; }
        public int SemesterId { get; set; }
        public decimal RegistrationFees { get; set; }
        public int Panalty { get; set; }
        public decimal ToLocalCurrency { get; set; }
        public decimal StudyFeesUpdated { get; set; }
    }

    internal class AddEditStudentCommandHandler : IRequestHandler<AddEditStudentCommand, Result<string>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<string> _unitOfWork;
        private readonly IUploadService _uploadService;
        private readonly ICurrentUserService _currentUser;
        private readonly IStringLocalizer<AddEditStudentCommandHandler> _localizer;

        public AddEditStudentCommandHandler(IUnitOfWork<string> unitOfWork, IMapper mapper, IUploadService uploadService, IStringLocalizer<AddEditStudentCommandHandler> localizer, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _currentUser = currentUser;
        }

        public async Task<Result<string>> Handle(AddEditStudentCommand command, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.Repository<Student>().GetByIdAsync(command.Id);

            if (student==null)
            {
                var mappedStudent = _mapper.Map<Student>(command);
                mappedStudent.UniversityId = int.Parse(_currentUser.UniversityId);
                await _unitOfWork.Repository<Student>().AddAsync(mappedStudent);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<string>.SuccessAsync(mappedStudent.Id, _localizer["Student Added"]);
            }
            else
            {
                var mappedStudent = _mapper.Map(command, student);
                mappedStudent.UniversityId = int.Parse(_currentUser.UniversityId);
                await _unitOfWork.Repository<Student>().UpdateAsync(mappedStudent);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<string>.SuccessAsync(student.Id, _localizer["Student Updated"]);
            }
        }
    }
}
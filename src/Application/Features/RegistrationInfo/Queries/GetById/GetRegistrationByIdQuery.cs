using SSDB.Application.Extensions;
using SSDB.Application.Interfaces.Repositories;
using SSDB.Application.Specifications.Catalog;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SSDB.Domain.Enums;

namespace SSDB.Application.Features.RegistrationInfo
{
    public class GetRegistrationInfoByIdQuery : IRequest<Result<GetRegistrationInfoByIdResponse>>
    {
        public int UniversityId { get; set; }
        public string StudentId { get; set; }
    }

    internal class GetRegistrationInfoByIdQueryHandler : IRequestHandler<GetRegistrationInfoByIdQuery, Result<GetRegistrationInfoByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUnitOfWork<string> _studentsUnitOfWork;
        private readonly IMapper _mapper;
        public GetRegistrationInfoByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IUnitOfWork<string> studentsUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _studentsUnitOfWork = studentsUnitOfWork;
        }

        public async Task<Result<GetRegistrationInfoByIdResponse>> Handle(GetRegistrationInfoByIdQuery request, CancellationToken cancellationToken)
        {
            var university = await _unitOfWork.Repository<University>().GetByIdAsync(request.UniversityId);
            if(university.Type.ToLower() == UniversityType.Inhouse.ToString().ToLower())
            {
                var student = await _studentsUnitOfWork.Repository<Student>().Entities
                    .Include(x=>x.Currency)
                    .Include(x=>x.Fuculty)
                    .Include(x=>x.Semester)
                .FirstOrDefaultAsync(x => x.UniversityId == request.UniversityId && x.Id == request.StudentId);
                var result = _mapper.Map<GetRegistrationInfoByIdResponse>(student);
                return await Result<GetRegistrationInfoByIdResponse>.SuccessAsync(result);
            }

            var data = await _unitOfWork.Repository<StudentsRegistrationInfo>().Entities
                .FirstOrDefaultAsync(x=>x.UniversityId == request.UniversityId && x.StudentNumber == request.StudentId);
            var mappedData = _mapper.Map<GetRegistrationInfoByIdResponse>(data);
            return await Result< GetRegistrationInfoByIdResponse>.SuccessAsync(mappedData);
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SSDB.Application.Features.Students.Queries;
using SSDB.Application.Interfaces.Repositories;
using SSDB.Application.Models;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSDB.Application.Features.Students.Queries
{
    public class GetStudentRegistrationInfoQuery : IRequest<GetStudentRegistrationInfoByIdResponse>
    {
        public string StudentNumber { get; set; }
    }
    internal class GetStudentRegistrationInfoQueryHandler : IRequestHandler<GetStudentRegistrationInfoQuery, GetStudentRegistrationInfoByIdResponse>
    {
        private readonly IUnitOfWork<string> _unitOfWork;
        private readonly IMapper _mapper;

        public GetStudentRegistrationInfoQueryHandler(IUnitOfWork<string> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetStudentRegistrationInfoByIdResponse> Handle(GetStudentRegistrationInfoQuery request, CancellationToken cancellationToken)
        {
            var studetns = await _unitOfWork.Repository<Student>().Entities
                .Include(x => x.Currency)
                .Include(x => x.Fuculty)
                .Include(x => x.Semester)
                .Include(x => x.Batch)
                .Include(x => x.Department)
                .Where(x => x.Id == request.StudentNumber && (x.Status == "U" || x.Status == "N"))
                .FirstOrDefaultAsync();

            var response = _mapper.Map<GetStudentRegistrationInfoByIdResponse>(studetns);
            if (response == null)
            {
                response.Respond = false;
                response.Respond_id = 404;
                response.Message = "Not Found";
                return response;
            }

            response.Respond = true;
            response.Respond_id = 200;
            return response;
        }
    }
}

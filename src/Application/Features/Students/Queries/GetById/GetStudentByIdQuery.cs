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

namespace SSDB.Application.Features.Students.Queries
{
    public class GetStudentByIdQuery : IRequest<Result<GetStudentByIdResponse>>
    {
        public string Number { get; set; }
    }

    internal class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Result<GetStudentByIdResponse>>
    {
        private readonly IUnitOfWork<string> _unitOfWork;
        private readonly IMapper _mapper;
        public GetStudentByIdQueryHandler(IUnitOfWork<string> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetStudentByIdResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repository<Student>().GetByIdAsync(request.Number);
            var mappedData = _mapper.Map<GetStudentByIdResponse>(data);
            return await Result< GetStudentByIdResponse>.SuccessAsync(mappedData);
        }
    }
}
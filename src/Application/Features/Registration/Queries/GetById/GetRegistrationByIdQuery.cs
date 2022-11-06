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

namespace SSDB.Application.Features.Registrations.Queries
{
    public class GetRegistrationByIdQuery : IRequest<Result<GetRegistrationByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetRegistrationByIdQueryHandler : IRequestHandler<GetRegistrationByIdQuery, Result<GetRegistrationByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        public GetRegistrationByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetRegistrationByIdResponse>> Handle(GetRegistrationByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repository<Registration>().GetByIdAsync(request.Id);
            var mappedData = _mapper.Map<GetRegistrationByIdResponse>(data);
            return await Result< GetRegistrationByIdResponse>.SuccessAsync(mappedData);
        }
    }
}
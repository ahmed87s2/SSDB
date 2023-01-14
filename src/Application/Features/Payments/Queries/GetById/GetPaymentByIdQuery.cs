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

namespace SSDB.Application.Features.Payments.Queries
{
    public class GetPaymentByIdQuery : IRequest<Result<GetPaymentByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Result<GetPaymentByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        public GetPaymentByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetPaymentByIdResponse>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repository<Payment>().GetByIdAsync(request.Id);
            var mappedData = _mapper.Map<GetPaymentByIdResponse>(data);
            return await Result< GetPaymentByIdResponse>.SuccessAsync(mappedData);
        }
    }
}
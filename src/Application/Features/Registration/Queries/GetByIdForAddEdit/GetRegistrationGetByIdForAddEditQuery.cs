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
using SSDB.Application.Features.Registrations.Commands;

namespace SSDB.Application.Features.Registrations.Queries
{
    public class GetRegistrationGetByIdForAddEditQuery : IRequest<Result<AddRegistrationCommand>>
    {
        public int Id { get; set; }
    }

    internal class  GetRegistrationGetByIdForAddEditQueryHandler : IRequestHandler<GetRegistrationGetByIdForAddEditQuery, Result<AddRegistrationCommand>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        public  GetRegistrationGetByIdForAddEditQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AddRegistrationCommand>> Handle(GetRegistrationGetByIdForAddEditQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repository<Registration>().GetByIdAsync(request.Id);
            var mappedData = _mapper.Map<AddRegistrationCommand>(data);
            return  await Result<AddRegistrationCommand>.SuccessAsync(mappedData);
        }
    }
}
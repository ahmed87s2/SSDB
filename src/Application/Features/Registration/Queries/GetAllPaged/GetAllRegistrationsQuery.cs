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
    public class GetAllPagedRegistrationsQuery : IRequest<PaginatedResult<GetAllPagedRegistrationsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetAllPagedRegistrationsQuery(int pageNumber, int pageSize, string searchString, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }
    }

    internal class GetAllRegistrationsQueryHandler : IRequestHandler<GetAllPagedRegistrationsQuery, PaginatedResult<GetAllPagedRegistrationsResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllRegistrationsQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetAllPagedRegistrationsResponse>> Handle(GetAllPagedRegistrationsQuery request, CancellationToken cancellationToken)
        {
            var RegistrationFilterSpec = new RegistrationFilterSpecification(request.SearchString);
            if (request.OrderBy?.Any() != true)
            {
                var data = await _unitOfWork.Repository<Registration>().Entities
                    .Include(x=>x.Student)
                    .Include(x=>x.Semester)
                    .Include(x=>x.Currency)
                   .Specify(RegistrationFilterSpec)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                
                var mappedData = _mapper.Map<PaginatedResult<GetAllPagedRegistrationsResponse>>(data);
                return mappedData;
            }
            else
            {
                var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                var data = await _unitOfWork.Repository<Registration>().Entities
                   .Specify(RegistrationFilterSpec)
                   .OrderBy(ordering)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);

                var mappedData = _mapper.Map<PaginatedResult<GetAllPagedRegistrationsResponse>>(data);
                return mappedData;

            }
        }
    }
}
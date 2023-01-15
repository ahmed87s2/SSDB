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
using SSDB.Application.Interfaces.Services.Identity;
using SSDB.Application.Interfaces.Services;

namespace SSDB.Application.Features.Payments.Queries
{
    public class GetAllPagedPaymentsQuery : IRequest<PaginatedResult<GetAllPagedPaymentsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetAllPagedPaymentsQuery(int pageNumber, int pageSize, string searchString, string orderBy)
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

    internal class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPagedPaymentsQuery, PaginatedResult<GetAllPagedPaymentsResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserService _userService;
        public GetAllPaymentsQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, ICurrentUserService currentUser, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
            _userService = userService;
        }

        public async Task<PaginatedResult<GetAllPagedPaymentsResponse>> Handle(GetAllPagedPaymentsQuery request, CancellationToken cancellationToken)
        {
            var PaymentFilterSpec = new PaymentFilterSpecification(request.SearchString);
            var isAdmin = await _userService.IsAdminAsync(_currentUser.UserId);
            var expression = getExpression(isAdmin);
            if (request.OrderBy?.Any() != true)
            {
                var data = await _unitOfWork.Repository<Payment>().Entities
                   .Specify(PaymentFilterSpec)
                   .Where(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                
                var mappedData = _mapper.Map<PaginatedResult<GetAllPagedPaymentsResponse>>(data);
                return mappedData;
            }
            else
            {
                var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                var data = await _unitOfWork.Repository<Payment>().Entities
                   .Specify(PaymentFilterSpec)
                   .Where(expression)
                   .OrderBy(ordering)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);

                var mappedData = _mapper.Map<PaginatedResult<GetAllPagedPaymentsResponse>>(data);
                return mappedData;

            }
        }

        private Expression<Func<Payment, bool>> getExpression(bool isAdmin)
        {
            if (!isAdmin)
            {
                return (x => x.UniversityId == int.Parse(_currentUser.UniversityId));
            }
            return (x => x.Id != null);
        }
    }
}
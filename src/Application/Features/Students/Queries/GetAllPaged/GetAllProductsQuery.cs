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

namespace SSDB.Application.Features.Students.Queries.GetAllPaged
{
    public class GetAllStudentsQuery : IRequest<PaginatedResult<GetAllPagedStudentsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetAllStudentsQuery(int pageNumber, int pageSize, string searchString, string orderBy)
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

    internal class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, PaginatedResult<GetAllPagedStudentsResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllStudentsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllPagedStudentsResponse>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetAllPagedStudentsResponse>> expression = e => new GetAllPagedStudentsResponse
            {
                Id = e.CurrencyId,
                Name = e.NameA,
                Description = e.Comments,
                Rate = e.BatchId??0,
                Barcode = e.BatchId??0,
                University = e.University.Name,
                UniversityId = e.CurrencyId
            };
            var studentFilterSpec = new StudentFilterSpecification(request.SearchString);
            if (request.OrderBy?.Any() != true)
            {
                var data = await _unitOfWork.Repository<Student>().Entities
                   .Specify(studentFilterSpec)
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;
            }
            else
            {
                var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                var data = await _unitOfWork.Repository<Student>().Entities
                   .Specify(studentFilterSpec)
                   .OrderBy(ordering) // require system.linq.dynamic.core
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;

            }
        }
    }
}
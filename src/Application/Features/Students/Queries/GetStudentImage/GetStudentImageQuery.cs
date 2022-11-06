using SSDB.Application.Interfaces.Repositories;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SSDB.Application.Features.Students.Queries
{
    public class GetStudentImageQuery : IRequest<Result<string>>
    {
        public string Number { get; set; }

        public GetStudentImageQuery(string studentNumber)
        {
            Number = studentNumber;
        }
    }

    internal class GetStudentImageQueryHandler : IRequestHandler<GetStudentImageQuery, Result<string>>
    {
        private readonly IUnitOfWork<string> _unitOfWork;

        public GetStudentImageQueryHandler(IUnitOfWork<string> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(GetStudentImageQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repository<Student>().Entities.Where(p => p.Id == request.Number).Select(a => a.Std_Picture).FirstOrDefaultAsync(cancellationToken);
            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
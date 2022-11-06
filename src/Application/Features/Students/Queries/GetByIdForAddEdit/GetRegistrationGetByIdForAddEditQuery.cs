using SSDB.Application.Interfaces.Repositories;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using SSDB.Application.Features.Students.Commands;

namespace SSDB.Application.Features.Students.Queries
{
    public class GetStudentGetByIdForAddEditQuery : IRequest<Result<AddEditStudentCommand>>
    {
        public string Number { get; set; }
    }

    internal class GetStudentGetByIdQueryHandler : IRequestHandler<GetStudentGetByIdForAddEditQuery, Result<AddEditStudentCommand>>
    {
        private readonly IUnitOfWork<string> _unitOfWork;
        private readonly IMapper _mapper;
        public GetStudentGetByIdQueryHandler(IUnitOfWork<string> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AddEditStudentCommand>> Handle(GetStudentGetByIdForAddEditQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repository<Student>().GetByIdAsync(request.Number);
            var mappedData = _mapper.Map<AddEditStudentCommand>(data);
            return  await Result<AddEditStudentCommand>.SuccessAsync(mappedData);
        }
    }
}
using AutoMapper;
using SSDB.Application.Interfaces.Repositories;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SSDB.Application.Features.Universities.Queries.GetById
{
    public class GetUniversityByIdQuery : IRequest<Result<GetUniversityByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetStudentByIdQueryHandler : IRequestHandler<GetUniversityByIdQuery, Result<GetUniversityByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetStudentByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetUniversityByIdResponse>> Handle(GetUniversityByIdQuery query, CancellationToken cancellationToken)
        {
            var university = await _unitOfWork.Repository<University>().GetByIdAsync(query.Id);
            var mappedUniversity = _mapper.Map<GetUniversityByIdResponse>(university);
            return await Result<GetUniversityByIdResponse>.SuccessAsync(mappedUniversity);
        }
    }
}
using AutoMapper;
using SSDB.Application.Interfaces.Repositories;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Constants.Application;
using SSDB.Shared.Wrapper;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SSDB.Application.Features.Universities.Queries.GetAll
{
    public class GetAllUniversitiesQuery : IRequest<Result<List<GetAllUniversitiesResponse>>>
    {
        public GetAllUniversitiesQuery()
        {
        }
    }

    internal class GetAllUniversitiesCachedQueryHandler : IRequestHandler<GetAllUniversitiesQuery, Result<List<GetAllUniversitiesResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllUniversitiesCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllUniversitiesResponse>>> Handle(GetAllUniversitiesQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<University>>> getAllUniversities = () => _unitOfWork.Repository<University>().GetAllAsync();
            var UniversityList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllUniversitiesCacheKey, getAllUniversities);
            var mappedUniversities = _mapper.Map<List<GetAllUniversitiesResponse>>(UniversityList);
            return await Result<List<GetAllUniversitiesResponse>>.SuccessAsync(mappedUniversities);
        }
    }
}
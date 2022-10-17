using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SSDB.Application.Features.Students.Queries.GetStudentImage;
using SSDB.Application.Interfaces.Repositories;
using SSDB.Application.Models;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSDB.Application.Features.Students.Queries.GetStdForReg
{
    public class GetStdForRegQuery : IRequest<Result<List<StdForReg>>>
    {
    }
    internal class GetStdForRegQueryHandler : IRequestHandler<GetStdForRegQuery, Result<List<StdForReg>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetStdForRegQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<StdForReg>>> Handle(GetStdForRegQuery request, CancellationToken cancellationToken)
        {
            var studetns = await _unitOfWork.Repository<Student>().Entities
                .Include(x => x.Registration)
                .Include(x => x.Currency)
                .Include(x => x.Fuculty)
                .Include(x => x.Semester)
                .ToListAsync();

            var finalList = _mapper.Map<List<StdForReg>>(studetns);

            return await Result<List<StdForReg>>.SuccessAsync(finalList);
        }
    }
}

using SSDB.Application.Features.Universities.Queries.GetAll;
using SSDB.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using SSDB.Application.Features.Universities.Commands;
using SSDB.Application.Features.Utilities.GetDropDownListInfo;
using SSDB.Application.Enums;

namespace SSDB.Client.Infrastructure.Managers.Catalog.Utilities
{
    public interface IUtilitiesManager : IManager
    {
        Task<IResult<List<DropDownListItemResponse>>> GetDropDownListDataAsync(ListType type);

    }
}
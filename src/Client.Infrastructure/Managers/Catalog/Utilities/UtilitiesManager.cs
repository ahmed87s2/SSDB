using SSDB.Application.Features.Universities.Queries.GetAll;
using SSDB.Client.Infrastructure.Extensions;
using SSDB.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SSDB.Application.Features.Universities.Commands;
using SSDB.Application.Features.Utilities.Queries;
using SSDB.Application.Features.Utilities.GetDropDownListInfo;
using SSDB.Application.Enums;

namespace SSDB.Client.Infrastructure.Managers.Catalog.Utilities
{
    public class UtilitiesManager : IUtilitiesManager
    {
        private readonly HttpClient _httpClient;

        public UtilitiesManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<List<DropDownListItemResponse>>> GetDropDownListDataAsync(ListType type)
        {
            var url = Routes.UtilitiesEndpoints.GetAll+ (int)type;
            var response = await _httpClient.GetAsync(url);
            return await response.ToResult<List<DropDownListItemResponse>>();
        }

    }
}
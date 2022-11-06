using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SSDB.Application.Features.Utilities.GetDropDownListInfo;
using SSDB.Application.Enums;
using SSDB.Application.Features.Utilities.Queries;

namespace SSDB.Server.Controllers.v1.Catalog
{
    public class UtilitiesController : BaseApiController<UtilitiesController>
    {
        /// <summary>
        /// Get drop down list data
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [HttpGet(nameof(GetDropdownListData))]
        public async Task<ActionResult<DropDownListItemResponse>> GetDropdownListData(ListType type)
        {
            var data = await _mediator.Send(new GetDropdownListDataQuery() {Type = type});
            return Ok(data);
        }

    }
}
using SSDB.Application.Features.RegistrationInfo.Queries;
using SSDB.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SSDB.Application.Features.RegistrationInfo;

namespace SSDB.Server.Controllers.v1.Catalog
{
    public class RegistrationInfoController : BaseApiController<RegistrationInfoController>
    {
        
        /// <summary>
        /// Get All RegistrationInfo
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.RegistrationInfo.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var RegistrationInfo = await _mediator.Send(new GetAllPagedRegistrationInfoQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(RegistrationInfo);
        }


        /// <summary>
        /// Update students registration info
        /// </summary>
        /// <param name="query">Registration Id</param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.RegistrationInfo.View)]
        [HttpGet(nameof(GetRegistrationInfo))]
        public async Task<ActionResult<GetRegistrationInfoByIdResponse>> GetRegistrationInfo([FromRoute]GetRegistrationInfoByIdQuery query)
        {
            var RegistrationInforesult = await _mediator.Send(query);
            return Ok(RegistrationInforesult);
        }

    }
}
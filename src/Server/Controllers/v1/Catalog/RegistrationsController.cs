using SSDB.Application.Features.Registrations.Commands;
using SSDB.Application.Features.Registrations.Queries;
using SSDB.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SSDB.Server.Controllers.v1.Catalog
{
    public class RegistrationsController : BaseApiController<RegistrationsController>
    {
        
        /// <summary>
        /// Get All Registrations
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Registrations.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var Registrations = await _mediator.Send(new GetAllPagedRegistrationsQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(Registrations);
        }

        /// <summary>
        /// Get Registration Details
        /// </summary>
        /// <param name="id">Registration Id</param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Registrations.View)]
        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            var Registrations = await _mediator.Send(new GetRegistrationByIdQuery() {Id = id });
            return Ok(Registrations);
        }

        /// <summary>
        /// Get Registration Info for Add or Edit
        /// </summary>
        /// <param name="id">Registration Id</param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Registrations.View)]
        [HttpGet(nameof(GetByIdForAddEdit))]
        public async Task<IActionResult> GetByIdForAddEdit(int id)
        {
            var Registrations = await _mediator.Send(new GetRegistrationGetByIdForAddEditQuery() { Id = id });
            return Ok(Registrations);
        }


        /// <summary>
        /// Add/Edit a Registration
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditRegistrationCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Search Registrations and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Registrations.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportRegistrationsQuery(searchString)));
        }
    }
}
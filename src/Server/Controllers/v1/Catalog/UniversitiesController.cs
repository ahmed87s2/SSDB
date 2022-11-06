using SSDB.Application.Features.Universities.Queries.GetAll;
using SSDB.Application.Features.Universities.Queries.GetById;
using SSDB.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SSDB.Application.Features.Universities.Commands;
using SSDB.Application.Features.Universities.Commands.Delete;
using SSDB.Application.Features.Universities.Queries.Export;

namespace SSDB.Server.Controllers.v1.Catalog
{
    public class UniversitiesController : BaseApiController<UniversitiesController>
    {
        /// <summary>
        /// Get All Universities
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Universities.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var universitys = await _mediator.Send(new GetAllUniversitiesQuery());
            return Ok(universitys);
        }

        /// <summary>
        /// Get a University By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Universities.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var university = await _mediator.Send(new GetUniversityByIdQuery() { Id = id });
            return Ok(university);
        }

        /// <summary>
        /// Create/Update a University
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Universities.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditUniversityCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a University
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Universities.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteUniversityCommand { Id = id }));
        }

        /// <summary>
        /// Search Universities and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Universities.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportUniversitiesQuery(searchString)));
        }
    }
}
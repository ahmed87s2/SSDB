using SSDB.Application.Features.Students.Commands.AddEdit;
using SSDB.Application.Features.Students.Commands.Delete;
using SSDB.Application.Features.Students.Queries.Export;
using SSDB.Application.Features.Students.Queries.GetAllPaged;
using SSDB.Application.Features.Students.Queries.GetStudentImage;
using SSDB.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SSDB.Application.Models;
using SSDB.Application.Features.Students.Queries.GetStdForReg;
using System.Collections.Generic;

namespace SSDB.Server.Controllers.v1.Catalog
{
    public class StudentsController : BaseApiController<StudentsController>
    {
        [Authorize(Policy = Permissions.Students.View)]
        [HttpGet("GetStdForReg")]
        public async Task<ActionResult<List<StdForReg>>> GetStdForReg()
        {
            var students = await _mediator.Send(new GetStdForRegQuery());
            return Ok(students);
        }


        /// <summary>
        /// Get All Students
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Students.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var students = await _mediator.Send(new GetAllStudentsQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(students);
        }

        /// <summary>
        /// Get a Student Image by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Students.View)]
        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetStudentImageAsync(int id)
        {
            var result = await _mediator.Send(new GetStudentImageQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Add/Edit a Student
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Students.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditStudentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Student
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK response</returns>
        [Authorize(Policy = Permissions.Students.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteStudentCommand { Id = id }));
        }

        /// <summary>
        /// Search Students and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Students.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportStudentsQuery(searchString)));
        }
    }
}
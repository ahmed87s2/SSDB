using SSDB.Application.Features.Students.Commands;
using SSDB.Application.Features.Students.Queries;
using SSDB.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SSDB.Application.Models;
using System.Collections.Generic;
using SSDB.Application.Features.Registrations.Queries;

namespace SSDB.Server.Controllers.v1.Catalog
{
    public class StudentsController : BaseApiController<StudentsController>
    {
        [AllowAnonymous]
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
        /// Get Student Details
        /// </summary>
        /// <param name="studentNumber">Student Number</param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Students.View)]
        [HttpGet((nameof(GetById)))]
        public async Task<IActionResult> GetById(string studentNumber)
        {
            var Registrations = await _mediator.Send(new GetStudentByIdQuery() { Number = studentNumber });
            return Ok(Registrations);
        }

        /// <summary>
        /// Get Student Info for Add or Edit
        /// </summary>
        /// <param name="studentNumber">Student Number</param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Students.View)]
        [HttpGet(nameof(GetByIdForAddEdit))]
        public async Task<IActionResult> GetByIdForAddEdit(string studentNumber)
        {
            var Registrations = await _mediator.Send(new GetStudentGetByIdForAddEditQuery() { Number = studentNumber });
            return Ok(Registrations);
        }

        /// <summary>
        /// Get a Student Image by Id
        /// </summary>
        /// <param name="studentNumber"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Students.View)]
        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetStudentImageAsync(string studentNumber)
        {
            var result = await _mediator.Send(new GetStudentImageQuery(studentNumber));
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
        /// Change Student Status
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [AllowAnonymous]
        [HttpPost(nameof(ChangeStatus))]
        public async Task<IActionResult> ChangeStatus(ChangeStudentStatusCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Student
        /// </summary>
        /// <param name="studentNumber"></param>
        /// <returns>Status 200 OK response</returns>
        [Authorize(Policy = Permissions.Students.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string studentNumber)
        {
            return Ok(await _mediator.Send(new DeleteStudentCommand { StudentNumber = studentNumber }));
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
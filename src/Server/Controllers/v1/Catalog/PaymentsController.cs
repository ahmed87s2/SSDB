using SSDB.Application.Features.Payments.Commands;
using SSDB.Application.Features.Payments.Queries;
using SSDB.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SSDB.Server.Controllers.v1.Catalog
{
    public class PaymentsController : BaseApiController<PaymentsController>
    {
        
        /// <summary>
        /// Get All Payments
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Payments.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var Payments = await _mediator.Send(new GetAllPagedPaymentsQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(Payments);
        }

        /// <summary>
        /// Get Payment Details
        /// </summary>
        /// <param name="id">Payment Id</param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Payments.View)]
        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            var Payments = await _mediator.Send(new GetPaymentByIdQuery() {Id = id });
            return Ok(Payments);
        }

        /// <summary>
        /// Add/Edit a Payment
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(AddPaymentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Search Payments and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Payments.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportPaymentsQuery(searchString)));
        }
    }
}
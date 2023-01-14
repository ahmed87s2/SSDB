using SSDB.Application.Features.Payments.Commands;
using SSDB.Application.Features.Payments.Queries;
using SSDB.Application.Requests.Catalog;
using SSDB.Shared.Wrapper;
using System.Threading.Tasks;

namespace SSDB.Client.Infrastructure.Managers.Catalog.Payment
{
    public interface IPaymentManager : IManager
    {
        Task<PaginatedResult<GetAllPagedPaymentsResponse>> GetPaymentsAsync(GetAllPagedPaymentsRequest request);

        Task<IResult<int>> SaveAsync(AddPaymentCommand request);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
        Task<IResult<AddPaymentCommand>> GetForAddEdit(int id);
        Task<IResult<GetPaymentByIdResponse>> GetById(int id);
    }
}
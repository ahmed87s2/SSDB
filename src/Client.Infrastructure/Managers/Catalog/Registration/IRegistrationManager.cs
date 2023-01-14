using SSDB.Application.Features.Registrations.Commands;
using SSDB.Application.Features.Registrations.Queries;
using SSDB.Application.Requests.Catalog;
using SSDB.Shared.Wrapper;
using System.Threading.Tasks;

namespace SSDB.Client.Infrastructure.Managers.Catalog.Registration
{
    public interface IRegistrationManager : IManager
    {
        Task<PaginatedResult<GetAllPagedRegistrationsResponse>> GetRegistrationsAsync(GetAllPagedRegistrationsRequest request);

        Task<IResult<int>> SaveAsync(AddRegistrationCommand request);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
        Task<IResult<AddRegistrationCommand>> GetForAddEdit(int id);
        Task<IResult<GetRegistrationByIdResponse>> GetById(int id);
        Task<IResult<string>> UpdateRegistrationAsync(UpdateRegistrationInfoCommand request);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using SSDB.Application.Features.DocumentTypes.Commands;
using SSDB.Application.Features.DocumentTypes.Queries.GetAll;
using SSDB.Shared.Wrapper;

namespace SSDB.Client.Infrastructure.Managers.Misc.DocumentType
{
    public interface IDocumentTypeManager : IManager
    {
        Task<IResult<List<GetAllDocumentTypesResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditDocumentTypeCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}
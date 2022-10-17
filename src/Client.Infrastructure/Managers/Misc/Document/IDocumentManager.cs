using SSDB.Application.Features.Documents.Commands.AddEdit;
using SSDB.Application.Features.Documents.Queries.GetAll;
using SSDB.Application.Requests.Documents;
using SSDB.Shared.Wrapper;
using System.Threading.Tasks;
using SSDB.Application.Features.Documents.Queries.GetById;

namespace SSDB.Client.Infrastructure.Managers.Misc.Document
{
    public interface IDocumentManager : IManager
    {
        Task<PaginatedResult<GetAllDocumentsResponse>> GetAllAsync(GetAllPagedDocumentsRequest request);

        Task<IResult<GetDocumentByIdResponse>> GetByIdAsync(GetDocumentByIdQuery request);

        Task<IResult<int>> SaveAsync(AddEditDocumentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}
using AutoMapper;
using SSDB.Application.Features.DocumentTypes.Commands;
using SSDB.Application.Features.DocumentTypes.Queries.GetAll;
using SSDB.Application.Features.DocumentTypes.Queries.GetById;
using SSDB.Domain.Entities.Misc;

namespace SSDB.Application.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<AddEditDocumentTypeCommand, DocumentType>().ReverseMap();
            CreateMap<GetDocumentTypeByIdResponse, DocumentType>().ReverseMap();
            CreateMap<GetAllDocumentTypesResponse, DocumentType>().ReverseMap();
        }
    }
}
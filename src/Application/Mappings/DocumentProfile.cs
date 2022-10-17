using AutoMapper;
using SSDB.Application.Features.Documents.Commands.AddEdit;
using SSDB.Application.Features.Documents.Queries.GetById;
using SSDB.Domain.Entities.Misc;

namespace SSDB.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<AddEditDocumentCommand, Document>().ReverseMap();
            CreateMap<GetDocumentByIdResponse, Document>().ReverseMap();
        }
    }
}
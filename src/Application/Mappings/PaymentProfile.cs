using AutoMapper;
using SSDB.Application.Features.Payments.Commands;
using SSDB.Application.Features.Payments.Queries;
using SSDB.Application.Features.Students.Queries;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;

namespace SSDB.Application.Mappings
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<AddPaymentCommand, Payment>().ReverseMap();
            CreateMap<GetPaymentByIdResponse, Payment>().ReverseMap();
            CreateMap<GetPaymentByIdResponse, Payment>().ReverseMap();
            CreateMap<GetAllPagedPaymentsResponse, Payment>().ReverseMap();
            CreateMap<GetPaymentByIdResponse, AddPaymentCommand>().ReverseMap();
            CreateMap<PaginatedResult<GetAllPagedPaymentsResponse>, PaginatedResult<Payment>>().ReverseMap();
            

        }
    }
}
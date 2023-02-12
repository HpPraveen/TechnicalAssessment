using AutoMapper;
using Invoicing.Domain;
using Invoicing.Domain.DTO;

namespace Invoicing.API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<InvoiceLine, InvoiceLineDto>().ReverseMap();
        }
    }
}
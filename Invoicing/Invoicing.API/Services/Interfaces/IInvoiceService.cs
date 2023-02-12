using Invoicing.Domain.DTO;

namespace Invoicing.API.Services.Interfaces
{
    public interface IInvoiceService
    {
        object GetInvoiceById(string id);
        object GetInvoices(int pageNumber, int pageSize);
        object CreateUpdateInvoice(InvoiceDto invoiceDto);
        bool DeleteInvoice(string id);
    }
}

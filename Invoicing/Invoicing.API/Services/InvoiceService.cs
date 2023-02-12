using AutoMapper;
using Invoicing.API.Services.Interfaces;
using Invoicing.Domain;
using Invoicing.Domain.DTO;
using Invoicing.Infrastructure;

namespace Invoicing.API.Services
{
    internal class InvoiceService : IInvoiceService
    {
        private readonly IGenericUnitOfWork _genericUnitOfWork;
        private readonly IMapper _mapper;

        public InvoiceService(IGenericUnitOfWork genericUnitOfWork, IMapper mapper)
        {
            _genericUnitOfWork = genericUnitOfWork;
            _mapper = mapper;
        }

        public object GetInvoiceById(string id)
        {
            var invoiceDetails = _genericUnitOfWork.InvoiceRepository.Get(i => i.Id == id, includeProperties: "InvoiceLines").FirstOrDefault();
            return _mapper.Map<InvoiceDto>(invoiceDetails);
        }

        public object GetInvoices(int pageNumber, int pageSize)
        {
            static IOrderedQueryable<Invoice> OrderBy(IQueryable<Invoice> o) => o.OrderByDescending(i => i.SysCreatedOn);

            var invoices = _genericUnitOfWork.InvoiceRepository.GetPaging(
                includeProperties: "InvoiceLines", orderBy: OrderBy, pageNumber: pageNumber, pageSize: pageSize);

            var response = (ResponseDto)invoices;
            var totalInvoices = response.TotalRecords;
            var invoiceDto = _mapper.Map<List<InvoiceDto>>(response.Result);
            var responseResult = PaginationHelper.CreatePageResponse(invoiceDto, pageNumber, pageSize, totalInvoices, null);
            return responseResult;
        }

        public object CreateUpdateInvoice(InvoiceDto invoiceDto)
        {

            var invoice = _mapper.Map<Invoice>(invoiceDto);

            decimal totalAmount = 0;
            foreach (var invoiceLines in invoice.InvoiceLines)
            {
                invoiceLines.LineAmount = invoiceLines.Quantity * invoiceLines.UnitPrice;
                totalAmount += invoiceLines.LineAmount;
            }
            invoice.TotalAmount = totalAmount;

            if (!string.IsNullOrEmpty(invoice.Id))
            {
                invoice.SysUpdatedOn = DateTime.Now;
                _genericUnitOfWork.InvoiceRepository.Update(invoice);
            }
            else
            {
                invoice.Id = Guid.NewGuid().ToString();
                invoice.SysCreatedOn = DateTime.Now;
                _genericUnitOfWork.InvoiceRepository.Insert(invoice);
            }

            _genericUnitOfWork.Commit();
            return _mapper.Map<InvoiceDto>(invoice);
        }

        public bool DeleteInvoice(string id)
        {
            try
            {
                var category = _genericUnitOfWork.InvoiceRepository.Get(c => c.Id == id /*&& c.IsDeleted == false*/).FirstOrDefault();
                if (category == null) return false;

                _genericUnitOfWork.InvoiceRepository.Delete(category);
                _genericUnitOfWork.Commit();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

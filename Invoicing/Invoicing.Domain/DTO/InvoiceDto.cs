namespace Invoicing.Domain.DTO
{
    public class InvoiceDto
    {
        public string? Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public List<InvoiceLineDto> InvoiceLines { get; set; }
    }
}

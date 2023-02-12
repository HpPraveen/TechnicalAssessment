namespace Invoicing.Domain.DTO
{
    public class InvoiceLineDto
    {
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineAmount { get; set; }
    }
}

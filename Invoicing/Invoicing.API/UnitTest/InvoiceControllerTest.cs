using Invoicing.API.Controllers;
using Invoicing.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Invoicing.API.UnitTest
{
    [TestClass]
    public class InvoiceControllerTest
    {
        private readonly InvoiceController _invoiceController;

        public InvoiceControllerTest(InvoiceController invoiceController)
        {
            _invoiceController = invoiceController;
        }

        [TestMethod]
        public void CreateInvoice_ReturnsOkResult()
        {
            var invoice = new InvoiceDto
            {
                Date = DateTime.Now,
                Description = "Sample Invoice",
                TotalAmount = 100,
                InvoiceLines = new List<InvoiceLineDto>
                {
                    new()
                    {
                        Amount = 100,
                        Quantity = 1,
                        UnitPrice = 100,
                        LineAmount = 100
                    }
                }
            };

            var result = _invoiceController.CreateInvoice(invoice);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

    }
}

using Invoicing.API.Services.Interfaces;
using Invoicing.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class InvoiceController : ControllerBase
    {

        private readonly IInvoiceService _invoiceService;
        private ResponseDto _response;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(IInvoiceService invoiceService, ILogger<InvoiceController> logger)
        {
            _invoiceService = invoiceService;
            _response = new ResponseDto();
            _logger = logger;
        }


        [HttpGet]
        [Route("GetInvoiceById/{invoiceId}")]
        public object GetInvoiceById(string invoiceId)
        {
            try
            {
                var result = _invoiceService.GetInvoiceById(invoiceId);

                _response.Result = result;
                _response.DisplayMessage = "Get Invoice by invoiceId - " + invoiceId + " ";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpGet]
        [Route("GetAllInvoices/{pageNumber}/{pageSize}")]
        public object GetInvoices(int pageNumber, int pageSize)
        {
            try
            {
                _response = (ResponseDto)_invoiceService.GetInvoices(pageNumber, pageSize);
                _response.DisplayMessage = _response.Result == null ? "Invoice not found" : "Get Invoice(s)";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPost]
        [Route("CreateInvoice")]
        public object CreateInvoice([FromBody] InvoiceDto invoiceDto)
        {
            try
            {
                var result = _invoiceService.CreateUpdateInvoice(invoiceDto);

                _response.Result = result;
                _response.DisplayMessage = "Invoice is created";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _logger.LogError(ex, "Failed to create invoice");
            }

            return _response;
        }

        [HttpPut]
        [Route("UpdateInvoice")]
        public object UpdateInvoice([FromBody] InvoiceDto invoiceDto)
        {
            try
            {
                var result = _invoiceService.CreateUpdateInvoice(invoiceDto);

                _response.Result = result;
                _response.DisplayMessage = "Invoice is updated";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _logger.LogError(ex, "Failed to update invoice");
            }

            return _response;
        }

        [HttpDelete]
        [Route("DeleteInvoice/{invoiceId}")]
        public object DeleteInvoice(string invoiceId)
        {
            try
            {
                var result = _invoiceService.DeleteInvoice(invoiceId);

                _response.Result = result;
                _response.DisplayMessage = result ? "Invoice is deleted" : "Invoice not found";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Result = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _logger.LogError(ex, "Failed to delete invoice");

            }

            return _response;
        }
    }

}

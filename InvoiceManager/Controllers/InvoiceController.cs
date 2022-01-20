using InvoiceManager.AuthHandler;
using InvoiceManager.Core.EditModels;
using InvoiceManager.Core.Entities;
using InvoiceManager.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceManager.Controllers
{

    [ApiKeyAuth(AuthenticationScheme = ApiKeyAuthConstants.DefaultScheme)]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("getInvoiceById/{id}")]
        public IActionResult GetInvoiceById(int id)
        {
            var invoice = _invoiceService.GetInvoiceById(id);

            if (invoice == null)
                return BadRequest($"It doesn't excist any invoice with this id: {id}.");

            return Ok(invoice);
        }

        [HttpGet("getAllInvoices")]
        public IActionResult GetAllInvoices()
        {
            var invoices = _invoiceService.GetAllInvoices();
            return Ok(invoices);

        }

        [HttpPost("createInvoice")]
        public async Task<IActionResult> CreateInvoice([FromBody] Invoice invoice)
        {
            return Ok(await _invoiceService.CreateInvoice(invoice));
        }

        [HttpPatch("editInvoice")]
        public async Task<IActionResult> EditInvoice(int id, [FromBody] InvoiceEditModel invoiceEditModel)
        {
            var invoice = _invoiceService.GetInvoiceById(id);
            if (invoice == null)
                return BadRequest($"It doesn't excist any invoice with this id: {id}.");

            return Ok(await _invoiceService.EditInvoiceData(id, invoiceEditModel));
        }

        [HttpDelete("deleteInvoice")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = _invoiceService.GetInvoiceById(id);
            if (invoice == null)
                return BadRequest($"It doesn't excist any invoice with this id: {id}.");

            await _invoiceService.DeleteInvoice(id);
            return Ok();
        }

        [HttpPatch("payInvoice")]
        [ProducesResponseType(200, Type = typeof(List<Invoice>))]
        public IActionResult UpdatePaymentStatusInvoice(int invoiceId)
        {
            var invoice = _invoiceService.GetInvoiceById(invoiceId);
            if (invoice == null)
                return BadRequest($"It doesn't excist any invoice with this id: {invoiceId}.");
            
            if (invoice.IsPaid)
                return BadRequest("Invoice is already paid.");

            _invoiceService.UpdatePaymentStatusInvoice(invoiceId);
            return Ok();

        }

        [HttpGet("getUnpaidInvoices")]
        [ProducesResponseType(200, Type = typeof(List<Invoice>))]
        public IActionResult GetUnpaidInvoices()
        {
            return Ok(_invoiceService.GetAllUnpaidInvoices());
        }

        [HttpPatch("addInvoiceItems")]
        [ProducesResponseType(200, Type = typeof(Invoice))]
        public IActionResult AddInvoiceItems(int invoiceId, [FromBody] List<Item> items)
        {
            var invoice = _invoiceService.GetInvoiceById(invoiceId);
            if (invoice == null)
                return BadRequest($"It doesn't excist any invoice with this id: {invoiceId}.");

            return Ok(_invoiceService.AddInvoiceItems(invoiceId, items));
        }

        [HttpPatch("removeInvoiceItems")]
        [ProducesResponseType(200, Type = typeof(Invoice))]
        public IActionResult RemoveInvoiceItems(int invoiceId, [FromQuery] List<int> items)
        {
            var invoice = _invoiceService.GetInvoiceById(invoiceId);
            if (invoice == null)
                return BadRequest($"It doesn't excist any invoice with this id: {invoiceId}.");

            return Ok(_invoiceService.RemoveInvoiceItems(invoiceId, items));
        }

        [HttpPatch("removeInvoiceItem")]
        [ProducesResponseType(200, Type = typeof(Invoice))]
        public IActionResult RemoveInvoiceItem(int invoiceId, int itemId)
        {
            var invoice = _invoiceService.GetInvoiceById(invoiceId);
            if (invoice == null)
                return BadRequest($"It doesn't excist any invoice with this id: {invoiceId}.");

            return Ok(_invoiceService.RemoveInvoiceItem(invoiceId, itemId));
        }
    }
}

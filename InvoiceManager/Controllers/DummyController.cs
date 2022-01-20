using InvoiceManager.Core.Entities;
using InvoiceManager.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SelfRefreshingCache.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManager.Controllers
{
    [Route("api/[controller]")]
    public class DummyController : ControllerBase
    {
        private readonly ILogger<IEnumerable<Invoice>> _logger;
        private readonly IInvoiceService _invoiceService;
        public DummyController(ILogger<IEnumerable<Invoice>> logger, IInvoiceService invoiceService)
        {
            _logger = logger;
            _invoiceService = invoiceService;
        }

        [HttpGet()]
        public IActionResult DummyEndpoint()
        {
            var srcObj = new SelfRefreshingCache<IEnumerable<Invoice>>(_logger, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(50), _invoiceService.GetAllInvoices);
            
            //If called for the first time, it starts automatic refreshes.
            srcObj.GetOrCreate();

            //In reasonable cases, e.g. when the result cannot be obtained, it throws an exception.
            var srcObj2 = new SelfRefreshingCache<IEnumerable<Invoice>>(_logger, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(50), ThrowDummyException);
            srcObj2.GetOrCreate();

            return Ok();
        }

        private IEnumerable<Invoice> ThrowDummyException()
        {
            throw new Exception();
        }
    }
}

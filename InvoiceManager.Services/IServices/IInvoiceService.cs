using InvoiceManager.Core.EditModels;
using InvoiceManager.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceManager.Services.IServices
{
    public interface IInvoiceService
    {
        public Invoice GetInvoiceById(int invoiceId);
        public IEnumerable<Invoice> GetAllInvoices();
        public Task<Invoice> CreateInvoice(Invoice invoice);        
        public Task<Invoice> EditInvoiceData(int invoiceId, InvoiceEditModel invoiceEditModel);
        public Task DeleteInvoice(int invoiceId);
        public Task UpdatePaymentStatusInvoice(int invoiceId);
        public IEnumerable<Invoice> GetAllUnpaidInvoices();
        public Task<Invoice> AddInvoiceItems(int invoiceId, IList<Item> items);
        public Task<Invoice> RemoveInvoiceItems(int invoiceId, IList<int> itemsId);
        public Task<Invoice> RemoveInvoiceItem(int invoiceId, int itemId);
    }
}

using InvoiceManager.Core.EditModels;
using InvoiceManager.Core.Entities;
using InvoiceManager.Data;
using InvoiceManager.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManager.Services.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceManagerAppDbContext _dbContext;

        public InvoiceService(InvoiceManagerAppDbContext dbContext/*, InvoiceRepository invoiceRepository*/)
        {
            _dbContext = dbContext;
        }

        public async Task<Invoice> CreateInvoice(Invoice invoice)
        {
            _dbContext.Add(invoice);
            await _dbContext.SaveChangesAsync();

            return invoice;
        }

        public Invoice GetInvoiceById(int invoiceId)
        {
            return _dbContext.Invoices.Include(i=>i.Items).FirstOrDefault(i => i.Id == invoiceId);
        }

        public async Task<Invoice> EditInvoiceData(int invoiceId, InvoiceEditModel invoiceEditModel)
        {
            var editedInvoice = _dbContext.Invoices.FirstOrDefault(i => i.Id == invoiceId);

            editedInvoice.IsPaid = invoiceEditModel.IsPaid;
            editedInvoice.Description = invoiceEditModel.Description;

            _dbContext.Update(editedInvoice);
            await _dbContext.SaveChangesAsync();

            return editedInvoice;
        }

        public async Task UpdatePaymentStatusInvoice(int invoiceId)
        {
            var payedInvoice = _dbContext.Invoices.FirstOrDefault(i => i.Id == invoiceId);
            payedInvoice.IsPaid = true;

            _dbContext.Update(payedInvoice);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<Invoice> GetAllUnpaidInvoices()
        {
            return _dbContext.Invoices.Include(i => i.Items).Where(i => i.IsPaid == false).ToList();
        }

        public IEnumerable<Invoice> GetAllInvoices()
        {
            return _dbContext.Invoices.Include(i => i.Items).ToList();
        }

        public async Task DeleteInvoice(int invoiceId)
        {
            var invoiceToDelete = _dbContext.Invoices.FirstOrDefault(i => i.Id == invoiceId);
            _dbContext.Remove(invoiceToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Invoice> AddInvoiceItems(int invoiceId, IList<Item> items)
        {
            var editedInvoice = _dbContext.Invoices.Include(i => i.Items).FirstOrDefault(i => i.Id == invoiceId);

            var itemsToAdd = new List<Item>();
            foreach (var item in items)
            {
                itemsToAdd.Add(item);
            }

            editedInvoice.Items = itemsToAdd;
            _dbContext.Update(editedInvoice);
            await _dbContext.SaveChangesAsync();

            return editedInvoice;
        }

        public async Task<Invoice> RemoveInvoiceItems(int invoiceId, IList<int> itemsId)
        {
            var editedInvoice = _dbContext.Invoices.Include(i => i.Items).FirstOrDefault(i => i.Id == invoiceId);
            //var itemsToBeRemoved = editedInvoice.Items.Where(t => itemsId.Contains(t.Id)).ToList();

            foreach (var item in editedInvoice.Items)
            {
                if (itemsId.Contains(item.Id))
                {
                    editedInvoice.Items.Remove(item);
                }
            }


            _dbContext.Update(editedInvoice);
            await _dbContext.SaveChangesAsync();

            return editedInvoice;
        }

        public async Task<Invoice> RemoveInvoiceItem(int invoiceId, int itemId)
        {
            var editedInvoice = _dbContext.Invoices.Include(i => i.Items).FirstOrDefault(i => i.Id == invoiceId);
            var itemToBeRemoved = editedInvoice.Items.FirstOrDefault(i => i.Id == itemId);

            editedInvoice.Items.Remove(itemToBeRemoved);
            _dbContext.Update(editedInvoice);
            await _dbContext.SaveChangesAsync();

            return editedInvoice;

        }
    }
}

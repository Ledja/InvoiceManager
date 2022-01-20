using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InvoiceManager.Core.Entities
{
    public class Invoice
    {
        public Invoice()
        {
            Items = new Collection<Item>();
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsPaid { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}

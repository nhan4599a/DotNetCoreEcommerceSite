using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Product
    {
        public Product()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Decription { get; set; }
        public double Price { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}

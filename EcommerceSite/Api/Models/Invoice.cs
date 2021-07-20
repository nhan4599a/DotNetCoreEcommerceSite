using System;
using System.Collections.Generic;

namespace Api.Models
{
    public class Invoice
    {
        public Invoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime IssuedDate { get; set; }

        public virtual AspNetUser Customer { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
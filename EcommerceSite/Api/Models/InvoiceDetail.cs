using System;

namespace Api.Models
{
    public partial class InvoiceDetail
    {
        public Guid InvoiceId { get; set; }
        public Guid ProductId { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
    }
}

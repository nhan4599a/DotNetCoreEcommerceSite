using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50), Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}

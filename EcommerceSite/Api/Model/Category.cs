using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Model
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50), Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

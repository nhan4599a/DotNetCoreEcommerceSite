using System.ComponentModel.DataAnnotations;

namespace SharedModel
{
    public class SearchInputModel
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Keyword { get; set; }
    }
}
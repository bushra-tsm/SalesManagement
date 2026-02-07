using System.ComponentModel.DataAnnotations;

namespace SalesManagement.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Brand { get; set; }

        public string Category { get; set; }   

        public string SkinType { get; set; }  

        [Required]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}

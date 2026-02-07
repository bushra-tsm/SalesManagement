using System.ComponentModel.DataAnnotations;

namespace SalesManagement.Models
{
    public class SaleViewModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public List<Customer> Customers { get; set; }

        public List<Product> Products { get; set; }
    }
}

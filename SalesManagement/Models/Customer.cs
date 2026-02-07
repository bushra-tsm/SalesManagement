using System.ComponentModel.DataAnnotations;

namespace SalesManagement.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        public string Name { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public string SkinType { get; set; }

    }
}

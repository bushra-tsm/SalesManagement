using Microsoft.AspNetCore.Mvc.Rendering;

namespace SalesManagement.Models
{
    public class InvoiceVM
    {
        public int CustomerId { get; set; }
        public string CustomerEmail { get; set; }

        public List<int> ProductIds { get; set; }
        public List<int> Quantities { get; set; }

        public List<SelectListItem> Customers { get; set; }
        public List<SelectListItem> Products { get; set; }
    }
}

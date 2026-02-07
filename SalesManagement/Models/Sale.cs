namespace SalesManagement.Models
{
    public class Sale
    {
        public int SaleId { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime SaleDate { get; set; }

        public decimal TotalAmount { get; set; }

        public List<SaleDetail> SaleDetails { get; set; }
    }
}

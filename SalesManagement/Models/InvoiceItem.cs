namespace SalesManagement.Models
{
    public class InvoiceItem
    {
        public int InvoiceItemId { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal Total => Price * Quantity;
    }
}

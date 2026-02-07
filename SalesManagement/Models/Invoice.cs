namespace SalesManagement.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public decimal GrandTotal { get; set; }

        public ICollection<InvoiceItem> Items { get; set; }
    }
}

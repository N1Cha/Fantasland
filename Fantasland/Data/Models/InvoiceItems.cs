namespace Data.Models
{
    public class InvoiceItems
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public virtual Product Product { get; set; }

        public virtual Invoices Invoice { get; set; }
    }
}

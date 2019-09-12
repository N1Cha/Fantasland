using System;
using System.Collections.ObjectModel;

namespace Data.Models
{
    public class Invoices
    {
        public int Id { get; set; }

        public string OrganizationCode { get; set; }

        public string InvoiceNumber { get; set; }

        public bool IsOrganization { get; set; }

        public double PaymentAmount { get; set; }

        public DateTime InvoiceDate { get; set; }

        public virtual ObservableCollection<InvoiceItems> Items { get; set; }
    }
}

using Data;
using Data.Models;
using Fantasland.Infrastructure;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace Fantasland.Sales
{
    public class AllSalesViewModel
    {
        public AllSalesViewModel()
        {
            this.AllInvoices = new ObservableCollection<Invoices>();

            using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
            {
                context.Invoices.Include(i => i.Items).Load();
                context.InvoiceItems.Include(x => x.Product).Load();

                this.AllInvoices = new ObservableCollection<Invoices>(context.Invoices.Local);
            }
        }

        public ObservableCollection<Invoices> AllInvoices { get; set; }
    }
}

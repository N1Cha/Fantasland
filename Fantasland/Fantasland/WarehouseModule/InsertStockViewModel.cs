using Data;
using Data.Models;
using Fantasland.Infrastructure;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows.Input;

namespace Fantasland.WarehouseModule
{
    public class InsertStockViewModel : NotifyPropertyChanged
    {
        private Product selectedItem;

        private ICommand addRowCommand;
        private ICommand deleteRowCommand;
        private ICommand insertStockInWerehouse;

        public InsertStockViewModel()
        {
            this.NewStock = new ObservableCollection<Product>();
            this.AllProducts = new ObservableCollection<Product>();
            this.NewStock.Add(new Product());

            using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
            {
                context.Products.Load();
                this.AllProducts = context.Products.Local;
            }
        }

        public ObservableCollection<Product> NewStock { get; set; }

        public ObservableCollection<Product> AllProducts { get; set; }

        public Product SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                this.selectedItem = value;
                NotifyChanged(nameof(SelectedItem));
            }
        }

        public ICommand AddRowCommand
        {
            get { return this.addRowCommand = new Command<object>(OnAddRowCommand); }
        }
         
        public ICommand DeleteRowCommand
        {
            get { return this.deleteRowCommand = new Command<object>(OnDeleteRowCommand); }
        }

        public ICommand InsertStockInWerehouse
        {
            get { return this.insertStockInWerehouse = new Command<object>(OnInsertStockInWerehouse); }
        }

        private void OnAddRowCommand()
        {
            this.NewStock.Add(new Product());
        }

        private void OnDeleteRowCommand()
        {
            if(this.NewStock.Count > 1 && this.SelectedItem != null)
            {
                this.NewStock.Remove(this.SelectedItem);
            }
        }

        private void OnInsertStockInWerehouse()
        {
            if(this.NewStock != null && this.NewStock.Count > 0)
            {

            }
        }
    }
}

using Data.Models;
using Fantasland.Infrastructure;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Fantasland.WarehouseModule
{
    public class InsertStockViewModel : NotifyPropertyChanged
    {
        private Product selectedItem;

        private ICommand addRowCommand;
        private ICommand deleteRowCommand;

        public InsertStockViewModel()
        {
            this.NewStock = new ObservableCollection<Product>();
            this.NewStock.Add(new Product());
        }

        public ObservableCollection<Product> NewStock { get; set; }

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
    }
}

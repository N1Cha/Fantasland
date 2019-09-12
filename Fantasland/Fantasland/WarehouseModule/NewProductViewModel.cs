using Data;
using Data.Models;
using Fantasland.Infrastructure;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Input;

namespace Fantasland.WarehouseModule
{
    public class NewProductViewModel : NotifyPropertyChanged
    {
        private Product newProduct;
        private Category selectedCategory;
        private ICommand saveProductCommand;

        public NewProductViewModel()
        {
            this.NewProduct = new Product();
            this.Categories = new ObservableCollection<Category>();
            using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
            {
                context.Categories.Load();
                this.Categories = context.Categories.Local;
            }
        }

        public Product NewProduct
        {
            get { return this.newProduct; }
            set
            {
                this.newProduct = value;
                this.NotifyChanged(nameof(NewProduct));
            }
        }

        public Category SelectedCategory
        {
            get { return this.selectedCategory; }
            set
            {
                this.selectedCategory = value;
                this.NotifyChanged(nameof(SelectedCategory));
            }
        }

        public ObservableCollection<Category> Categories { get; set; }
        
        public ICommand SaveProductCommand
        {
            get { return this.saveProductCommand = new Command<object>(OnSaveProductCommand); }
        }

        private void OnSaveProductCommand()
        {
            if (this.SelectedCategory != null)
            {
                this.NewProduct.CategoryId = this.SelectedCategory.Id;
            }
            else
            {
                this.NewProduct.CategoryId = 0;
            }

            if (IsNewProductValid())
            {
                using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
                {
                    context.Products.Add(this.NewProduct);
                    context.SaveChanges();
                    MessageBox.Show("The product is added successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Set valid product name, code, price and category!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private bool IsNewProductValid()
        {
            if (!string.IsNullOrWhiteSpace(this.NewProduct.Name) && !string.IsNullOrWhiteSpace(this.NewProduct.Code)
                && this.NewProduct.Price > 0.0 && this.NewProduct.CategoryId != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

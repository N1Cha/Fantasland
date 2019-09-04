using Data.Models;
using System.Collections.ObjectModel;
using Fantasland.Infrastructure;
using System.Data.Entity;
using Data;
using System.Windows.Input;
using System.Windows;
using System.Linq;

namespace Fantasland.WarehouseModule
{
    public class CategoryViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<Category> allCategories;
        private Category newCategory;
        private ICommand addCategoryCommand;

        public CategoryViewModel()
        {
            this.AllCategories = new ObservableCollection<Category>();
            this.NewCategory = new Category();

            using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
            {
                context.Categories.Load();
                this.AllCategories = context.Categories.Local;
            }
        }

        public Category NewCategory
        {
            get { return this.newCategory; }
            set
            {
                this.newCategory = value;
                this.NotifyChanged(nameof(NewCategory));
            }
        }

        public ObservableCollection<Category> AllCategories
        {
            get { return this.allCategories; }
            set
            {
                this.allCategories = value;
                this.NotifyChanged(nameof(AllCategories));
            }
        }

        public ICommand AddCategoryCommand
        {
            get { return this.addCategoryCommand = new Command<object>(OnAddCategoryCommand); }
        }

        private void OnAddCategoryCommand(object data)
        {
            bool isExist = false;

            if(!string.IsNullOrWhiteSpace(this.NewCategory.Name))
            {
                isExist = this.IsNewCategoryAlreadyExist(this.NewCategory);
            }

            if(!isExist)
            {
                using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
                {
                    context.Categories.Add(this.NewCategory);
                    context.SaveChanges();

                    context.Categories.Load();
                    this.AllCategories = new ObservableCollection<Category>(context.Categories.Local.OrderBy(x => x.Id));
                }
            }
            else
            {
                MessageBox.Show("There is already exist category with same name!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool IsNewCategoryAlreadyExist(Category newCategory)
        {
            foreach (Category c in this.AllCategories)
            {
                if (string.Compare(c.Name, this.NewCategory.Name, true) == 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

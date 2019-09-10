using Data;
using Data.Models;
using Fantasland.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Fantasland.WarehouseModule
{
    public class NewCategoryViewModel : NotifyPropertyChanged
    {
        private Category newCategory;
        private ICommand saveCategoryCommand;
        private ObservableCollection<Category> allCategories;
        private bool? dialogResult;

        public NewCategoryViewModel()
        {
            this.NewCategory = new Category();
            this.AllCategories = new ObservableCollection<Category>();
            using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
            {
                context.Categories.Load();
                this.AllCategories = context.Categories.Local;
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

        public bool? DialogResult
        {
            get { return this.dialogResult; }
            set
            {
                this.dialogResult = value;
                this.NotifyChanged(nameof(DialogResult));
            }
        }

        public ICommand SaveCategoryCommand
        {
            get { return this.saveCategoryCommand = new Command<object>(OnSaveCategoryCommand); }
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

        private void OnSaveCategoryCommand()
        {
            bool isExist = false;

            if (!string.IsNullOrWhiteSpace(this.NewCategory.Name))
            {
                isExist = this.IsNewCategoryAlreadyExist(this.NewCategory);
            }

            if (!isExist)
            {
                using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
                {
                    context.Categories.Add(this.NewCategory);
                    context.SaveChanges();
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

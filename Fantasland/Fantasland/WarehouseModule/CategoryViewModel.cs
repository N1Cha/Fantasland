﻿using Data.Models;
using System.Collections.ObjectModel;
using Fantasland.Infrastructure;
using System.Data.Entity;
using Data;
using System.Windows.Input;
using System.Linq;
using Autofac;

namespace Fantasland.WarehouseModule
{
    public class CategoryViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<Category> allCategories;
        private Category selectedCategory;
        private ICommand addCategoryCommand;
        private ICommand deleteCategoryCommand;

        public CategoryViewModel()
        {
            this.AllCategories = new ObservableCollection<Category>();
            this.SelectedCategory = new Category();

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

        public Category SelectedCategory
        {
            get { return this.selectedCategory; }
            set
            {
                this.selectedCategory = value;
                this.NotifyChanged(nameof(SelectedCategory));
            }
        }

        public ICommand AddCategoryCommand
        {
            get { return this.addCategoryCommand = new Command<Category>(OnAddCategoryCommand); }
        }

        public ICommand DeleteCategoryCommand
        {
            get { return this.deleteCategoryCommand = new Command<object>(OnDeleteCategoryCommand); }
        }

        private void OnAddCategoryCommand()
        {
            Bootstraper.Container.Resolve<NewCategoryView>().ShowDialog();
            using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
            {
                context.Categories.Load();
                this.AllCategories = new ObservableCollection<Category>(context.Categories.Local.OrderBy(x => x.Id));
            }
        }


        private void OnDeleteCategoryCommand()
        {
            if (this.SelectedCategory != null)
            {
                using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
                {
                    context.Categories.Load();
                    context.Categories.Local.Remove(context.Categories.Local.First(c => c.Id == this.SelectedCategory.Id));
                    context.SaveChanges();

                    this.AllCategories = new ObservableCollection<Category>(context.Categories.Local.OrderBy(x => x.Id));
                }
            }
        }
    }
}

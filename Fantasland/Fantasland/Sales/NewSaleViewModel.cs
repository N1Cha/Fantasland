﻿using Data;
using Data.Models;
using Fantasland.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Fantasland.Sales
{
    public class NewSaleViewModel : NotifyPropertyChanged
    {
        private bool isOraganization;
        private bool isEnabled;
        private double paymentAmount;
        private string selectedBuyerType;
        private Invoices newInvoice;
        private InvoiceItems selectedItem;
        private ICommand addItemCommand;
        private ICommand deleteItemCommand;
        private ICommand finnishSaleCommand;
        private ICommand paymentCommand;

        public NewSaleViewModel()
        {
            List<Invoices> mountInvoices = new List<Invoices>();
            this.AllProducts = new ObservableCollection<Product>();
            this.SelectedItem = new InvoiceItems();
            this.IsEnabled = true;

            this.BuyerType = new ObservableCollection<string>() { "Customer", "Organization" };
            this.SelectedBuyerType = "Customer";
            this.InvoiceItems = new ObservableCollection<InvoiceItems>() { new InvoiceItems() { Product = new Product() } };

            using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
            {
                context.Invoices.Where(i => i.InvoiceDate.Month == DateTime.Today.Month).Load();
                mountInvoices = context.Invoices.Local.ToList();

                context.Products.Load();
                this.AllProducts = context.Products.Local;
            }

            this.NewInvoice = new Invoices()
            {
                InvoiceDate = DateTime.Now.Date,
                InvoiceNumber = GenerateInvoiceNumber(mountInvoices)
            };
        }

        public ICommand AddItemCommand
        {
            get { return this.addItemCommand = new Command<object>(OnAddItemCommand); }
        }

        public ICommand DeleteItemCommand
        {
            get { return this.deleteItemCommand = new Command<object>(OnDeleteItemCommand); }
        }

        public ICommand FinnishSaleCommand
        {
            get { return this.finnishSaleCommand = new Command<object>(OnFinnishSaleCommand); }
        }

        public ICommand PaymentCommand
        {
            get { return this.paymentCommand = new Command<object>(OnPaymentCommand); }
        }

        public bool IsOraganization
        {
            get { return this.isOraganization; }
            set
            {
                this.isOraganization = value;
                this.NotifyChanged(nameof(IsOraganization));
            }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set
            {
                this.isEnabled = value;
                this.NotifyChanged(nameof(IsEnabled));
            }
        }

        public double PaymentAmount
        {
            get { return this.paymentAmount; }
            set
            {
                this.paymentAmount = value;
                this.NotifyChanged(nameof(PaymentAmount));
            }
        }

        public string SelectedBuyerType
        {
            get { return this.selectedBuyerType; }
            set
            {
                this.selectedBuyerType = value;
                this.NotifyChanged(nameof(SelectedBuyerType));

                this.IsOraganization = this.IsOrganiationSelected(value);
            }
        }

        public Invoices NewInvoice
        {
            get { return this.newInvoice; }
            set
            {
                this.newInvoice = value;
                this.NotifyChanged(nameof(NewInvoice));
            }
        }

        public InvoiceItems SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                this.selectedItem = value;
                this.NotifyChanged(nameof(SelectedItem));
            }
        }

        public ObservableCollection<string> BuyerType { get; set; }

        public ObservableCollection<InvoiceItems> InvoiceItems { get; set; }

        public ObservableCollection<Product> AllProducts { get; set; }

        private bool IsOrganiationSelected(string type)
        {
            if (string.Compare("Organization", type, true) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GenerateInvoiceNumber(List<Invoices> mountInvoices)
        {
            StringBuilder strBuilder = new StringBuilder();
            DateTime date = DateTime.Today;

            strBuilder.Append(date.Year).Append(date.Month).Append(date.Day).Append(".").Append(mountInvoices.Count + 1);
            return strBuilder.ToString();
        }

        private double CalculatePaymentAmount()
        {
            double paymentAmount = 0;

            foreach (InvoiceItems item in this.InvoiceItems)
            {
                if (item.Product != null)
                {
                    paymentAmount += item.Product.Price * item.Quantity;
                }
            }

            return paymentAmount;
        }

        private void OnAddItemCommand()
        {
            this.InvoiceItems.Add(new InvoiceItems() { Product = new Product() });
            this.PaymentAmount = this.CalculatePaymentAmount();
        }

        private void OnDeleteItemCommand()
        {
            if (this.SelectedItem != null && this.InvoiceItems.Count > 1)
            {
                this.InvoiceItems.Remove(this.SelectedItem);
            }
            this.PaymentAmount = this.CalculatePaymentAmount();
        }

        private void OnFinnishSaleCommand()
        {
            if (IsInvoiceValid())
            {
                List<InvoiceItems> outOfStockItems = new List<InvoiceItems>();
                this.NewInvoice.IsOrganization = this.IsOraganization;
                this.NewInvoice.PaymentAmount = this.PaymentAmount;

                using (AppDbContext context = new AppDbContext(Constants.ConnectionString))
                {
                    context.Invoices.Add(this.NewInvoice);
                    context.SaveChanges();

                    context.Products.Load();
                    foreach (InvoiceItems item in this.InvoiceItems)
                    {
                        if(item.Product == null)
                        {
                            continue;
                        }

                        item.ProductId = item.Product.Id;
                        item.InvoiceId = this.NewInvoice.Id;

                        if (context.Products.FirstOrDefault(p => p.Id == item.ProductId).Quantity == 0
                            || context.Products.FirstOrDefault(p => p.Id == item.ProductId).Quantity - item.Quantity < 0)
                        {
                            outOfStockItems.Add(item);
                        }
                        else
                        {
                            context.InvoiceItems.Local.Add(item);
                            context.Products.Local.First(x => string.Compare(x.Name, item.Product.Name, true) == 0).Quantity -= item.Quantity;
                            context.Products.Remove(context.Products.Local.Where(x => string.Compare(x.Name, item.Product.Name, true) == 0).OrderByDescending(x=>x.Quantity).First());
                        }
                    }

                    if (outOfStockItems.Count == 0)
                    {
                        context.SaveChanges();
                        MessageBox.Show("The invoice is created successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.IsEnabled = false;
                    }
                    else
                    {
                        MessageBox.Show(this.ItemsOutOfStockMessage(outOfStockItems), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private void OnPaymentCommand()
        {
            this.PaymentAmount = this.CalculatePaymentAmount();
        }

        private bool IsInvoiceValid()
        {
            if (this.IsOraganization && string.IsNullOrWhiteSpace(this.NewInvoice.OrganizationCode))
            {
                MessageBox.Show("Enter Organization name!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            foreach (InvoiceItems item in this.InvoiceItems)
            {
                if(item.Product == null)
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(item.Product.Name) || item.Quantity == 0)
                {
                    MessageBox.Show("Select Product Name and quantity for every row!", "Warning  ", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            if (this.PaymentAmount <= 0)
            {
                MessageBox.Show("Payment amount must be more than 0!", "Warning  ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private string ItemsOutOfStockMessage(List<InvoiceItems> items)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("The following products are out of stock:");

            foreach (InvoiceItems item in items)
            {
                stringBuilder.AppendLine(item.Product.Name);
            }

            return stringBuilder.ToString();
        }
    }
}

using StoreeTaskk.Commands;
using StoreeTaskk.Models;
using StoreeTaskk.Repositories;
using StoreeTaskk.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreeTaskk.ViewModels
{
    public class AddProductViewModel:BaseViewModel
    {
        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand SelectionChanged { get; set; }

        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; OnPropertyChanged(); }
        }

        private decimal productPrice;

        public decimal ProductPrice
        {
            get { return productPrice; }
            set { productPrice = value; OnPropertyChanged(); }
        }



        private ObservableCollection<Categories> categories;

        public ObservableCollection<Categories> Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        private Categories selectedItem;

        public Categories SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }

        Reposs repo;



        public async void GetProducts(ObservableCollection<Products> products)
        {
            repo= new Reposs();
            await repo.GetAllProducts(products);
        }

        public async void AddProducts()
        {
            repo= new Reposs();
            await repo.AddProduct(ProductName, ProductPrice, SelectedItem.Id);
        }

        public async void AddPanel()
        {
            repo = new Reposs();

            await repo.AddPanelUserControl();
        }

        public async void GetCategories(ObservableCollection<Categories> categories)
        {
            repo = new Reposs();

            await repo.GetAllCategories(categories);
        }

        public AddProductViewModel()
        {
            ObservableCollection<Categories> categories = new ObservableCollection<Categories>();
            ObservableCollection<Products> products = new ObservableCollection<Products>();

            repo = new Reposs();
            GetCategories(categories);

            Categories = categories;

            AddProductCommand = new RelayCommand((obj) =>
            {
                if (ProductName != null && ProductPrice != 0)
                {
                    AddProducts();
                    products.Clear();
                    GetProducts(products);
                    App.wrapPanel.Children.Clear();
                    AddPanel();

                    MessageBox.Show($"Product added successfully");
                }
                else
                {
                    MessageBox.Show("Invalid Data");
                }
            });
        }
    }
}


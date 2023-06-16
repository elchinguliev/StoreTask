using StoreeTaskk.Commands;
using StoreeTaskk.Models;
using StoreeTaskk.Repositories;
using StoreeTaskk.Services;
using StoreeTaskk.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreeTaskk.ViewModels
{
    public class ProductUserControlViewModel : BaseViewModel
    {  
        
        public ProductUserControlViewModel()
        {
            ObservableCollection<Products> products = new ObservableCollection<Products>();

            GetAllProducts(products);

            DeleteCommand = new RelayCommand((obj) =>
            {
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].Name == ProductName)
                    {
                        DialogResult dialog = System.Windows.Forms.MessageBox.Show("Delete ?", "Delete product", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            DeleteProduct(products, i);
                            products.Clear();
                            GetAllProducts(products);
                            App.wrapPanel.Children.Clear();
                            AddPanel();

                        }
                    }
                }
            });

            UpdateProductCommand = new RelayCommand((obj) =>
            {
                UpdateProductWindow productUpdate = new UpdateProductWindow();
                UpdateProductViewModel productUpdateUserControl = new UpdateProductViewModel();
                CategoryService categoryService = new CategoryService();

                var category = categoryService.SearchCategory(Category);

                productUpdateUserControl.ProductName = ProductName;

                productUpdateUserControl.ProductPrice = ProductPrice;

                productUpdateUserControl.ProductCategory = category.Name;

                productUpdate.DataContext = productUpdateUserControl;

                productUpdate.ShowDialog();
            });

        }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand UpdateProductCommand { get; set; }

        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private string image;

        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        private decimal productPrice;

        public decimal ProductPrice
        {
            get { return productPrice; }
            set { productPrice = value; }
        }

        private int category;

        public int Category
        {
            get { return category; }
            set { category = value; OnPropertyChanged(); }
        }
        Reposs repo;

        public async void GetAllProducts(ObservableCollection<Products> products)
        {
            repo = new Reposs();
            await repo.GetAllProducts(products);
        }

        public async void DeleteProduct(ObservableCollection<Products> products, int i)
        {
            repo = new Reposs();

            await repo.DeleteProduct(products[i].Id);
        }

        public async void AddPanel()
        {
            repo = new Reposs();

            await repo.AddPanelUserControl();
        }

     
    }
}

using StoreeTaskk.Commands;
using StoreeTaskk.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreeTaskk.ViewModels
{
    public class UpdateProductViewModel:BaseViewModel
    {
        public RelayCommand UpdateProductCommand { get; set; }

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

        private string productCateGory;

        public string ProductCategory
        {
            get { return productCateGory; }
            set { productCateGory = value; OnPropertyChanged(); }
        }

        private string newProductName;

        public string NewProductName
        {
            get { return newProductName; }
            set { newProductName = value; OnPropertyChanged(); }
        }

        private decimal newProductPrice;

        public decimal NewProductPrice
        {
            get { return newProductPrice; }
            set { newProductPrice = value; OnPropertyChanged(); }
        }

        public async void Update()
        {
            Reposs repo = new Reposs();
            await repo.UpdateProduct(ProductName, NewProductName, NewProductPrice);
            await repo.AddPanelUserControl();
        }

        public UpdateProductViewModel()
        {
            UpdateProductCommand = new RelayCommand((obj) =>
            {
                if (NewProductName == null || NewProductName.Trim() == string.Empty || NewProductPrice <= 0)
                {
                    MessageBox.Show("Invalid Data");
                }
                else if (NewProductPrice != 0 && NewProductName != string.Empty)
                {
                    Update();
                    MessageBox.Show("Item updated succesfully");
                }
            });
        }

    }
}

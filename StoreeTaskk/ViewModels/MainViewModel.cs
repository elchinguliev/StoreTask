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

namespace StoreeTaskk.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        public RelayCommand AddProductCommand { get; set; }
        private ObservableCollection<Categories> categoriesComboBox = new ObservableCollection<Categories>();
        public ObservableCollection<Categories> CategoriesComboBoxItemSource
        {
            get { return categoriesComboBox; }
            set { categoriesComboBox = value; OnPropertyChanged(); }
        }

        private Categories selectedItem;

        public Categories SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }

        private int selectedindex;
        

        public int SelectedIndex
        {
            get { return selectedindex; }
            set { selectedindex = value; OnPropertyChanged(); }
        }
            private Reposs repo;
        public RelayCommand SelectionChanged { get; set; }

        public async void GetAll(ObservableCollection<Products> products, ObservableCollection<Categories> categories)
        {
            repo = new Reposs();
            await repo.AddPanelUserControl();
        }

        public async void GetAllProducts(Categories SelectedItem, Categories category)
        {
            repo = new Reposs();

            await repo.AllProducts(SelectedItem, category);
        }

        public async void GetAllCategories(ObservableCollection<Categories> categories)
        {
            repo = new Reposs();

            await repo.AddCategoriesCombobox(categories);
        }

        public MainViewModel()
        {
            ObservableCollection<Products> products = new ObservableCollection<Products>();
            ObservableCollection<Categories> categories = new ObservableCollection<Categories>();
            CategoryService categoryServices = new CategoryService();

            GetAll(products, categories);

            SelectedIndex = categoryServices.SeacrhCategoryName("All products").Id - 1;

            GetAllCategories(CategoriesComboBoxItemSource);



            SelectionChanged = new RelayCommand((obj) =>
            {
                App.wrapPanel.Children.Clear();
                products.Clear();
                CategoryService services = new CategoryService();

                var category = services.SeacrhCategoryName("All products");

                repo = new Reposs();
                GetAllProducts(SelectedItem, category);

            });

            AddProductCommand = new RelayCommand((obj) =>
            {
                AddProductWindow addProduct = new AddProductWindow();
                addProduct.ShowDialog();
            });

        }

    }
}

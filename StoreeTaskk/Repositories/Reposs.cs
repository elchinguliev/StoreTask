using StoreeTaskk.Commands;
using StoreeTaskk.Models;
using StoreeTaskk.ViewModels;
using StoreeTaskk.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreeTaskk.Repositories
{
    public class Reposs
    {

        public async Task GetAllProducts(ObservableCollection<Products> products)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                var query = "SELECT * FROM Product";

                SqlDataReader reader = null;

                using (var command = new SqlCommand(query, conn))
                {
                    reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        Products product = new Products();
                        product.Id = (int)reader[0];
                        product.Name = reader[1].ToString();
                        product.Prices = (decimal)reader[2];
                        product.CategoryId = (int)reader[3];
                        product.Image = reader[4].ToString();
                        products.Add(product);
                    }
                }
            }
        }
        public async Task GetAllCategories(ObservableCollection<Categories> categories)
        {

            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                var query = "SELECT * FROM Categories";

                SqlDataReader reader = null;

                using (var command = new SqlCommand(query, conn))
                {
                    reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        Categories category = new Categories();
                        category.Id = (int)reader[0];
                        category.Name = reader[1].ToString();
                        categories.Add(category);
                    }
                }
            }
        }
        public async Task AllProducts(Categories SelectedItem, Categories category)
        {
            ObservableCollection<Products> products = new ObservableCollection<Products>();
            await GetAllProducts(products);

            ProductUserControl cs;
            ProductUserControlViewModel foodUsercontrolViewModel;
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].CategoryId == SelectedItem.Id || SelectedItem.Name == category.Name)
                {
                    cs = new ProductUserControl();
                    foodUsercontrolViewModel = new ProductUserControlViewModel();
                    foodUsercontrolViewModel.ProductName = products[i].Name;
                    foodUsercontrolViewModel.ProductPrice = products[i].Prices;
                    foodUsercontrolViewModel.Image = products[i].Image;
                    foodUsercontrolViewModel.Category = products[i].CategoryId;

                    cs.DataContext = foodUsercontrolViewModel;
                    App.wrapPanel.Children.Add(cs);
                }
            }
        }

        public async Task DeleteProduct(int id)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                SqlTransaction sqlTransaction = null;

                var query = "DELETE FROM Product WHERE Id=@id";

                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;

                sqlTransaction = conn.BeginTransaction();


                command.Transaction = sqlTransaction;

                SqlParameter parameterName = new SqlParameter();
                parameterName.ParameterName = "@id";
                parameterName.SqlDbType = SqlDbType.Int;
                parameterName.Value = id;

                command.Parameters.Add(parameterName);

                try
                {
                    await command.ExecuteNonQueryAsync();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                    sqlTransaction.Rollback();
                }
            }
        }

        public async Task AddPanelUserControl()
        {
            ObservableCollection<Products> products = new ObservableCollection<Products>();
            await GetAllProducts(products);
            App.wrapPanel.Children.Clear();
            ProductUserControl pus;
            ProductUserControlViewModel  productUserControlViewModel;
            int left = 70;
            int up = 10;
            int right = 0;
            int down = 70;
            for (int i = 0; i < products.Count; i++)
            {
                pus = new ProductUserControl();
                productUserControlViewModel = new ProductUserControlViewModel();
                productUserControlViewModel.ProductName = products[i].Name;
                productUserControlViewModel.ProductPrice = products[i].Prices;
                productUserControlViewModel.Image = products[i].Image;
                productUserControlViewModel.Category = products[i].CategoryId;

                pus.Margin = new Thickness(left, up, right, down);
                pus.DataContext = productUserControlViewModel;
                App.wrapPanel.Children.Add(pus);
            }
        }

        public async Task AddProduct(string name, decimal price, int categoryId)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                SqlTransaction sqlTransaction = null;

                sqlTransaction = conn.BeginTransaction();

                SqlCommand command = new SqlCommand("INSERT INTO Product(Name,Prices,CategoriesId) VALUES(@name,@price,@categoryId)", conn);
                command.Transaction = sqlTransaction;

                SqlParameter parameterCategoryId = new SqlParameter();
                parameterCategoryId.ParameterName = "@categoryId";
                parameterCategoryId.SqlDbType = SqlDbType.Int;
                parameterCategoryId.Value = categoryId;

                SqlParameter parameterName = new SqlParameter();
                parameterName.ParameterName = "@name";
                parameterName.SqlDbType = SqlDbType.NVarChar;
                parameterName.Value = name;

                SqlParameter parameterPrice = new SqlParameter();
                parameterPrice.ParameterName = "@price";
                parameterPrice.SqlDbType = SqlDbType.Money;
                parameterPrice.Value = price;

                command.Parameters.Add(parameterName);
                command.Parameters.Add(parameterCategoryId);
                command.Parameters.Add(parameterPrice);

                try
                {
                    await command.ExecuteNonQueryAsync();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                    sqlTransaction.Rollback();
                }
            }
        }
        public async Task UpdateProduct(string oldname, string newname, decimal price)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                var query = "UPDATE Product SET Name=@newname , Prices=@price WHERE Name=@oldname";


                SqlTransaction sqlTransaction = null;

                sqlTransaction = conn.BeginTransaction();

                using (var command = new SqlCommand(query, conn))
                {
                    command.Transaction = sqlTransaction;

                    SqlParameter parameterName = new SqlParameter();
                    parameterName.ParameterName = "@oldname";
                    parameterName.Value = oldname;
                    parameterName.SqlDbType = SqlDbType.NVarChar;

                    SqlParameter parameterNewName = new SqlParameter();
                    parameterNewName.ParameterName = "@newname";
                    parameterNewName.Value = newname;
                    parameterNewName.SqlDbType = SqlDbType.NVarChar;

                    SqlParameter parameterPrice = new SqlParameter();
                    parameterPrice.ParameterName = "@price";
                    parameterPrice.Value = price;
                    parameterPrice.SqlDbType = SqlDbType.Money;

                    command.Parameters.Add(parameterName);
                    command.Parameters.Add(parameterPrice);
                    command.Parameters.Add(parameterNewName);

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        sqlTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}");
                        sqlTransaction.Rollback();
                    }
                }
            }
        }
        public async Task AddCategoriesCombobox(ObservableCollection<Categories> CategoriesComboBoxItemSource)
        {
            ObservableCollection<Categories> categories = new ObservableCollection<Categories>();
            await GetAllCategories(categories);


            for (int i = 0; i < categories.Count; i++)
            {
                CategoriesComboBoxItemSource.Add(categories[i]);
            }
        }









    }
}

using StoreeTaskk.Models;
using StoreeTaskk.ViewModels;
using StoreeTaskk.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreeTaskk.Services
{
    public class CategoryService
    {
        public Categories SearchCategory(int categoryNo)
        {
            Categories categories = new Categories();
            using (var conn =new SqlConnection())
            {
                conn.ConnectionString=ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();
                var query = "Select * from Categories Where Id=@category";
                SqlCommand cmd = conn.CreateCommand();  
                cmd.CommandText = query;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName="@category";
                parameter.SqlDbType=SqlDbType.Int;
                parameter.Value=categoryNo;
                cmd.Parameters.Add(parameter);
                using (var reader=cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Id=(int)reader[0];
                        categories.Name=reader[1].ToString();
                    }
                }
                
            }
            return categories;
        }

        public void SelectionChangeCategoriesComboBox(string name, decimal price, string image, int categoryId)
        {
            ProductUserControl pus;
            ProductUserControlViewModel productUserControlViewModel;
            int left = 70;
            int up = 10;
            int right = 0;
            int down = 70;
            pus = new ProductUserControl();
            productUserControlViewModel = new ProductUserControlViewModel();
            productUserControlViewModel.ProductName = name;
            productUserControlViewModel.ProductPrice = price;
            productUserControlViewModel.Image = image;
            productUserControlViewModel.Category = categoryId;
            pus.Margin = new Thickness(left, up, right, down);
            pus.DataContext = productUserControlViewModel;
            App.wrapPanel.Children.Add(pus);
        }

        public Categories SeacrhCategoryName(string name)
        {
            Categories category = new Categories();
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                conn.Open();

                var query = "SELECT * FROM Categories WHERE Name=@name";

                SqlDataReader reader = null;

                using (var command = new SqlCommand(query, conn))
                {

                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@name";
                    parameter.SqlDbType = SqlDbType.NVarChar;
                    parameter.Value = name;

                    command.Parameters.Add(parameter);

                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        category.Id = (int)reader[0];
                        category.Name = reader[1].ToString();
                    }
                }
            }

            return category;
        }
    }
}

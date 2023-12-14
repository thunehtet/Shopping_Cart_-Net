using Shopping.Models;

using Microsoft.Data.SqlClient;
using Microsoft.Win32.SafeHandles;
using Shopping.Constants;

namespace Shopping.Data
{
    //Retrieving the ProductData in the SQL DataBase (Product Table)
    public class ProductData
    {
        public static List<Product> GetProduct()
        {
            List<Product > products = new List<Product>();

            string connectionString = CommonConstants.connectionString;

            using (SqlConnection conn=new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT ProductID,Name,Description,Price,Image, ReviewCount,CountClick from Product";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader=cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = (int)reader["ProductID"],
                        Name = (string)reader["Name"],
                        Description= (string)reader["Description"],
                        Price = (int)reader["Price"],
                        Image = (string)reader["Image"],
                        ReviewCount = (int)reader["ReviewCount"],
                        CountClick = (int)reader["CountClick"]
                    };

                    products.Add(product);
                }
            }
            return products;
        }
    }
}

using Microsoft.Data.SqlClient;
using Shopping.Constants;
using Shopping.Models;
namespace Shopping.Data
{
    //ViewCartData in SQL Database (ViewCart table)
    public class ViewCartData
    {
        public static void AddViewCart(ViewCart c)
        {
            using (SqlConnection conn = new SqlConnection(CommonConstants.connectionString))
            {
                conn.Open();

                string sql = @"Insert into ViewCart(ProductID,NumberOfPurchase,Name,Description,Price,Image)
                                Values(@ProductID,@NumberOfPurchase,@Name,@Description,@Price,@Image)";



                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ProductID", c.ProductID);
                cmd.Parameters.AddWithValue("@NumberOfPurchase", c.NumberOfPurchase);
                cmd.Parameters.AddWithValue("@Name",c.Name);
                cmd.Parameters.AddWithValue("@Description",c.Description);
                cmd.Parameters.AddWithValue("@Price",c.Price);
                cmd.Parameters.AddWithValue("@Image",c.Image);

                cmd.ExecuteNonQuery();
            }
        }

        public static List<ViewCart> GetViewCart()
        {
            List<ViewCart> cart = new List<ViewCart>();

            using (SqlConnection conn = new SqlConnection(CommonConstants.connectionString))
            {
                conn.Open();
                string sql = @"SELECT ProductID,Sum(NumberOfPurchase)as Total,Name,Description,Price,Image from ViewCart group by
                             ProductID,Name,Description,Price,Image";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ViewCart carts = new ViewCart()
                    {
                        ProductID = (int)reader["ProductID"],
                        Name = (string)reader["Name"],
                        Description = (string)reader["Description"],
                        Price = (int)reader["Price"],
                        Image = (string)reader["Image"],
                        NumberOfPurchase = (int)reader["Total"]
                    };

                    cart.Add(carts);
                }
            }
            return cart;
        }

        public static void Delete()
        {

            using (SqlConnection conn = new SqlConnection(CommonConstants.connectionString))
            {
                conn.Open();

                string sql = @"Delete from ViewCart";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

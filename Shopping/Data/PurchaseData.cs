using Microsoft.Data.SqlClient;
using Shopping.Constants;
using Shopping.Models;

namespace Shopping.Data
{
    //PurchaseData from SQL Database (Purchase Table)
    public class PurchaseData
    {
        public static void AddProduct(Purchase p)
        {

            using (SqlConnection conn = new SqlConnection(CommonConstants.connectionString))
            {
                conn.Open();

                string sql = @"Insert into Purchase(ProductID,CustomerID,NumberOfPurchase)
                                Values(@ProductID,@CustomerID,@NumberOfPurchase)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ProductID",p.ProductID);
                cmd.Parameters.AddWithValue("@CustomerID",p.CustomerID);
                cmd.Parameters.AddWithValue("@NumberOfPurchase",p.NumberOfPurchase);

                cmd.ExecuteNonQuery();                    
            }
           
           
        }
        public static List<Purchase> GetPurchaseList()
        {

            List<Purchase> list = new List<Purchase>();

            using (SqlConnection conn = new SqlConnection(CommonConstants.connectionString))
            {
                conn.Open();

                string sql = @"SELECT * From Purchase";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Purchase p = new Purchase
                    {
                        ProductID = (int)reader["ProductID"],
                        CustomerID = (int)reader["CustomerID"],
                        NumberOfPurchase = (int)reader["NumberOfPurchase"]
                    };

                    list.Add(p);
                }
            }
            return list;
        }

        public static void UpdateProduct(Purchase p)
        {
            using (SqlConnection conn = new SqlConnection(CommonConstants.connectionString))
            {
                conn.Open();

                string sql = @"Update Purchase set NumberOfPurchase=@NumberOfPurchase where ProductID=@ProductID";



                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ProductID", p.ProductID);

                cmd.Parameters.AddWithValue("@NumberOfPurchase", p.NumberOfPurchase);

                cmd.ExecuteNonQuery();
            }
          
        }
        //Retrieving the PurchaseHistory based on the Customer UserId
        public static List<Purchase> PurchaseHistory(int userId)
        {
            List<Purchase> purs = new List<Purchase>();

            using (SqlConnection conn = new SqlConnection(CommonConstants.connectionString))
            {
                conn.Open();
                string sql = "SELECT * from Purchase where CustomerID="+userId;

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Purchase pur = new Purchase()
                    {
                        ProductID = (int)reader["ProductID"],
                        NumberOfPurchase = (int)reader["NumberOfPurchase"],
                    };
                    purs.Add(pur);
                }
            }

            return purs;
         
        }

    }
}

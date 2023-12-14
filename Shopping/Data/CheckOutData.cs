using Microsoft.Data.SqlClient;
using Shopping.Constants;
using Shopping.Models;
namespace Shopping.Data
{
    public class CheckOutData
    {
        //Inserting the item(s) to be checked out from the user to store in this SQL database (CheckOut Table)
        public static void AddCheckOut(CheckOut c)
        {
            using (SqlConnection conn = new SqlConnection(CommonConstants.connectionString))
            {
                conn.Open();

                string sql = @"Insert into CheckOut(Date,Quantity,ActivationCode,ProductID,CustomerID)
                                Values(@Date,@Quantity,@ActivationCode,@ProductID,@CustomerID)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Date", c.Date);
                cmd.Parameters.AddWithValue("@Quantity", c.Quantity);
                cmd.Parameters.AddWithValue("@ActivationCode", c.ActivationCode);
                cmd.Parameters.AddWithValue("@ProductID", c.ProductID);
                cmd.Parameters.AddWithValue("@CustomerID", c.CustomerID);


                cmd.ExecuteNonQuery();
            }
        }

        //Retrieve the checkout cart data
        public static List<CheckOut> GetCheckOut(int id)
        {
            List<CheckOut> data = new List<CheckOut>();

            using (SqlConnection conn = new SqlConnection(CommonConstants.connectionString))
            {
                conn.Open();
                string sql = @"SELECT p.Name,p.Description,p.Image,c.Date,c.Quantity,c.ActivationCode 
                             from Product p, CheckOut c where p.ProductID=c.ProductID and c.CustomerID=@CustomerID
                             order by c.Date desc";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CustomerID", id);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CheckOut c = new CheckOut()
                    {
                        
                        Name = (string)reader["Name"],
                        Description = (string)reader["Description"],
                        ActivationCode = (string)reader["ActivationCode"],
                        Quantity = (int)reader["Quantity"],
                        Date = (DateTime)reader["Date"],
                        Image = (string)reader["Image"]
                       
                    };

                    data.Add(c);
                }
            }
            return data;
        }

        

        
    }
}

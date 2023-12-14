using Microsoft.Data.SqlClient;
using Shopping.Constants;
using Shopping.Models;
namespace Shopping.Data
{
    //UserData from SQL Database (Customer Table)
    public class UserData
    {
        public static List<User> GetUser()
        {
            List<User>users= new List<User>();

            using (SqlConnection conn = new SqlConnection(CommonConstants.connectionString))
            {
                conn.Open();
                string sql = "SELECT Id,Username,Password from Customer";
                
                SqlCommand cmd=new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User()
                    {
                        Id = (int)reader["Id"],
                        Username = (string)reader["Username"],
                        Password = (string)reader["Password"],
                        
                    };

                    users.Add(user);
                }
                return users;

            }

        }

        public static User GetUserByUsername(string username)
        {
            if (username == null)
            {
                return null;
            }

            User user = null;

            using (SqlConnection conn = new SqlConnection(CommonConstants.connectionString))
            {
                conn.Open();
                string sql = @"select Id,Username,Password from Customer where Username = @username";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@username", username));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user = new User()
                    {
                        Id = (int)reader["Id"],
                        Username = (string)reader["Username"],
                        Password = (string)reader["Password"],
                    };
                }
            }
            return user;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using wpf_game_dev_cycle.Model;

namespace wpf_game_dev_cycle.Repositories
{
    public class UserRepositoryControl : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool isUserValid;

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [dev_cycle_company].[admin_accounts] where Login=@login and [password]=@password";
                command.Parameters.Add("@login", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                
                isUserValid = command.ExecuteScalar() == null ? false : true;
            }
            return isUserValid;
        }

        public bool RegisterUser(NetworkCredential credential, string name, string lastname, string email, string phone)
        {
            bool isUserValid;
            int result;
            
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into [Account] (id, username, password, name, lastname, email, phone)" +
                                      "values (NEWID(), @username, @password, @name, @lastname, @email, @phone)";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                command.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = lastname;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                command.Parameters.Add("@phone", SqlDbType.NVarChar).Value = phone;
                
                result = command.ExecuteNonQuery();
            }

            if (result <= 0)
                throw new Exception();

            return true;
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [Account] where username=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            Login = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                        };
                    }
                }
            }
            return user;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}

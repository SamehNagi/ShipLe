using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleRESTServer.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Configuration;

namespace SimpleRESTServer
{
    public class UserPersistence
    {        
        /// <summary>
        /// Returns all the users in the database
        /// </summary>
        /// <returns>List of all existing users</returns>
        public static List<User> GetUsers()
        {
            string SQLString = "Select * From Users";
            List<User> Users = new List<User>();
            string ConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;

            try
            {
                using (MySqlConnection Conn = new MySqlConnection(ConnectionString))
                {
                    Conn.Open();
                    using (MySqlCommand CMD = new MySqlCommand(SQLString, Conn))
                    {
                        MySqlDataReader DR = CMD.ExecuteReader();

                        while (DR.Read())
                        {
                            User CurrentUser = new User()
                            {
                                UserID    = (long)DR["UserID"],
                                Username  = (string)DR["Username"],
                                Password  = (string)DR["Password"],
                                Email     = (string)DR["Email"],
                                FirstName = (string)DR["FirstName"],
                                LastName  = (string)DR["LastName"]
                            };

                            Users.Add(CurrentUser);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                /*Nothing TO DO*/
            }

            return Users;
        }

        /// <summary>
        /// Function to Create a new user account
        /// </summary>
        /// <param name="UserToSave">New user information</param>
        /// <returns>User ID</returns>
        public static long SaveUser(User UserToSave)
        {
            long UserID = 0;
            string ConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;
            string SQLString = "Insert Into Users (Username, FirstName, LastName, Email, Password) Values (@Username, @FirtsName, @LastName, @Email, @Password)";

            using (MySqlConnection Conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    for (int I = 0; I < 3; I++)
                    {
                        Conn.Open();
                        if (Conn.State == System.Data.ConnectionState.Open) break;
                    }

                }
                catch (Exception ex)
                {

                }

                if (Conn.State == System.Data.ConnectionState.Open)
                {
                    using (MySqlCommand CMD = new MySqlCommand(SQLString, Conn))
                    {
                        CMD.Parameters.Add("Username",  MySqlDbType.VarChar,  100).Value  = UserToSave.Username;
                        CMD.Parameters.Add("FirstName", MySqlDbType.VarChar,  100).Value  = UserToSave.FirstName;
                        CMD.Parameters.Add("LastName",  MySqlDbType.VarChar,  100).Value  = UserToSave.LastName;
                        CMD.Parameters.Add("Email",     MySqlDbType.VarChar,  100).Value  = UserToSave.Email;
                        CMD.Parameters.Add("Password",  MySqlDbType.VarChar,  100).Value  = UserToSave.Password;

                        CMD.ExecuteNonQuery();
                        UserID = CMD.LastInsertedId;
                    }
                }
            }


            return UserID;
        }

        /// <summary>
        /// Searchs for a specific user using the username
        /// </summary>
        /// <param name="Username">User Name</param>
        /// <returns>User Personal Information</returns>
        public static User GetUser(string Username)
        {
            User ResultUser = null;
            string SQLString = "Select * From Users Where Username = @Username";
            string ConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;

            using (MySqlConnection Conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    for (int I = 0; I < 3; I++ )
                    {
                        Conn.Open();
                        if (Conn.State == System.Data.ConnectionState.Open) break;
                    }

                }
                catch(Exception ex)
                {

                }

                /*Check Connection State*/
                if(Conn.State == System.Data.ConnectionState.Open)
                {
                    MySqlParameter Parameter = new MySqlParameter() 
                    { 
                        ParameterName ="@Username", 
                        DbType = System.Data.DbType.String, 
                        Size = 100, Value = Username
                    };

                    using (MySqlCommand CMD = new MySqlCommand(SQLString, Conn))
                    {
                        CMD.Parameters.Add(Parameter);
                        MySqlDataReader DR = CMD.ExecuteReader();
                        if(DR.Read() == true)
                        {
                            ResultUser = new User()
                            {
                                UserID    = long.Parse(DR["UserID"].ToString()),
                                Username  = DR["Username"].ToString(),
                                FirstName = DR["FirstName"].ToString(),
                                LastName  = DR["LastName"].ToString(),
                                Email     = DR["Email"].ToString(),
                                Password  = DR["Password"].ToString()
                            };
                        }
                    }
                }
            }

            return ResultUser;
        }

        /// <summary>
        /// Delete a user using his\her username
        /// </summary>
        /// <param name="Username">Username</param>
        /// <returns></returns>
        public static bool DeleteUser(long UserID)
        {
            bool Status = false;
            string SQLString = "Delete From Users Where UserID=@ID";

            string ConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;

            using (MySqlConnection Conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    for (int I = 0; I < 3; I++)
                    {
                        Conn.Open();
                        if (Conn.State == System.Data.ConnectionState.Open) break;
                    }

                }
                catch (Exception ex)
                {

                }

                /*Check Connection State*/
                if (Conn.State == System.Data.ConnectionState.Open)
                {
                    using (MySqlCommand CMD = new MySqlCommand(SQLString, Conn))
                    {
                        MySqlParameter Parameter = new MySqlParameter()
                        {
                            ParameterName = "@ID",
                            DbType = System.Data.DbType.Int32,
                            Size = 100,
                            Value = UserID
                        };
                        CMD.Parameters.Add(Parameter);

                        int Result = CMD.ExecuteNonQuery();

                        if (Result != 0) Status = true; else Status = false;
                    }
                }
            }

            return Status;
        }

        /// <summary>
        /// Update a user using his\her username
        /// </summary>
        /// <param name="UserData"></param>
        /// <returns></returns>
        public static bool UpdateUser(User UserData)
        {
            bool Result = false;
            string SQLString = string.Format("UPDATE Users SET " + 
                                             "Username = @Username, FirstName=@FirstName, LastName=@LastName, Email=@Email, Password=@Password Where UserID=@UserID", 
                                             UserData.FirstName, UserData.LastName, UserData.Email, UserData.Password, UserData.Username);
            string ConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;

            using (MySqlConnection Conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    for (int I = 0; I < 3; I++)
                    {
                        Conn.Open();
                        if (Conn.State == System.Data.ConnectionState.Open) break;
                    }

                }
                catch (Exception ex)
                {
                    Result = false;
                }

                /*Check Connection State*/
                if (Conn.State == System.Data.ConnectionState.Open)
                {
                    using (MySqlCommand CMD = new MySqlCommand(SQLString, Conn))
                    {
                        CMD.Parameters.Add("Username", MySqlDbType.VarChar, 100).Value = UserData.Username;
                        CMD.Parameters.Add("FirstName", MySqlDbType.VarChar, 100).Value = UserData.FirstName;
                        CMD.Parameters.Add("LastName", MySqlDbType.VarChar, 100).Value = UserData.LastName;
                        CMD.Parameters.Add("Email", MySqlDbType.VarChar, 100).Value = UserData.Email;
                        CMD.Parameters.Add("Password", MySqlDbType.VarChar, 100).Value = UserData.Password;
                        CMD.Parameters.Add("UserID", MySqlDbType.Int32).Value = UserData.UserID;
                        int AffectedRows = CMD.ExecuteNonQuery();
                        if (AffectedRows != 0) Result = true;
                    }
                }
            }

            return Result;
        }
    }
}
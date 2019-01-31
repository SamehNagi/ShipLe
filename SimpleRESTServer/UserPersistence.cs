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
        public List<User> getUsers()
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
        public long saveUser(User UserToSave)
        {
            long UserID = 0;
            string ConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;
            string SQLString = string.Format("INSERT INTO Users (Username, FirstName, LastName, Email, Password) VALUES ('{0}', '{1}', '{2}', {3}, {4}",
                                          UserToSave.Username, UserToSave.FirstName, UserToSave.LastName, UserToSave.Email, UserToSave.Password);

            using (MySqlConnection Conn = new MySqlConnection(ConnectionString))
            {
                Conn.Open();
                using (MySqlCommand CMD = new MySqlCommand(SQLString, Conn))
                {
                    CMD.ExecuteNonQuery();
                    UserID = CMD.LastInsertedId;
                }
            }


            return UserID;
        }

        /// <summary>
        /// Searchs for a specific user using the username
        /// </summary>
        /// <param name="Username">User Name</param>
        /// <returns>User Personal Information</returns>
        public User getUser(string Username)
        {
            User ResultUser = null;
            string SQLString = string.Format("Select * From Users Where Username ='{0}'", Username);
            string ConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;

            using (MySqlConnection Conn = new MySqlConnection())
            {
                Conn.Open();
                using (MySqlCommand CMD = new MySqlCommand(SQLString, Conn))
                {
                    MySqlDataReader DR = CMD.ExecuteReader();
                    if(DR.Read() == true)
                    {
                        ResultUser = new User()
                        {
                            UserID    = (long)DR["UserID"],
                            Username  = (string)DR["Username"],
                            FirstName = (string)DR["FirstName"],
                            LastName  = (string)DR["LastName"],
                            Email     = (string)DR["Email"],
                            Password  = (string)DR["Password"],
                        };
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
        public bool deleteUser(string Username)
        {
            bool Status = false;
            string SQLString = string.Format("Delete From Users Where Username ='{0}'", Username);

            string ConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;

            using (MySqlConnection Conn = new MySqlConnection())
            {
                Conn.Open();
                using (MySqlCommand CMD = new MySqlCommand(SQLString, Conn))
                {
                    int Result = CMD.ExecuteNonQuery();

                    if (Result != 0) Status = true; else Status = false;
                }
            }

            return Status;
        }

        /// <summary>
        /// Update a user using his\her username
        /// </summary>
        /// <param name="UserData"></param>
        /// <returns></returns>
        public bool updateUser(User UserData)
        {
            bool Result;
            string SQLString = string.Format("UPDATE Users SET " + 
                                             "FirstName='{0}', LastName='{1}', Email='{2}', Password='{3}' Where Username='{4}'", 
                                             UserData.FirstName, UserData.LastName, UserData.Email, UserData.Password, UserData.Username);
            string ConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;

            using (MySqlConnection Conn = new MySqlConnection())
            {
                Conn.Open();
                using (MySqlCommand CMD = new MySqlCommand(SQLString, Conn))
                {
                    int AffectedRows = CMD.ExecuteNonQuery();
                    if (AffectedRows != 0) Result = true; else Result = false;
                }
            }

            return Result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleRESTServer.Models;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Configuration;

namespace SimpleRESTServer
{
    public class TripPersistence
    {
        public List<Trip> GetTrips()
        {
            List<Trip> Trips = new List<Trip>();

            string SQLString = "Select * From Trips";
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

                if (Conn.State == System.Data.ConnectionState.Open)
                {
                    using(MySqlCommand CMD = new MySqlCommand(SQLString, Conn))
                    {
                        MySqlDataReader DR = CMD.ExecuteReader();

                        while(DR.Read())
                        {
                            Trip TripData = new Trip()
                            {
                                TripID              = long.Parse(DR["TripID"].ToString()),
                                UserID              = long.Parse(DR["UserID"].ToString()),
                                Source_Country      = long.Parse(DR["Source_Country"].ToString()),
                                Destination_Country = long.Parse(DR["Destination_Country"].ToString()),
                                Travel_Date         = DateTime.Parse(DR["Travel_Date"].ToString()),
                                Arival_Date         = DateTime.Parse(DR["Arival_Date"].ToString()),
                                Transportation      = long.Parse(DR["Transportation"].ToString()),
                                TripNote            = DR["Note"].ToString(),
                                Available_Weight    = float.Parse(DR["Weight"].ToString())
                            };

                            Trips.Add(TripData);
                        }
                    }
                }
            }

            return Trips;
        }

        /// <summary>
        /// Save a New Trip to Database
        /// </summary>
        /// <param name="TripData"></param>
        /// <returns></returns>
        public long SaveTrip(Trip TripData)
        {
            long TripID = 0;
            string SQLString = string.Format("Insert Into Trips " +
                               "(UserID, Source_Country, Destination_Country, Transportation, Travel_Date, Arival_Date, Weight, Note) " +
                               "Vales ({0}, {1}, {2}, {3}, #{4}#, #{5}#, {6}, '{7}')", 
                               TripData.UserID, TripData.Source_Country, TripData.Destination_Country, TripData.Transportation, 
                               TripData.Travel_Date, TripData.Arival_Date, TripData.Available_Weight, TripData.TripNote);
            string ConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;

            using (MySqlConnection Conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    for (int I = 0; I < 3; I++)
                    {
                        Conn.Open();
                        if (Conn.State == ConnectionState.Open) break;
                    }

                }
                catch (Exception ex)
                {
                    //Action to take in case of connection faild
                }

                if (Conn.State == ConnectionState.Open)
                {
                    using (MySqlCommand CMD = new MySqlCommand(SQLString, Conn))
                    {
                        int AffectedRows = CMD.ExecuteNonQuery();

                        if (AffectedRows != 0) TripID = CMD.LastInsertedId;
                    }
                }
            }

            return TripID;
        }

        /// <summary>
        /// Get Trip Data based on its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Trip GetTrip(long ID)
        {
            Trip TripData = null;
            string SQLString = "Select * From Trips Where TripID=" + ID.ToString();
            string ConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;

            using(MySqlConnection Conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    for (int I = 0; I < 3; I++)
                    {
                        Conn.Open();
                        if (Conn.State == ConnectionState.Open) break;
                    }

                }
                catch (Exception ex)
                {

                }

                if(Conn.State == ConnectionState.Open)
                {
                    using(MySqlCommand CMD = new MySqlCommand(SQLString, Conn))
                    {
                        MySqlDataReader DR = CMD.ExecuteReader();

                        if (DR.Read())
                        {
                            TripData = new Trip()
                            {
                                UserID              = long.Parse(DR["UserID"].ToString()),
                                TripID              = long.Parse(DR["TripID"].ToString()),
                                Source_Country      = long.Parse(DR["Source_Country"].ToString()),
                                Destination_Country = long.Parse(DR["Destination_Country"].ToString()),
                                Travel_Date         = DateTime.Parse(DR["Travel_Date"].ToString()),
                                Arival_Date         = DateTime.Parse(DR["Arival_Date"].ToString()),
                                Transportation      = long.Parse(DR["Transportation"].ToString()),
                                Available_Weight    = float.Parse(DR["Weight"].ToString()),
                                TripNote            = DR["Note"].ToString()
                            };
                        }
                    }
                }
            }

            return TripData;
        }

        /// <summary>
        /// Delete Trip Data based on its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteTrip(long ID)
        {
            bool Deleted = false;
            string SQLString = "Delete From Trips Where TripID=" + ID.ToString();
            string ConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;

            using (MySqlConnection Conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    for (int I = 0; I < 3; I++)
                    {
                        Conn.Open();
                        if (Conn.State == ConnectionState.Open) break;
                    }

                }
                catch (Exception ex)
                {

                }

                if (Conn.State == ConnectionState.Open)
                {
                    using (MySqlCommand CMD = new MySqlCommand(SQLString, Conn))
                    {
                        int AffectedRows = CMD.ExecuteNonQuery();

                        if (AffectedRows != 0) Deleted = true;
                    }
                }
            }

            return Deleted;
        }

        /// <summary>
        /// Update Trip Data Using its Data
        /// </summary>
        /// <param name="TripData"></param>
        /// <returns></returns>
        public bool UpdateTrips(Trip TripData)
        {
            bool Updated = false;
            string SQLString = "Update Trips Set "  +
                                "TripID              = @TripID"         + 
                                "UserID              = @UserID,"        +
                                "Travel_Date         = @TDate"          +
                                "Arival_Date         = @ADate"          +
                                "Source_Country      = @SCountry"       +
                                "Destination_Country = @DCountry"       +
                                "Transportation      = @Transportation" +
                                "Weight              = @Weight"         +
                                "Note                = @Note"           +
                                "Where TripID        = @TripID";
                                
            string ConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;

            using (MySqlConnection Conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    for (int I = 0; I < 3; I++)
                    {
                        Conn.Open();
                        if (Conn.State == ConnectionState.Open) break;
                    }

                }
                catch (Exception ex)
                {

                }

                if (Conn.State == ConnectionState.Open)
                {
                    using (MySqlCommand CMD = new MySqlCommand(SQLString, Conn))
                    {
                        CMD.Parameters.Add("@TripID",         MySqlDbType.Int32).Value = TripData.TripID;
                        CMD.Parameters.Add("@UserID",         MySqlDbType.Int32).Value = TripData.UserID;
                        CMD.Parameters.Add("@TDate",          MySqlDbType.Date).Value  = TripData.Travel_Date;
                        CMD.Parameters.Add("@ADate",          MySqlDbType.Date).Value  = TripData.Arival_Date;
                        CMD.Parameters.Add("@SCountry",       MySqlDbType.Int32).Value = TripData.Source_Country;
                        CMD.Parameters.Add("@DCountry",       MySqlDbType.Int32).Value = TripData.Destination_Country;
                        CMD.Parameters.Add("@Transportation", MySqlDbType.Int32).Value = TripData.Transportation;
                        CMD.Parameters.Add("@Weight",         MySqlDbType.Float).Value = TripData.Available_Weight;
                        CMD.Parameters.Add("@Note",           MySqlDbType.Text).Value  = TripData.TripNote;

                        int AffectedRows = CMD.ExecuteNonQuery();
                        if (AffectedRows != 0) Updated = true;
                    }
                }
            }

            return Updated;
        }
    }
}
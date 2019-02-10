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
            string SQLString = "";
            string ConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;

			MySql.Data.MySqlClient.MySqlConnection conn;
            conn = new MySql.Data.MySqlClient.MySqlConnection();
			try
			{
				conn.ConnectionString = myConnectionString;
                conn.Open();
				
				string sqlString = "INSERT INTO Trips (Username, From_City_Country, To_City_Country, TransportationType, OutboundTripDetails_Day, " +
                                "OutboundTripDetails_Time, OutboundTripDetails_Duration, AddAReturnTrip, ReturnTripDetails_Day, ReturnTripDetails_Time, " +
                                "ReturnTripDetails_Duration, AvailableWeight, ExcludedCategories, TripNote) " +
                                "VALUES ('" + tripToSave.Username + "', '" + tripToSave.From_City_Country + "', '" + tripToSave.To_City_Country + "', " +
                                            "'" + tripToSave.Transportation + "', '" + tripToSave.OutboundTripDetails_Day.ToString("yyyy-MM-dd") + "', " +
                                            "'" + tripToSave.OutboundTripDetails_Time + "', " + tripToSave.OutboundTripDetails_Duration + ", " +
                                            "" + tripToSave.AddAReturnTrip + ", '" + tripToSave.ReturnTripDetails_Day.ToString("yyyy-MM-dd") + "', " +
                                            "'" + tripToSave.ReturnTripDetails_Time + "', " + tripToSave.ReturnTripDetails_Duration + ", " +
                                            "" + tripToSave.AvailableWeight + ", '" + tripToSave.ExcludedCategories + "', '" + tripToSave.TripNote + "')";
				
				MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
				cmd.ExecuteNonQuery();
				long tripId = cmd.LastInsertedId;
				return tripId;
			}
			catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
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
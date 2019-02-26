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
        public static List<Trip> GetTrips()
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
                        if (Conn.State == ConnectionState.Open) break;
                    }

                }
                catch (Exception ex)
                {

                }

                if (Conn.State == ConnectionState.Open)
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
                                SourceCountry       = long.Parse(DR["SourceCountry"].ToString()),
                                DestinationCountry  = long.Parse(DR["DestinationCountry"].ToString()),
                                TravelDate          = DateTime.Parse(DR["TravelDate"].ToString()),
                                ArrivalDate         = DateTime.Parse(DR["ArrivalDate"].ToString()),
                                TransportationType  = long.Parse(DR["TransportationType"].ToString()),
                                TripNote            = DR["TripNote"].ToString(),
                                AvailableWeight = float.Parse(DR["AvailableWeight"].ToString())
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
        public static long SaveTrip(Trip TripData)
        {
            long TripID = 0;
            string TravelDate = TripData.TravelDate.ToString("yyyy-dd-MM hh:mm");
            string ArrivalDate = TripData.ArrivalDate.ToString("yyyy-dd-MM hh:mm");

            string SQLString = string.Format("Insert Into Trips " +
                               "(UserID, SourceCountry, DestinationCountry, TravelDate, ArrivalDate, AvailableWeight, TransportationType, TripNote) " +
                               "Values ({0}, {1}, {2}, '{3}', '{4}', {5}, {6}, '{7}')", 
                               TripData.UserID, TripData.SourceCountry, TripData.DestinationCountry,
                               TravelDate, ArrivalDate, TripData.AvailableWeight.ToString(), TripData.TransportationType, TripData.TripNote);
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
        public static Trip GetTrip(long ID)
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
                                SourceCountry       = long.Parse(DR["SourceCountry"].ToString()),
                                DestinationCountry  = long.Parse(DR["DestinationCountry"].ToString()),
                                TravelDate          = DateTime.Parse(DR["TravelDate"].ToString()),
                                ArrivalDate         = DateTime.Parse(DR["ArrivalDate"].ToString()),
                                TransportationType  = long.Parse(DR["TransportationType"].ToString()),
                                AvailableWeight     = float.Parse(DR["AvailableWeight"].ToString()),
                                TripNote            = DR["TripNote"].ToString()
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
        public static bool DeleteTrip(long ID)
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
        public static bool UpdateTrips(Trip TripData)
        {
            bool Updated = false;
            string SQLString = "Update Trips Set "  +
                                "TripID              = @TripID,"             + 
                                "UserID              = @UserID,"             +
                                "TravelDate          = @TDate,"              +
                                "ArrivalDate         = @ADate,"              +
                                "SourceCountry       = @SCountry,"           +
                                "DestinationCountry  = @DCountry,"           +
                                "TransportationType  = @TransportationType," +
                                "AvailableWeight     = @Weight,"             +
                                "TripNote            = @TripNote "               +
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
                        CMD.Parameters.Add("@TripID",             MySqlDbType.Int32).Value = TripData.TripID;
                        CMD.Parameters.Add("@UserID",             MySqlDbType.Int32).Value = TripData.UserID;
                        CMD.Parameters.Add("@TDate",              MySqlDbType.Date).Value  = TripData.TravelDate;
                        CMD.Parameters.Add("@ADate",              MySqlDbType.Date).Value  = TripData.ArrivalDate;
                        CMD.Parameters.Add("@SCountry",           MySqlDbType.Int32).Value = TripData.SourceCountry;
                        CMD.Parameters.Add("@DCountry",           MySqlDbType.Int32).Value = TripData.DestinationCountry;
                        CMD.Parameters.Add("@TransportationType", MySqlDbType.Int32).Value = TripData.TransportationType;
                        CMD.Parameters.Add("@Weight",             MySqlDbType.Float).Value = TripData.AvailableWeight;
                        CMD.Parameters.Add("@TripNote",           MySqlDbType.Text).Value  = TripData.TripNote;

                        int AffectedRows = CMD.ExecuteNonQuery();
                        if (AffectedRows != 0) Updated = true;
                    }
                }
            }

            return Updated;
        }
    }
}
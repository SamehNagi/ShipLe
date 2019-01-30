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
    public class TripPersistence
    {
        public ArrayList getTrips()
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;
            conn = new MySql.Data.MySqlClient.MySqlConnection();
			try
			{
				conn.ConnectionString = myConnectionString;
                conn.Open();
				
				ArrayList tripArray = new ArrayList();

                MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

                string sqlString = "SELECT * FROM Trips";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

				mySQLReader = cmd.ExecuteReader();
				while (mySQLReader.Read())
				{
					Trip t = new Trip();
	
					t.TripID                       = mySQLReader.GetInt32(0);
					t.Username                     = mySQLReader.GetString(1);
					t.From_City_Country            = mySQLReader.GetString(2);
					t.To_City_Country              = mySQLReader.GetString(3);
					t.TransportationType           = mySQLReader.GetString(4);
					t.OutboundTripDetails_Day      = mySQLReader.GetDateTime(5);
					t.OutboundTripDetails_Time     = mySQLReader.GetTimeSpan(6);
					t.OutboundTripDetails_Duration = mySQLReader.GetFloat(7);
					t.AddAReturnTrip               = mySQLReader.GetBoolean(8);
					t.ReturnTripDetails_Day        = mySQLReader.GetDateTime(9);
					t.ReturnTripDetails_Time       = mySQLReader.GetTimeSpan(10);
					t.ReturnTripDetails_Duration   = mySQLReader.GetFloat(11);
					t.AvailableWeight              = mySQLReader.GetFloat(12);
					t.ExcludedCategories           = mySQLReader.GetString(13);
					t.TripNote                     = mySQLReader.GetString(14);
	
					tripArray.Add(t);
				}
				return tripArray;
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

        public long saveTrip(Trip tripToSave)
        {
			MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;
            conn = new MySql.Data.MySqlClient.MySqlConnection();
			try
			{
				conn.ConnectionString = myConnectionString;
                conn.Open();
				
				string sqlString = "INSERT INTO Trips (Username, From_City_Country, To_City_Country, TransportationType, OutboundTripDetails_Day, " +
                                "OutboundTripDetails_Time, OutboundTripDetails_Duration, AddAReturnTrip, ReturnTripDetails_Day, ReturnTripDetails_Time, " +
                                "ReturnTripDetails_Duration, AvailableWeight, ExcludedCategories, TripNote) " +
                                "VALUES ('" + tripToSave.Username + "', '" + tripToSave.From_City_Country + "', '" + tripToSave.To_City_Country + "', " +
                                            "'" + tripToSave.TransportationType + "', '" + tripToSave.OutboundTripDetails_Day.ToString("yyyy-MM-dd") + "', " +
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

        public Trip getTrip(long tripId)
        {
			MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;
            conn = new MySql.Data.MySqlClient.MySqlConnection();
			try
			{
				conn.ConnectionString = myConnectionString;
                conn.Open();
				
				Trip t = new Trip();
				MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

				string sqlString = "SELECT * FROM Trips WHERE TripID = " + tripId.ToString();
				MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
				
				mySQLReader = cmd.ExecuteReader();
				if (mySQLReader.Read())
				{
					t.TripID = mySQLReader.GetInt32(0);
					t.Username = mySQLReader.GetString(1);
					t.From_City_Country = mySQLReader.GetString(2);
					t.To_City_Country = mySQLReader.GetString(3);
					t.TransportationType = mySQLReader.GetString(4);
					t.OutboundTripDetails_Day = mySQLReader.GetDateTime(5);
					t.OutboundTripDetails_Time = mySQLReader.GetTimeSpan(6);
					t.OutboundTripDetails_Duration = mySQLReader.GetFloat(7);
					t.AddAReturnTrip = mySQLReader.GetBoolean(8);
					t.ReturnTripDetails_Day = mySQLReader.GetDateTime(9);
					t.ReturnTripDetails_Time = mySQLReader.GetTimeSpan(10);
					t.ReturnTripDetails_Duration = mySQLReader.GetFloat(11);
					t.AvailableWeight = mySQLReader.GetFloat(12);
					t.ExcludedCategories = mySQLReader.GetString(13);
					t.TripNote = mySQLReader.GetString(14);
	
					return t;
				}
				else
				{
					return null;
				}
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

        public bool deleteTrip(long tripId)
        {
			MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            try
			{
				conn.ConnectionString = myConnectionString;
                conn.Open();
				
				Trip u = new Trip();
				MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
	
				string sqlString = "SELECT * FROM Trips WHERE TripID = " + tripId.ToString();
				MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
				
				mySQLReader = cmd.ExecuteReader();
				if (mySQLReader.Read())
				{
					mySQLReader.Close();
	
					sqlString = "DELETE FROM Trips WHERE TripID = " + tripId.ToString();
					cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
	
					cmd.ExecuteNonQuery();
					return true;
				}
				else
				{
					return false;
				}
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

        public bool updateTrip(long tripId, Trip tripToSave)
        {
			MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            try
			{
				conn.ConnectionString = myConnectionString;
                conn.Open();
				
				MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

				string sqlString = "SELECT * FROM Trips WHERE TripID = " + tripId.ToString();
				MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
				
				mySQLReader = cmd.ExecuteReader();
				if (mySQLReader.Read())
				{
					mySQLReader.Close();
	
					sqlString = "UPDATE Trips SET " +
													"Username='" + tripToSave.Username + "', " +
													"From_City_Country='" + tripToSave.From_City_Country + "', " +
													"To_City_Country='" + tripToSave.To_City_Country + "', " +
													"TransportationType='" + tripToSave.TransportationType + "', " +
													"OutboundTripDetails_Day='" + tripToSave.OutboundTripDetails_Day.ToString("yyyy-MM-dd") + "', " +
													"OutboundTripDetails_Time='" + tripToSave.OutboundTripDetails_Time + "', " +
													"OutboundTripDetails_Duration='" + tripToSave.OutboundTripDetails_Duration + "', " +
													"AddAReturnTrip=" + tripToSave.AddAReturnTrip + ", " +
													"ReturnTripDetails_Day='" + tripToSave.ReturnTripDetails_Day.ToString("yyyy-MM-dd") + "', " +
													"ReturnTripDetails_Time='" + tripToSave.ReturnTripDetails_Time + "', " +
													"ReturnTripDetails_Duration='" + tripToSave.ReturnTripDetails_Duration + "', " +
													"AvailableWeight=" + tripToSave.AvailableWeight + ", " +
													"ExcludedCategories='" + tripToSave.ExcludedCategories + "', " +
													"TripNote='" + tripToSave.TripNote + "' " +
													"WHERE TripID = " + tripId.ToString();
					cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
	
					cmd.ExecuteNonQuery();
					return true;
				}
				else
				{
					return false;
				}
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
    }
}
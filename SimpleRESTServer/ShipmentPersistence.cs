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
    public class ShipmentPersistence
    {
        public ArrayList getShipments()
        {
			MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;
            conn = new MySql.Data.MySqlClient.MySqlConnection();
			try
			{
				conn.ConnectionString = myConnectionString;
                conn.Open();
				
				ArrayList shipmentArray = new ArrayList();

				MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
	
				string sqlString = "SELECT * FROM Shipments";
				MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
	
				mySQLReader = cmd.ExecuteReader();
				while (mySQLReader.Read())
				{
					Shipment sh = new Shipment();
	
					sh.ShipmentID        = mySQLReader.GetInt32(0);
                    sh.TripID            = mySQLReader.GetInt32(1);
                    sh.Username          = mySQLReader.GetString(2);
					sh.From_City_Country = mySQLReader.GetString(3);
					sh.To_City_Country   = mySQLReader.GetString(4);
					sh.IWantItBefore     = mySQLReader.GetDateTime(5);
					sh.ShipmentName      = mySQLReader.GetString(6);
					sh.ShipmentNote      = mySQLReader.GetString(7);
	
					shipmentArray.Add(sh);
				}
				return shipmentArray;
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

        public long saveShipment(Shipment shipmentToSave)
        {
			MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            try
			{
				conn.ConnectionString = myConnectionString;
                conn.Open();
				
				string sqlString = "INSERT INTO Shipments (TripID, Username, From_City_Country, To_City_Country, IWantItBefore, ShipmentName, ShipmentNote) " +
                "VALUES (" + shipmentToSave.TripID + ", '" + shipmentToSave.Username + "', '" + shipmentToSave.From_City_Country + "', '" + shipmentToSave.To_City_Country + "', " +
                "'" + shipmentToSave.IWantItBefore.ToString("yyyy-MM-dd") + "', '" + shipmentToSave.ShipmentName + "', '" + shipmentToSave.ShipmentNote + "')";

				MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
				cmd.ExecuteNonQuery();
				long shipmentId = cmd.LastInsertedId;
				return shipmentId;
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

        public Shipment getShipment(long shipmentId)
        {
			MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            try
			{
				conn.ConnectionString = myConnectionString;
                conn.Open();
				
				Shipment sh = new Shipment();
				MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
	
				string sqlString = "SELECT * FROM Shipments WHERE ShipmentID = " + shipmentId.ToString();
				MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
	
				mySQLReader = cmd.ExecuteReader();
				if (mySQLReader.Read())
				{
					sh.ShipmentID        = mySQLReader.GetInt32(0);
                    sh.TripID            = mySQLReader.GetInt32(1);
                    sh.Username          = mySQLReader.GetString(2);
					sh.From_City_Country = mySQLReader.GetString(3);
					sh.To_City_Country   = mySQLReader.GetString(4);
					sh.IWantItBefore     = mySQLReader.GetDateTime(5);
					sh.ShipmentName      = mySQLReader.GetString(6);
					sh.ShipmentNote      = mySQLReader.GetString(7);
	
					return sh;
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

        public bool deleteShipment(long shipmentId)
        {
			MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            try
			{
				Shipment u = new Shipment();
				MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
	
				string sqlString = "SELECT * FROM Shipments WHERE ShipmentID = " + shipmentId.ToString();
				MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
	
				mySQLReader = cmd.ExecuteReader();
				if (mySQLReader.Read())
				{
					mySQLReader.Close();
	
					sqlString = "DELETE FROM Shipments WHERE ShipmentID = " + shipmentId.ToString();
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

        public bool updateShipment(long shipmentId, Shipment shipmentToSave)
        {
			MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString = ConfigurationManager.ConnectionStrings["PhpMySqlRemoteDB"].ConnectionString;
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            try
			{
				MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

				string sqlString = "SELECT * FROM Shipments WHERE ShipmentID = " + shipmentId.ToString();
				MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
	
				mySQLReader = cmd.ExecuteReader();
				if (mySQLReader.Read())
				{
					mySQLReader.Close();
	
					sqlString = "UPDATE Shipments SET " +
													"TripID=" + shipmentToSave.TripID + ", " +
                                                    "Username='" + shipmentToSave.Username + "', " +
                                                    "From_City_Country='" + shipmentToSave.From_City_Country + "', " +
													"To_City_Country='" + shipmentToSave.To_City_Country + "', " +
													"IWantItBefore='" + shipmentToSave.IWantItBefore.ToString("yyyy-MM-dd") + "', " +
													"ShipmentName='" + shipmentToSave.ShipmentName + "', " +
													"ShipmentNote='" + shipmentToSave.ShipmentNote + "' " +
													"WHERE ShipmentID = " + shipmentId.ToString();
	
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
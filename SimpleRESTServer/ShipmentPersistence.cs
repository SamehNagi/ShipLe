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
        /// <summary>
        /// Gets All shipments in the database
        /// </summary>
        /// <returns></returns>
        public List<Shipment> GetShimpments()
        {
            List<Shipment> Shipments = new List<Shipment>();
            string SQLQuery = "Select * From Shipments";
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
                    using (MySqlCommand CMD = new MySqlCommand(SQLQuery, Conn))
                    {

                        MySqlDataReader DR = CMD.ExecuteReader();

                        while (DR.Read())
                        {
                            Shipment Ship = new Shipment()
                            {
                                ShipmentID        = DR["ShipmentID"].ToString(),
                                TripID            = DR["TripID"].ToString(),
                                Username          = DR["Username"].ToString(),
                                From_City_Country = DR["From_City_Country"].ToString(),
                                To_City_Country   = DR["To_City_Country"],
                                IWantItBefore     = DR["IWantItBefore"],
                                ShipmentName      = DR["ShipmentName"],
                                ShipmentNote      = DR["ShipmentNote"]
                            };

                            Shipments.Add(Ship);
                        }
                    }
                }
            }

            return Shipments;
        }

        /// <summary>
        /// Save New Shipment to the Database
        /// </summary>
        /// <param name="ShipmentData"></param>
        /// <returns></returns>
        public long SaveShipment(Shipment ShipmentData)
        {
            long ShipmentID = 0;
            string SQLQuery = string.Format("INSERT INTO Shipments (TripID, Username, From_City_Country, To_City_Country, " +
                              "IWantItBefore, ShipmentName, ShipmentNote) Values ({0},'{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                              ShipmentData.ShipmentID, ShipmentData.Username, ShipmentData.From_City_Country,
                              ShipmentData.To_City_Country, ShipmentData.IWantItBefore, ShipmentData.ShipmentName,
                              ShipmentData.ShipmentNote);
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
                    using (MySqlCommand CMD = new MySqlCommand(SQLQuery, Conn))
                    {
                        CMD.ExecuteNonQuery();
                        ShipmentID = CMD.LastInsertedID();
                    }
                }
            }
        }

        /// <summary>
        /// Gets a specific Shipment using ID
        /// </summary>
        /// <param name="ShipmentID"></param>
        /// <returns></returns>
        public Shipment GetShipment(long ShipmentID)
        {
            Shipment ShipmentData = null;
            string SQLQuery = "Select * From Shipments Where ShipmentID=" + ShipmentID.ToString();
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
                    using (MySqlCommand CMD = new MySqlCommand(SQLQuery, Conn))
                    {
                        MySqlDataReader DR = CMD.ExecuteQuery();

                        if (DR.Read())
                        {
                            ShipmentData = new Shipment()
                            {
                                ShipmentID          = DR["ShipmentID"].ToString(),
                                TripID              = DR["TripID"].ToString(),
                                Username            = DR["Username"].ToString(),
                                From_City_Country   = DR["From_City_Country"].ToString(),
                                To_City_Country     = DR["To_City_Country"].ToString(),
                                IWantItBefore       = DR["IWantItBefore"].ToString(),
                                ShipmentName        = DR["ShipmentName"].ToString(),
                                ShipmentNote        = DR["ShipmentNote"].ToString(),
                            };
                        }
                    }
                }
            }

            return ShipmentData;
        }

        /// <summary>
        /// Deletes a Shipment from the database using ID
        /// </summary>
        /// <param name="ShipmentID">ID of the shipment to be deleted</param>
        /// <returns></returns>
        public bool DeleteShipment(long ShipmentID)
        {
            bool Deleted = false;
            string SQLQuery = "Delete From Shipments Where ShipmentID=" + ShipmentID.ToString();
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
                    using (MySqlCommand CMD = new MySqlCommand(SQLQuery, Conn))
                    {
                        if (CMD.ExecuteNonQuery() > 0) Deleted = true;
                    }
                }
            }

            return Deleted;
        }

        /// <summary>
        /// Update Shipment Data using its ID
        /// </summary>
        /// <param name="ShipmentData"></param>
        /// <returns></returns>
        public bool UpdateShipment(Shipment ShipmentData)
        {
            string SQLQuery = "Update Shipments Set " +
                                                    "TripID="             + ShipmentData.TripID + ", " +
                                                    "Username='"          + ShipmentData.Username + "', " +
                                                    "From_City_Country='" + ShipmentData.From_City_Country + "', " +
                                                    "To_City_Country='"   + ShipmentData.To_City_Country + "', " +
                                                    "IWantItBefore='"     + ShipmentData.IWantItBefore.ToString("yyyy-MM-dd") + "', " +
                                                    "ShipmentName='"      + ShipmentData.ShipmentName + "', " +
                                                    "ShipmentNote='"      + ShipmentData.ShipmentNote + "' " +
                                                    "Where ShipmentID = " + ShipmentData.ShipmentID.ToString(); 

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
                    using (MySqlCommand CMD = new MySqlCommand(SQLQuery, Conn))
                    {
                    }
                }
            }
            try
			{

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
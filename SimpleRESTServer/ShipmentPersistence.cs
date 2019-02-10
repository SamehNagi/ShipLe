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
        public static List<Shipment> GetShimpments()
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
                                ShipmentID        = long.Parse(DR["ShipmentID"].ToString()),
                                TripID            = long.Parse(DR["TripID"].ToString()),
                                Username          = DR["Username"].ToString(),
                                From_City_Country = DR["From_City_Country"].ToString(),
                                To_City_Country   = DR["To_City_Country"].ToString(),
                                IWantItBefore     = DateTime.Parse(DR["IWantItBefore"].ToString()),
                                ShipmentName      = DR["ShipmentName"].ToString(),
                                ShipmentNote      = DR["ShipmentNote"].ToString()
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
        public static long SaveShipment(Shipment ShipmentData)
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
                    //Check if the connection is faild
                }

                if (Conn.State == System.Data.ConnectionState.Open)
                {
                    using (MySqlCommand CMD = new MySqlCommand(SQLQuery, Conn))
                    {
                        CMD.ExecuteNonQuery();
                        ShipmentID = CMD.LastInsertedId;
                    }
                }
            }

            return ShipmentID;
        }

        /// <summary>
        /// Gets a specific Shipment using ID
        /// </summary>
        /// <param name="ShipmentID"></param>
        /// <returns></returns>
        public static Shipment GetShipment(long ShipmentID)
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
                        MySqlDataReader DR = CMD.ExecuteReader();

                        if (DR.Read())
                        {
                            ShipmentData = new Shipment()
                            {
                                ShipmentID          = long.Parse(DR["ShipmentID"].ToString()),
                                TripID              = long.Parse(DR["TripID"].ToString()),
                                Username            = DR["Username"].ToString(),
                                From_City_Country   = DR["From_City_Country"].ToString(),
                                To_City_Country     = DR["To_City_Country"].ToString(),
                                IWantItBefore       = DateTime.Parse(DR["IWantItBefore"].ToString()),
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
        public static bool DeleteShipment(long ShipmentID)
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
        public static bool UpdateShipment(Shipment ShipmentData)
        {
            bool Updated = false;
            string SQLQuery = "Update Shipments Set " +
                                                    "TripID              = @TripID,"          +
                                                    "Username            = @Username,"        +
                                                    "From_City_Country   = @FromCountry,"     +
                                                    "To_City_Country     = @ToCountry,"       +
                                                    "IWantItBefore       = @DeliveryDate,"    +
                                                    "ShipmentName        = @Shipmentname,"    +
                                                    "ShipmentNote        = @ShipmentNote,"    +
                                                    "Where ShipmentID    = @ShipmentID";

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
                    //Check for what to do in case the connection faild
                }

                if (Conn.State == System.Data.ConnectionState.Open)
                {
                    using (MySqlCommand CMD = new MySqlCommand(SQLQuery, Conn))
                    {
                        CMD.Parameters.Add("@TripID",       MySqlDbType.Int32).Value   = ShipmentData.TripID;
                        CMD.Parameters.Add("@Username",     MySqlDbType.VarChar).Value = ShipmentData.Username;
                        CMD.Parameters.Add("@FromCountry",  MySqlDbType.VarChar).Value = ShipmentData.From_City_Country;
                        CMD.Parameters.Add("@ToCountry",    MySqlDbType.VarChar).Value = ShipmentData.To_City_Country;
                        CMD.Parameters.Add("@DeliveryDate", MySqlDbType.Date).Value    = ShipmentData.IWantItBefore;
                        CMD.Parameters.Add("@Shipmentname", MySqlDbType.VarChar).Value = ShipmentData.ShipmentName;
                        CMD.Parameters.Add("@ShipmentNote", MySqlDbType.VarChar).Value = ShipmentData.ShipmentNote;

                        int AffectedRows = CMD.ExecuteNonQuery();
                        if (AffectedRows > 0) Updated = true;
                    }
                }
            }


            return Updated;
        }
    }
}
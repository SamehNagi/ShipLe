using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleRESTServer.Models
{
    public class Shipment
    {
        public long ShipmentID { get; set; }
        public long TripID { get; set; }
        public long UserID { get; set; }
        public String Source_Country { get; set; }
        public String Destination_Country { get; set; }
        public DateTime Delivery_Date { get; set; }
        public String ShipmentName { get; set; }
        public String ShipmentNote { get; set; }
    }
}
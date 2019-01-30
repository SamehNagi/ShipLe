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
        public String Username { get; set; }
        public String From_City_Country { get; set; }
        public String To_City_Country { get; set; }
        public DateTime IWantItBefore { get; set; }
        public String ShipmentName { get; set; }
        public String ShipmentNote { get; set; }
    }
}
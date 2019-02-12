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
        public String SourceCountry { get; set; }
        public String DestinationCountry { get; set; }
        public DateTime DeliveryDate { get; set; }
        public String ShipmentName { get; set; }
        public String ShipmentNote { get; set; }
    }
}
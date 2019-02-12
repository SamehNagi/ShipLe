using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleRESTServer.Models
{
    public class Trip
    {
        public long TripID { get; set; }
        public long UserID { get; set; }
        public long SourceCountry { get; set; }
        public long DestinationCountry { get; set; } 
        public long TransportationType { get; set; }
        public DateTime TravelDate { get; set; } 
        public DateTime ArrivalDate { get; set; }
        public float  AvailableWeight { get; set; }
        public String TripNote { get; set; }
    }
}
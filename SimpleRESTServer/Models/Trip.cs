using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleRESTServer.Models
{
    public class Trip
    {
        public long TripID { get; set; }
        public String Username { get; set; }
        public String From_City_Country { get; set; }
        public String To_City_Country { get; set; } 
        public String TransportationType { get; set; }
        public DateTime OutboundTripDetails_Day { get; set; } 
        public TimeSpan OutboundTripDetails_Time { get; set; }
        public float OutboundTripDetails_Duration { get; set; }
        public bool AddAReturnTrip { get; set; }
        public DateTime ReturnTripDetails_Day { get; set; }
        public TimeSpan ReturnTripDetails_Time { get; set; }
        public float ReturnTripDetails_Duration { get; set; }
        public float AvailableWeight { get; set; }
        public String ExcludedCategories { get; set; }
        public String TripNote { get; set; }
    }
}
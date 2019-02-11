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
        public long Source_Country { get; set; }
        public long Destination_Country { get; set; } 
        public long Transportation { get; set; }
        public DateTime Travel_Date { get; set; } 
        public DateTime Arrival_Date { get; set; }
        public String TripNote { get; set; }
        public float  Available_Weight { get; set; }
    }
}
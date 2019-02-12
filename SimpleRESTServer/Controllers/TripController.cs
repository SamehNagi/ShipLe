using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleRESTServer.Models;
using System.Collections;

namespace SimpleRESTServer.Controllers
{
    public class TripController : ApiController
    {
        /// <summary>
        /// Get all trips
        /// </summary>
        /// <returns></returns>
        // GET: api/Trip
        public List<Trip> Get()
        {
            return TripPersistence.GetTrips();
        }

        /// <summary>
        /// Get a specific trip by id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        // GET: api/Trip/?ID=
        public Trip Get(long ID)
        {
            Trip TripData = TripPersistence.GetTrip(ID);

            return TripData;
        }

        /// <summary>
        /// Create/Save a new trip
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        // POST: api/Trip
        public long Post([FromBody]Trip Value)
        {
            long ID = TripPersistence.SaveTrip(Value);

            return ID;
        }

        /// <summary>
        /// Modify a specific trip by id
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        // PUT: api/Trip/?ID=
        public bool Put(long ID, [FromBody]Trip Value)
        {
            Value.TripID = ID;
            bool Updated = TripPersistence.UpdateTrips(Value);

            return Updated;
        }

        /// <summary>
        /// Delete a specific trip by id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        // DELETE: api/Trip/?ID=
        public bool Delete(long ID)
        {
            bool Deleted = TripPersistence.DeleteTrip(ID);

            return Deleted;
        }
    }
}
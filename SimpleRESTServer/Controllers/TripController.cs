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
        // GET: api/Trip/5
        public Trip Get(long ID)
        {
            Trip TripData = TripPersistence.GetTrip(ID);
            if (TripData == null)
            {
                //TBD: Wrong Action, To be removed
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return TripData;
        }

        /// <summary>
        /// Create/Save a new trip
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        // POST: api/Trip
        public HttpResponseMessage Post([FromBody]Trip Value)
        {
            long ID = TripPersistence.SaveTrip(Value);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            // !Comment: This is used to set the location of the posted id in the header after the post is done.
            response.Headers.Location = new Uri(Request.RequestUri, string.Format("trip/{0}", ID));
            return response;
        }

        /// <summary>
        /// Modify a specific trip by id
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        // PUT: api/Trip/5
        public HttpResponseMessage Put(long ID, [FromBody]Trip Value)
        {
            Value.TripID = ID;
            bool Updated = TripPersistence.UpdateTrips(Value);
            HttpResponseMessage Response;

            if (Updated)
            {
                // TBD: This response should be removed in order to differentiate between connection faild and system specific faults
                // !Comment: return 402 -> record found with no content.
                Response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                // TBD: This response should be removed in order to differentiate between connection faild and system specific faults
                // !Comment: return 404 -> record not found.
                Response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Response;
        }

        /// <summary>
        /// Delete a specific trip by id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        // DELETE: api/Trip/5
        public HttpResponseMessage Delete(long ID)
        {
            bool Deleted = TripPersistence.DeleteTrip(ID);
            HttpResponseMessage Response;

            if (Deleted)
            {
                // TBD: This response should be removed in order to differentiate between connection faild and system specific faults
                // !Comment: return 402 -> record found with no content.
                Response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                // TBD: This response should be removed in order to differentiate between connection faild and system specific faults
                // !Comment: return 404 -> record not found.
                Response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Response;
        }
    }
}
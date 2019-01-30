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
        public ArrayList Get()
        {
            TripPersistence tp = new TripPersistence();
            return tp.getTrips();
        }

        /// <summary>
        /// Get a specific trip by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Trip/5
        public Trip Get(long id)
        {
            TripPersistence tp = new TripPersistence();
            Trip trip = tp.getTrip(id);
            if (trip == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return trip;
        }

        /// <summary>
        /// Create/Save a new trip
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/Trip
        public HttpResponseMessage Post([FromBody]Trip value)
        {
            TripPersistence tp = new TripPersistence();
            long id;
            id = tp.saveTrip(value);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            // !Comment: This is used to set the location of the posted id in the header after the post is done.
            response.Headers.Location = new Uri(Request.RequestUri, string.Format("trip/{0}", id));
            return response;
        }

        /// <summary>
        /// Modify a specific trip by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT: api/Trip/5
        public HttpResponseMessage Put(long id, [FromBody]Trip value)
        {
            TripPersistence tp = new TripPersistence();
            bool recordExisted = false;
            recordExisted = tp.updateTrip(id, value);

            HttpResponseMessage response;

            if (recordExisted)
            {
                // !Comment: return 402 -> record found with no content.
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                // !Comment: return 404 -> record not found.
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        /// <summary>
        /// Delete a specific trip by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Trip/5
        public HttpResponseMessage Delete(long id)
        {
            TripPersistence tp = new TripPersistence();
            bool recordExisted = false;

            recordExisted = tp.deleteTrip(id);

            HttpResponseMessage response;

            if (recordExisted)
            {
                // !Comment: return 402 -> record found with no content.
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                // !Comment: return 404 -> record not found.
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }
}
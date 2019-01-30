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
    public class ShipmentController : ApiController
    {
        /// <summary>
        /// Get all shipments
        /// </summary>
        /// <returns></returns>
        // GET: api/Shipment
        public ArrayList Get()
        {
            ShipmentPersistence sp = new ShipmentPersistence();
            return sp.getShipments();
        }

        /// <summary>
        /// Get a specific shipment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Shipment/5
        public Shipment Get(long id)
        {
            ShipmentPersistence tp = new ShipmentPersistence();
            Shipment shipment = tp.getShipment(id);
            if (shipment == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return shipment;
        }

        /// <summary>
        /// Create/Save a new shipment
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/Shipment
        public HttpResponseMessage Post([FromBody]Shipment value)
        {
            ShipmentPersistence sp = new ShipmentPersistence();
            long id;
            id = sp.saveShipment(value);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            // !Comment: This is used to set the location of the posted id in the header after the post is done.
            response.Headers.Location = new Uri(Request.RequestUri, string.Format("shipment/{0}", id));
            return response;
        }

        /// <summary>
        /// Modify a specific shipment by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT: api/Shipment/5
        public HttpResponseMessage Put(long id, [FromBody]Shipment value)
        {
            ShipmentPersistence sp = new ShipmentPersistence();
            bool recordExisted = false;
            recordExisted = sp.updateShipment(id, value);

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
        /// Delete a specific shipment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Shipment/5
        public HttpResponseMessage Delete(long id)
        {
            ShipmentPersistence sp = new ShipmentPersistence();
            bool recordExisted = false;

            recordExisted = sp.deleteShipment(id);

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
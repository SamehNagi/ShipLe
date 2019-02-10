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
        public List<Shipment> Get()
        {
            return ShipmentPersistence.GetShimpments();
        }

        /// <summary>
        /// Get a specific shipment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Shipment/5
        public Shipment Get(long ID)
        {
            Shipment ShipmentData = ShipmentPersistence.GetShipment(ID);


            return ShipmentData;
        }

        /// <summary>
        /// Create/Save a new shipment
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/Shipment
        public HttpResponseMessage Post([FromBody]Shipment Value)
        {
            HttpResponseMessage Response;
            long ID = ShipmentPersistence.SaveShipment(Value);

            if (ID != 0)
            {
                // !Comment: This is used to set the location of the posted id in the header after the post is done.
                Response = Request.CreateResponse(HttpStatusCode.Created);
                Response.Headers.Location = new Uri(Request.RequestUri, string.Format("shipment/{0}", ID));
            }
            else
            {
                // !Comment: This is used to set the location of the posted id in the header after the post is done.
                Response = Request.CreateResponse(HttpStatusCode.BadRequest);
                Response.Headers.Location = new Uri(Request.RequestUri, "Creating New Shipment Failed");
            }

            return Response;
        }

        /// <summary>
        /// Modify a specific shipment by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT: api/Shipment/5
        public HttpResponseMessage Put(long ID, [FromBody]Shipment Value)
        {
            Value.ShipmentID = ID;
            HttpResponseMessage Response;
            bool Updated = ShipmentPersistence.UpdateShipment(Value);


            if (Updated)
            {
                // TBD: Should not use these responses in order not to get confused with connection faild
                // !Comment: return 402 -> record found with no content.
                Response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                // TBD: Should not use these responses in order not to get confused with connection faild
                // !Comment: return 404 -> record not found.
                Response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Response;
        }

        /// <summary>
        /// Delete a specific shipment by id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        // DELETE: api/Shipment/5
        public HttpResponseMessage Delete(long ID)
        {
            HttpResponseMessage Response;
            bool Deleted = ShipmentPersistence.DeleteShipment(ID);

            if (Deleted)
            {
                // TBD: Should not use these responses in order not to get confused with connection faild
                // !Comment: return 402 -> record found with no content.
                Response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                // TBD: Should not use these responses in order not to get confused with connection faild
                // !Comment: return 404 -> record not found.
                Response = Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Response;
        }
    }
}
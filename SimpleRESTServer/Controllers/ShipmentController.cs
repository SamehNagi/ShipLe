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
        /// Get a specific shipment by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        // GET: api/Shipment/?ID=
        public Shipment Get(long ID)
        {
            Shipment ShipmentData = ShipmentPersistence.GetShipment(ID);

            return ShipmentData;
        }

        /// <summary>
        /// Create/Save a new shipment
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        // POST: api/Shipment
        public long Post([FromBody]Shipment Value)
        {
            long ID = ShipmentPersistence.SaveShipment(Value);

            return ID;
        }

        /// <summary>
        /// Modify a specific shipment by id
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        // PUT: api/Shipment/?ID=
        public bool Put(long ID, [FromBody]Shipment Value)
        {
            Value.ShipmentID = ID;
            bool Updated = ShipmentPersistence.UpdateShipment(Value);

            return Updated;
        }

        /// <summary>
        /// Delete a specific shipment by id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        // DELETE: api/Shipment/?ID=
        public bool Delete(long ID)
        {
            bool Deleted = ShipmentPersistence.DeleteShipment(ID);

            return Deleted;
        }
    }
}
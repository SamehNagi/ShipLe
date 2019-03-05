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
    public class UserController : ApiController
    {
        // To use the documentation from XML documentation file, use the ///

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        // GET: api/User
        public List<User> Get()
        {
            return UserPersistence.GetUsers();
        }

        /// <summary>
        /// Get a specific user by his\her username
        /// </summary>
        /// <param name="Username">Username to check for</param>
        /// <returns></returns>
        // GET: api/User/?Username=
        public User Get(string Username)
        {
            User UserData = UserPersistence.GetUser(Username);
            return UserData;
        }

        /// <summary>
        /// Create/Save a new user
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        // POST: api/User
        public long Post([FromBody]User Value)
        {
            long ID = UserPersistence.SaveUser(Value);

            return ID;
        }

        /// <summary>
        /// Modify a specific user by username
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        // PUT: api/User/?ID=
        public bool Put(long ID, [FromBody]User Value)
        {
            Value.UserID = ID;
            bool Updated = UserPersistence.UpdateUser(Value);

            return Updated;
        }

        /// <summary>
        /// Delete a specific user by id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        // DELETE: api/User/?ID=
        public bool Delete(long ID)
        {
            bool Deleted = UserPersistence.DeleteUser(ID);

            return Deleted;
        }
    }
}

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
            User udtResult = new User();
            User ResultUser = UserPersistence.GetUser(Username);
            if (Request.Headers.Contains("Password"))
            {
                string Password = Request.Headers.GetValues("Password").First();
            }

            if (ResultUser != null)
            {
                udtResult.UserID    = ResultUser.UserID;
                udtResult.Username  = ResultUser.Username;
                udtResult.FirstName = ResultUser.FirstName;
                udtResult.LastName  = ResultUser.LastName;
                udtResult.Email     = ResultUser.Email;
                udtResult.Password  = ResultUser.Password;
            }
            else
            {
                /*To be Changed*/
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return ResultUser;
        }

        /// <summary>
        /// Create/Save a new user
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        // POST: api/User
        public HttpResponseMessage Post([FromBody]User Value)
        {
            long id;
            id = UserPersistence.SaveUser(Value);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            // !Comment: This is used to set the location of the posted id in the header after the post is done.
            response.Headers.Location = new Uri(Request.RequestUri, string.Format("user/{0}", id));
            return response;
        }

        /// <summary>
        /// Modify a specific user by username
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        // PUT: api/User/?UserID=
        public HttpResponseMessage Put(long UserID, [FromBody]User Value)
        {
            bool recordExisted = false;
            User UserData = new User() 
            { 
                UserID    = UserID,
                Username  = Value.Username , 
                FirstName = Value.FirstName, 
                LastName  = Value.LastName, 
                Email     = Value.Email, 
                Password  = Value.Password
            };

            recordExisted = UserPersistence.UpdateUser(UserData);


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
        /// Delete a specific user by id
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        // DELETE: api/User/?UserID=
        public HttpResponseMessage Delete(long UserID)
        {
            bool recordExisted = false;
            User UserData = new User() { UserID = UserID };

            recordExisted = UserPersistence.DeleteUser(UserID);

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

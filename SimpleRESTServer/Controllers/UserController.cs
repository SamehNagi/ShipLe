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
        public ArrayList Get()
        {
            UserPersistence up = new UserPersistence();
            return up.getUsers();
        }

        /// <summary>
        /// Get a specific user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/User/5
        public User Get(string username)
        {
            User udtResult = new User();
            if ("eng_same7" == username)
            {
                udtResult.UserID = 1;
                udtResult.Username = username;
                udtResult.FirstName = "Sameh";
                udtResult.LastName = "Nagi";
                udtResult.Email = udtResult.FirstName + "." + udtResult.LastName + "@valeo.com";
                udtResult.Password = "123456";
            }
            else
            {
                udtResult.UserID = 100;
                udtResult.Username = username;
                udtResult.FirstName = "Ahmed";
                udtResult.LastName = "Abdelmaksoud";
                udtResult.Email = udtResult.FirstName + "." + udtResult.LastName + "@valeo.com";
                udtResult.Password = "654321";
            }

            return udtResult;
            UserPersistence up = new UserPersistence();
            User user = up.getUser(username);
            if (user == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return user;
        }

        /// <summary>
        /// Create/Save a new user
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/User
        public HttpResponseMessage Post([FromBody]User value)
        {
            UserPersistence up = new UserPersistence();
            long id;
            id = up.saveUser(value);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            // !Comment: This is used to set the location of the posted id in the header after the post is done.
            response.Headers.Location = new Uri(Request.RequestUri, string.Format("user/{0}", id));
            return response;
        }

        /// <summary>
        /// Modify a specific user by username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT: api/User/eng_same7
        public HttpResponseMessage Put([FromUri]string username, [FromBody]User value)
        {
            UserPersistence up = new UserPersistence();
            bool recordExisted = false;
            recordExisted = up.updateUser(username, value);

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
        /// <param name="username"></param>
        /// <returns></returns>
        // DELETE: api/User/5
        public HttpResponseMessage Delete(string username)
        {
            UserPersistence up = new UserPersistence();
            bool recordExisted = false;

            recordExisted = up.deleteUser(username);

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

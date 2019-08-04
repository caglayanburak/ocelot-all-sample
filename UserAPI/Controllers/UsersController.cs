using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserContext _context;
        private IHttpContextAccessor _httpContextAccessor;
        public UsersController(IUserContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Models.User> Get(string id)
        {
            Console.WriteLine("UserAPI CorrelationId: " + _httpContextAccessor.HttpContext.Request.Headers["OcRequestId"]);
            return _context.Users.Find<User>(new BsonDocument { { "_id", new ObjectId(id) } }).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] User user)
        {
            this._context.Users.InsertOne(user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

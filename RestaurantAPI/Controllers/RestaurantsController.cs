using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private static int _count = 0;
        private IRestaurantContext _restaurantContext;
        public RestaurantsController(IRestaurantContext restaurantContext)
        {
            _restaurantContext = restaurantContext;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> Get()
        {
            _count++;
            System.Console.WriteLine($"get...{_count}");
            if (_count <= 3)
            {
                Thread.Sleep(2000);
            }
             var result = this._restaurantContext.Restaurants.Find<Restaurant>(new BsonDocument()).ToList();

             return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Restaurant> Get(int id)
        {
             return _restaurantContext.Restaurants.Find<Restaurant>(new BsonDocument { { "_id", id } }).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Restaurant value)
        {
            this._restaurantContext.Restaurants.InsertOne(value);
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

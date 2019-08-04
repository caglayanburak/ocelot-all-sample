using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using ReservationApi.Models;

namespace ReservationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private IReservationContext _reservationContext;
        IHttpContextAccessor _httpContextAccessor;
        public ReservationsController(IReservationContext reservationContext, IHttpContextAccessor httpContextAccessor)
        {
            _reservationContext = reservationContext;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> Get()
        {
            var result = this._reservationContext.Reservations.Find<Reservation>(new BsonDocument()).ToList();

            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(string id)
        {
            Console.WriteLine("id:" + id);
            return _reservationContext.Reservations.Find<Reservation>(new BsonDocument { { "_id", new ObjectId(id) } }).FirstOrDefault();
        }

        [Authorize]
        [HttpGet("reservation-byuserid/{userId}")]
        public ActionResult<Reservation> GetByUserId(string userId)
        {
            Console.WriteLine("Reservation CoorelationId:" + _httpContextAccessor.HttpContext.Request.Headers["OcRequestId"]);
            return _reservationContext.Reservations.Find<Reservation>(new BsonDocument { { "userId", userId } }).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Reservation> Post([FromBody] Reservation reservation)
        {
            this._reservationContext.Reservations.InsertOne(reservation);
            return reservation;
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<ActionResult<Reservation>> Put([FromBody] Reservation reservation)
        {
            var result = await this._reservationContext.Reservations.FindOneAndUpdateAsync(
    c => c.id == reservation.id, // find this match
    Builders<Reservation>.Update
            //.Set(s => s.synonym, entity.synonym)
            .Set(s => s.restaurantId, reservation.restaurantId)
            .Set(s => s.userId, reservation.userId)
            .Set(s => s.time, reservation.time));

            return result;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReservationApi.Models
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        public string time { get; set; }
        public string userId { get; set; }
        public int restaurantId { get; set; }
    }
}
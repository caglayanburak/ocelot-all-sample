using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
    }
}
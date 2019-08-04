using MongoDB.Bson;
using MongoDB.Driver;

namespace RestaurantAPI.Models
{
    public class RestaurantContext : IRestaurantContext
    {
        private IMongoDatabase _database = null;
        public RestaurantContext(string connectionString, string database)
        {
            IMongoClient client = new MongoClient(connectionString);
            if (client != null)
            {
                _database = client.GetDatabase(database);
            }
        }

        public IMongoCollection<BsonDocument> GetDocument(string name)
        {
            return _database.GetCollection<BsonDocument>(name);
        }

        public IMongoCollection<Restaurant> Restaurants
        {
            get
            {
                return _database.GetCollection<Restaurant>("restaurants");
            }
        }

    }

    public interface IRestaurantContext
    {
        IMongoCollection<Restaurant> Restaurants { get; }
    }
}
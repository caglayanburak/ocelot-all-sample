using MongoDB.Bson;
using MongoDB.Driver;

namespace UserAPI.Models
{
    public class UserContext : IUserContext
    {
        private IMongoDatabase _database = null;
        public UserContext(string connectionString, string database)
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

        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("users");
            }
        }
    }
}
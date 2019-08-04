using MongoDB.Bson;
using MongoDB.Driver;

namespace ReservationApi.Models
{
    public class ReservationContext : IReservationContext
    {
        private IMongoDatabase _database = null;
        public ReservationContext(string connectionString, string database)
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

        public IMongoCollection<Reservation> Reservations
        {
            get
            {
                return _database.GetCollection<Reservation>("reservations");
            }
        }

    }

    public interface IReservationContext
    {
        IMongoCollection<Reservation> Reservations { get; }
    }
}
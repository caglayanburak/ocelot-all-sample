using MongoDB.Driver;

namespace UserAPI.Models
{
    public interface IUserContext
    {
        IMongoCollection<User> Users { get; }
    }
}
using KelaniSTEAM_Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using BCrypt.Net;

namespace KelaniSTEAM_Backend.Services;

public class AuthService
{
    private readonly IMongoCollection<User> _usersCollection;

    public AuthService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _usersCollection = mongoDatabase.GetCollection<User>("User");
    }

    public User Authenticate(string username, string password)
    {
        var user = _usersCollection.Find(u => u.UserName == username).FirstOrDefault();
        if (user == null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        return user;
    }

    public void CreateUser(User user)
    {
        _usersCollection.InsertOne(user);
    }

    public User GetUserByUsername(string username)
    {
        return _usersCollection.Find(u => u.UserName == username).FirstOrDefault();
    }

    public List<User> GetAllUsers()
        {
            return _usersCollection.Find(_ => true).SortByDescending(u => u.CreatedDate).ToList();
        }

        public void DeleteUserById(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);

            _usersCollection.DeleteOne(filter);
        }


}

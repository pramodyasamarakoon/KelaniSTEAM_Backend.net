using KelaniSTEAM_Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KelaniSTEAM_Backend.Services;

public class AlbumsService
{
    private readonly IMongoCollection<Album> _albumsCollection;

    public AlbumsService(
        IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _albumsCollection = mongoDatabase.GetCollection<Album>("Albums");
    }

    // Get All
    public async Task<List<Album>> GetAsync() =>
        await _albumsCollection.Find(_ => true).ToListAsync();

    // Get by Id
    public async Task<Album?> GetAsync(string id) =>
        await _albumsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    // Create New
    public async Task CreateAsync(Album newAlbum) =>
        await _albumsCollection.InsertOneAsync(newAlbum);

    // Update by Id
    public async Task UpdateAsync(string id, Album updatedAlbum) =>
        await _albumsCollection.ReplaceOneAsync(x => x.Id == id, updatedAlbum);

// Delete by Id
    public async Task RemoveAsync(string id) =>
        await _albumsCollection.DeleteOneAsync(x => x.Id == id);
}

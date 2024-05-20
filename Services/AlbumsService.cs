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

        _albumsCollection = mongoDatabase.GetCollection<Album>(
            databaseSettings.Value.AlbumsCollectionName);
    }

    public async Task<List<Album>> GetAsync() =>
        await _albumsCollection.Find(_ => true).ToListAsync();

    public async Task<Album?> GetAsync(string id) =>
        await _albumsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Album newAlbum) =>
        await _albumsCollection.InsertOneAsync(newAlbum);

    public async Task UpdateAsync(string id, Album updatedAlbum) =>
        await _albumsCollection.ReplaceOneAsync(x => x.Id == id, updatedAlbum);

    public async Task RemoveAsync(string id) =>
        await _albumsCollection.DeleteOneAsync(x => x.Id == id);
}

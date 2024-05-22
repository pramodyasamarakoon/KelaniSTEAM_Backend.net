using KelaniSTEAM_Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KelaniSTEAM_Backend.Services;

public class TshirtOrderService
{
    private readonly IMongoCollection<TshirtOrder> _tshirtOrderCollection;

    public TshirtOrderService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _tshirtOrderCollection = mongoDatabase.GetCollection<TshirtOrder>("TshirtOrders");
    }

    public async Task<List<TshirtOrder>> GetAsync() =>
        await _tshirtOrderCollection.Find(_ => true)
                                   .Sort(Builders<TshirtOrder>.Sort.Descending(a => a.CreatedDate))
                                   .ToListAsync();

    public async Task<TshirtOrder?> GetAsync(string id) =>
        await _tshirtOrderCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TshirtOrder newTshirtOrder) =>
        await _tshirtOrderCollection.InsertOneAsync(newTshirtOrder);

    public async Task UpdateStatusAsync(string id, bool status)
        {
            var filter = Builders<TshirtOrder>.Filter.Eq(b => b.Id, id);
            var update = Builders<TshirtOrder>.Update.Set(b => b.Status, status);
            await _tshirtOrderCollection.UpdateOneAsync(filter, update);
        }

    public async Task RemoveAsync(string id) =>
        await _tshirtOrderCollection.DeleteOneAsync(x => x.Id == id);

}
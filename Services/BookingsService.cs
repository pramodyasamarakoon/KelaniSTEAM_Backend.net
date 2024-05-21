using KelaniSTEAM_Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KelaniSTEAM_Backend.Services;

public class BookingService
{
    private readonly IMongoCollection<Booking> _bookingCollection;

    public BookingService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _bookingCollection = mongoDatabase.GetCollection<Booking>("Bookings");
    }

    public async Task<List<Booking>> GetAsync() =>
        await _bookingCollection.Find(_ => true).ToListAsync();

    public async Task<Booking?> GetAsync(string id) =>
        await _bookingCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Booking newBooking) =>
        await _bookingCollection.InsertOneAsync(newBooking);

    public async Task UpdateAsync(string id, Booking updatedBooking) =>
        await _bookingCollection.ReplaceOneAsync(x => x.Id == id, updatedBooking);

    public async Task RemoveAsync(string id) =>
        await _bookingCollection.DeleteOneAsync(x => x.Id == id);
}

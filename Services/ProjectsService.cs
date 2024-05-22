using KelaniSTEAM_Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KelaniSTEAM_Backend.Services;

public class ProjectService
{
     private readonly IMongoCollection<Project> _projectCollection;

     public ProjectService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _projectCollection = mongoDatabase.GetCollection<Project>("Projects");
    }

    public async Task<List<Project>> GetAsync() =>
        // await _bookingCollection.Find(_ => true).ToListAsync();
        await _projectCollection.Find(_ => true)
                                   .Sort(Builders<Project>.Sort.Descending(a => a.CreatedDate))
                                   .Limit(10)
                                   .ToListAsync();

    public async Task<Project?> GetAsync(string id) =>
        await _projectCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Project newProject) =>
        await _projectCollection.InsertOneAsync(newProject);

    public async Task RemoveAsync(string id) =>
        await _projectCollection.DeleteOneAsync(x => x.Id == id);

}
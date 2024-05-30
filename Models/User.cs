using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace KelaniSTEAM_Backend.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string UserName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    [BsonRepresentation(BsonType.String)]
    [JsonIgnore]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
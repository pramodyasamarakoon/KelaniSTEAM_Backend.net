using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace KelaniSTEAM_Backend.Models;

public class Album
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string AlbumName { get; set; } = null!;

    public string AlbumLink { get; set; } = null!;

    public string[] PhotographedBy { get; set; } = null!;
    public string[] EditedBy { get; set; } = null!;
    public string[] ImageUrls { get; set; } = null!;
    public DateTime AlbumDate { get; set; } = DateTime.Now;

}
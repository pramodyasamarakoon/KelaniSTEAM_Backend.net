using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace KelaniSTEAM_Backend.Models;

public class Project
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ProjectDescription { get; set; } = null!;
    public string CoverImageUrl { get; set; } = null!;
    public string[] PreviewImageUrls { get; set; } = null!;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
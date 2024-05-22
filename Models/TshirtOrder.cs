using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace KelaniSTEAM_Backend.Models;

public class TshirtOrder
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public string Department { get; set; } = null!;
    public string StudentNumber { get; set; } = null!;
    public string ContactNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Size { get; set; } = null!;
    public string PaymentMethod { get; set; } = null!;
    public string PaymentAmount { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public Boolean? Status { get; set; } = false;
    public DateTime CreatedDate { get; set; } = DateTime.Now;

}
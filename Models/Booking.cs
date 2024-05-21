using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace KelaniSTEAM_Backend.Models;

public class Booking
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public string University { get; set; } = null!;
    public string Event { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Mobile { get; set; } = null!;
    public string? Description { get; set; }
    public string Expectation { get; set; } = null!;
    public string WhatWeGet { get; set; } = null!;
    public string ProposalLink { get; set; } = null!;
    public DateTime BookingDate { get; set; } = DateTime.Now;
    public string? Other { get; set; }
    public Boolean? Status { get; set; }

}
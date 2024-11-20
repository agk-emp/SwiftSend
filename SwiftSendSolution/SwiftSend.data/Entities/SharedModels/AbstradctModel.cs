using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SwiftSend.data.Entities.SharedModels
{
    public abstract class AbstradctModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? UpdatedAt { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? CreatedBy { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? UpdatedBy { get; set; }
    }
}

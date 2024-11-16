using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SwiftSend.data.Entities.Identity
{
    public class UserRefreshToken
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("jwtToken")]
        public string JWTToken { get; set; }

        [BsonElement("refreshToken")]
        public string RefreshToken { get; set; }

        [BsonElement("expiration")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Expiration { get; set; }

        [BsonElement("isRevoked")]
        public bool IsRevoked { get; set; }

        [BsonElement("isUsed")]
        public bool IsUsed { get; set; }

        public UserRefreshToken()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}

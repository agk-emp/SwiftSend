using MongoDB.Bson.Serialization.Attributes;
using SwiftSend.data.Entities.SharedModels;

namespace SwiftSend.data.Entities
{
    public class Restaurant : AbstradctModel
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("meals")]
        public List<Meal> Meals { get; set; }



        [BsonElement("schedule")]
        public Schedule Schedule { get; set; }

        public Restaurant()
        {
            Meals = new();
        }
    }
}

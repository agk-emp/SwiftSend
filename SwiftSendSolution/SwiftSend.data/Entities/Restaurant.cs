using SwiftSend.data.Entities.SharedModels;

namespace SwiftSend.data.Entities
{
    public class Restaurant : AbstradctModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Meal> Meals { get; set; }

        public Restaurant()
        {
            Meals = new();
        }
    }
}

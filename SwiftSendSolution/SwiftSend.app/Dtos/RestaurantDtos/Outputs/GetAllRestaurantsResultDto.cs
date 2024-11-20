using SwiftSend.app.Dtos.RestaurantDtos.Inputs;

namespace SwiftSend.app.Dtos.RestaurantDtos.Outputs
{
    public class GetAllRestaurantsResultDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public ScheduleDto Schedule { get; set; }

    }
}

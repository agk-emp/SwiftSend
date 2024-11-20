using SwiftSend.app.Dtos.RestaurantDtos.Inputs;

namespace SwiftSend.app.Dtos.RestaurantDtos.Outputs
{
    //I created separately because I might want something
    //when getting a single restaurant not in the list and vice versa
    public class GetRestaurantByIdResultDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public ScheduleDto Schedule { get; set; }
    }
}

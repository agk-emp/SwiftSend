using System.ComponentModel.DataAnnotations;

namespace SwiftSend.app.Dtos.RestaurantDtos.Inputs
{
    public class CreateRestaurantDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }

        public ScheduleDto Schedule { get; set; }
    }
}

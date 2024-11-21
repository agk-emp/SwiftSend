using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SwiftSend.app.Dtos.RestaurantDtos.Inputs
{
    //I want the the updating of the schedule to be separated so I did not include it in here
    public class UpdateRestaurantDto
    {
        [FromRoute]
        public string Id { get; set; }
        [FromBody]
        public UpdateRestaurantBodyDto RestaurantUpdatingBody { get; set; }

        public class UpdateRestaurantBodyDto
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string Location { get; set; }
        }
    }
}

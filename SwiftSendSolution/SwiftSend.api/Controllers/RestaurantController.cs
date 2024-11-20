using Microsoft.AspNetCore.Mvc;
using SwiftSend.app.Abstracts.Repositories;
using SwiftSend.app.Common;
using SwiftSend.app.Dtos.RestaurantDtos.Inputs;
using SwiftSend.app.Dtos.RestaurantDtos.Outputs;

namespace SwiftSend.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantServices _restaurantServices;

        public RestaurantController(IRestaurantServices restaurantServices)
        {
            _restaurantServices = restaurantServices;
        }

        [HttpPost(nameof(CreateRestaurant))]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto requestDto)
        {
            await _restaurantServices.CreateRestaurant(requestDto);
            return Created();
        }

        [HttpGet(nameof(GetAllRestaurants))]
        public async Task<ActionResult<PagedResult<GetAllRestaurantsResultDto>>>
            GetAllRestaurants([FromQuery] GetAllRestaurantsRequestDto requestDto)
        {
            var result = await _restaurantServices.GetAllRestaurantsPaged(requestDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<>>
    }
}

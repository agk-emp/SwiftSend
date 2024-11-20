using SwiftSend.app.Common;
using SwiftSend.app.Dtos.RestaurantDtos.Inputs;
using SwiftSend.app.Dtos.RestaurantDtos.Outputs;

namespace SwiftSend.app.Abstracts.Repositories
{
    public interface IRestaurantServices
    {
        Task CreateRestaurant(CreateRestaurantDto requestDto);
        Task<PagedResult<GetAllRestaurantsResultDto>> GetAllRestaurantsPaged(GetAllRestaurantsRequestDto requestDto);
    }
}

using SwiftSend.app.Common;
using SwiftSend.app.Dtos.RestaurantDtos.Inputs;
using SwiftSend.app.Dtos.RestaurantDtos.Outputs;

namespace SwiftSend.app.Abstracts.Repositories
{
    public interface IRestaurantServices
    {
        Task CreateRestaurant(CreateRestaurantDto requestDto);
        Task<PagedResult<GetAllRestaurantsResultDto>> GetAllRestaurantsPaged(GetAllRestaurantsRequestDto requestDto);
        Task<GetRestaurantByIdResultDto> GetRestaurantById(GetRestaurantByIdRequestDto requestDto);
        Task DeleteRestaurant(DeleteRestaurantDto requestDto);
        Task UpdateRestaurant(UpdateRestaurantDto requestDto);
    }
}

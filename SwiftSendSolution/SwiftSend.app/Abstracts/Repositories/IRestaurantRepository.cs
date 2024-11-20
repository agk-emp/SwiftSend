using SwiftSend.data.Entities;

namespace SwiftSend.app.Abstracts.Repositories
{
    public interface IRestaurantRepository
    {
        Task AddRestaurant(Restaurant restaurant);
        Task<List<Restaurant>> GetAll();
        Task<Restaurant> GetById(string id);
        Task RemoveRestaurant(string id);
        Task Update(string id, Restaurant restaurant);
    }
}

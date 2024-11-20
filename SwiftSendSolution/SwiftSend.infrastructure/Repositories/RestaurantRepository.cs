using MongoDB.Driver;
using SwiftSend.app.Abstracts.Repositories;
using SwiftSend.data.Entities;
using SwiftSend.infrastructure.Context;

namespace SwiftSend.infrastructure.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly AppDbContext _appDbContext;

        public RestaurantRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddRestaurant(Restaurant restaurant)
        {
            await _appDbContext.Restaurants.InsertOneAsync(restaurant);
        }

        public async Task<List<Restaurant>> GetAll()
        {
            var result = await _appDbContext.Restaurants.Find(res => true).ToListAsync();
            return result ?? Enumerable.Empty<Restaurant>().ToList();
        }

        public async Task<Restaurant> GetById(string id)
        {
            var restaurant = await _appDbContext.Restaurants.Find(GetByIdFilter(id)).FirstOrDefaultAsync();
            if (restaurant is null)
            {
                throw new Exception("The restaurant was not found");
            }
            return restaurant;
        }

        public async Task RemoveRestaurant(string id)
        {
            await GetById(id);
            await _appDbContext.Restaurants.DeleteOneAsync(GetByIdFilter(id));
        }

        public async Task Update(string id, Restaurant restaurant)
        {
            var restaurantToUpdate = await GetById(id);
            await _appDbContext.Restaurants.ReplaceOneAsync(GetByIdFilter(id), restaurant);
        }

        #region private methods

        private static FilterDefinition<Restaurant> GetByIdFilter(string id)
        {
            return Builders<Restaurant>.Filter.Eq(r => r.Id, id);
        }
        #endregion
    }
}

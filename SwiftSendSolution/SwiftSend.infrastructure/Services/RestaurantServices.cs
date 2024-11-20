using SwiftSend.app.Abstracts.Repositories;
using SwiftSend.app.Common;
using SwiftSend.app.Dtos.RestaurantDtos.Inputs;
using SwiftSend.app.Dtos.RestaurantDtos.Outputs;
using SwiftSend.app.Helpers;
using SwiftSend.data.Entities;

namespace SwiftSend.infrastructure.Services
{
    public class RestaurantServices : IRestaurantServices
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantServices(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task CreateRestaurant(CreateRestaurantDto requestDto)
        {
            var restaurantToCreate = new Restaurant()
            {
                Name = requestDto.Name,
                Location = requestDto.Location,
                Schedule = new Schedule()
                {
                    Saturday = requestDto.Schedule.Saturday,
                    Sunday = requestDto.Schedule.Sunday,
                    Friday = requestDto.Schedule.Friday,
                    Monday = requestDto.Schedule.Monday,
                    Tuesday = requestDto.Schedule.Tuesday,
                    Thursday = requestDto.Schedule.Thursday,
                    Wednesday = requestDto.Schedule.Wednesday,
                    StartTime = requestDto.Schedule.StartTime,
                    EndTime = requestDto.Schedule.EndTime,
                }
            };

            await _restaurantRepository.AddRestaurant(restaurantToCreate);
        }

        public async Task<PagedResult<GetAllRestaurantsResultDto>> GetAllRestaurantsPaged(GetAllRestaurantsRequestDto requestDto)
        {
            var restaurants = await _restaurantRepository.GetAll();
            if (restaurants.Any())
            {
                return restaurants.Select(res => new GetAllRestaurantsResultDto()
                {
                    Name = res.Name,
                    Location = res.Location,
                    Schedule = new ScheduleDto()
                    {
                        Saturday = res.Schedule.Saturday,
                        Sunday = res.Schedule.Sunday,
                        Monday = res.Schedule.Monday,
                        Tuesday = res.Schedule.Tuesday,
                        Wednesday = res.Schedule.Wednesday,
                        Thursday = res.Schedule.Thursday,
                        Friday = res.Schedule.Friday,
                        StartTime = res.Schedule.StartTime,
                        EndTime = res.Schedule.EndTime,
                    }
                }).ToPaginatedResult(requestDto.PageNumber,
                    requestDto.PageSize);
            }
            return new PagedResult<GetAllRestaurantsResultDto>();
        }
    }
}

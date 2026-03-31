using _4_4_HttpClient.DTOs;

namespace _4_4_HttpClient.Services
{
    public class CarService : ICarService
    {
        public CarService()
        {
            
        }

        public Task<Guid> CreateCarAsync(CarDto createDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCarAsync(Guid carId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetDto>> GetAllCarsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CarDto> GetCarByIdAsync(Guid carId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCarAsync(Guid carId, UpdateDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}

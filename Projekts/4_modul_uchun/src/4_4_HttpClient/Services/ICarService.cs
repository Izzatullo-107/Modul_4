using _4_4_HttpClient.DTOs;

namespace _4_4_HttpClient.Services
{
    public interface ICarService
    {
        public Task<List<GetDto>> GetAllCarsAsync();
        public Task<CarDto> GetCarByIdAsync(Guid carId);
        public Task<Guid> CreateCarAsync(CarDto createDto);
        public Task<bool> UpdateCarAsync(Guid carId, UpdateDto updateDto);
        public Task<bool> DeleteCarAsync(Guid carId);
    }
}

using AutomationRadar.Model.Entities;

namespace AutomationRadar.Business.Interfaces
{
    public interface ICareerTransitionRepository
    {
        Task<IEnumerable<CareerTransition>> GetAllAsync();
        Task<CareerTransition?> GetByIdAsync(int id);
        Task<IEnumerable<CareerTransition>> GetByOriginAsync(int fromOccupationId);
        Task<IEnumerable<CareerTransition>> GetByDestinationAsync(int toOccupationId);
        Task AddAsync(CareerTransition transition);
        Task UpdateAsync(CareerTransition transition);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}

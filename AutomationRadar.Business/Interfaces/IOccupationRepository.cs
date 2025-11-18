using AutomationRadar.Model.Entities;

namespace AutomationRadar.Business.Interfaces
{
    public interface IOccupationRepository
    {
        Task<IEnumerable<Occupation>> GetAllAsync();
        Task<Occupation?> GetByIdAsync(int id);
        Task<IEnumerable<Occupation>> SearchByNameAsync(string name);
        Task AddAsync(Occupation occupation);
        Task UpdateAsync(Occupation occupation);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}

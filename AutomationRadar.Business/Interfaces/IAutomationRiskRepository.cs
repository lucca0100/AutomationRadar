using AutomationRadar.Model.Entities;

namespace AutomationRadar.Business.Interfaces
{
    public interface IAutomationRiskRepository
    {
        Task<IEnumerable<AutomationRisk>> GetAllAsync();
        Task<AutomationRisk?> GetByIdAsync(int id);
        Task<IEnumerable<AutomationRisk>> GetByOccupationIdAsync(int occupationId);
        Task AddAsync(AutomationRisk automationRisk);
        Task UpdateAsync(AutomationRisk automationRisk);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}

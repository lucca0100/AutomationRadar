using AutomationRadar.Business.Interfaces;
using AutomationRadar.Data.Context;
using AutomationRadar.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomationRadar.Business.Repositories
{
    public class AutomationRiskRepository : IAutomationRiskRepository
    {
        private readonly AppDbContext _context;

        public AutomationRiskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AutomationRisk>> GetAllAsync()
        {
            return await _context.AutomationRisks
                .AsNoTracking()
                .Include(r => r.Occupation)
                .OrderBy(r => r.Occupation!.Name)
                .ToListAsync();
        }

        public async Task<AutomationRisk?> GetByIdAsync(int id)
        {
            return await _context.AutomationRisks
                .AsNoTracking()
                .Include(r => r.Occupation)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<AutomationRisk>> GetByOccupationIdAsync(int occupationId)
        {
            return await _context.AutomationRisks
                .AsNoTracking()
                .Where(r => r.OccupationId == occupationId)
                .OrderByDescending(r => r.RiskLevel)
                .ToListAsync();
        }

        public async Task AddAsync(AutomationRisk automationRisk)
        {
            _context.AutomationRisks.Add(automationRisk);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutomationRisk automationRisk)
        {
            _context.AutomationRisks.Update(automationRisk);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.AutomationRisks.FindAsync(id);
            if (entity != null)
            {
                _context.AutomationRisks.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.AutomationRisks.AnyAsync(r => r.Id == id);
        }
    }
}

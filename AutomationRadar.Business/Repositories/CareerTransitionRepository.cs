using AutomationRadar.Business.Interfaces;
using AutomationRadar.Data.Context;
using AutomationRadar.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomationRadar.Business.Repositories
{
    public class CareerTransitionRepository : ICareerTransitionRepository
    {
        private readonly AppDbContext _context;

        public CareerTransitionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CareerTransition>> GetAllAsync()
        {
            return await _context.CareerTransitions
                .AsNoTracking()
                .Include(t => t.FromOccupation)
                .Include(t => t.ToOccupation)
                .OrderBy(t => t.FromOccupation!.Name)
                .ToListAsync();
        }

        public async Task<CareerTransition?> GetByIdAsync(int id)
        {
            return await _context.CareerTransitions
                .AsNoTracking()
                .Include(t => t.FromOccupation)
                .Include(t => t.ToOccupation)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<CareerTransition>> GetByOriginAsync(int fromOccupationId)
        {
            return await _context.CareerTransitions
                .AsNoTracking()
                .Where(t => t.FromOccupationId == fromOccupationId)
                .Include(t => t.FromOccupation)
                .Include(t => t.ToOccupation)
                .OrderBy(t => t.Priority)
                .ToListAsync();
        }

        public async Task<IEnumerable<CareerTransition>> GetByDestinationAsync(int toOccupationId)
        {
            return await _context.CareerTransitions
                .AsNoTracking()
                .Where(t => t.ToOccupationId == toOccupationId)
                .Include(t => t.FromOccupation)
                .Include(t => t.ToOccupation)
                .OrderBy(t => t.Priority)
                .ToListAsync();
        }

        public async Task AddAsync(CareerTransition transition)
        {
            _context.CareerTransitions.Add(transition);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CareerTransition transition)
        {
            _context.CareerTransitions.Update(transition);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.CareerTransitions.FindAsync(id);
            if (entity != null)
            {
                _context.CareerTransitions.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.CareerTransitions.AnyAsync(t => t.Id == id);
        }
    }
}

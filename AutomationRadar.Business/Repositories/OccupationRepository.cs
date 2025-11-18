using AutomationRadar.Business.Interfaces;
using AutomationRadar.Data.Context;
using AutomationRadar.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomationRadar.Business.Repositories
{
    public class OccupationRepository : IOccupationRepository
    {
        private readonly AppDbContext _context;

        public OccupationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Occupation>> GetAllAsync()
        {
            return await _context.Occupations
                .AsNoTracking()
                .OrderBy(o => o.Name)
                .ToListAsync();
        }

        public async Task<Occupation?> GetByIdAsync(int id)
        {
            return await _context.Occupations
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Occupation>> SearchByNameAsync(string name)
        {
            name = name?.Trim() ?? string.Empty;

            return await _context.Occupations
                .AsNoTracking()
                .Where(o => o.Name.ToLower().Contains(name.ToLower()))
                .OrderBy(o => o.Name)
                .ToListAsync();
        }

        public async Task AddAsync(Occupation occupation)
        {
            _context.Occupations.Add(occupation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Occupation occupation)
        {
            _context.Occupations.Update(occupation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var occupation = await _context.Occupations.FindAsync(id);
            if (occupation != null)
            {
                _context.Occupations.Remove(occupation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Occupations.AnyAsync(o => o.Id == id);
        }
    }
}

using Microsoft.EntityFrameworkCore;

public class SalonService : ISalonService
{
    private readonly ApplicationDbContext _context;

    public SalonService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Salon>> GetAllSalonsAsync()
    {
        return await _context.Salons.Include(s => s.Employees).Include(s => s.Services).ToListAsync();
    }

    public async Task<Salon> GetSalonByIdAsync(int id)
    {
        return await _context.Salons.Include(s => s.Employees).Include(s => s.Services).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddSalonAsync(Salon salon)
    {
        _context.Salons.Add(salon);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateSalonAsync(Salon salon)
    {
        _context.Salons.Update(salon);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSalonAsync(int id)
    {
        var salon = await _context.Salons.FindAsync(id);
        if (salon != null)
        {
            _context.Salons.Remove(salon);
            await _context.SaveChangesAsync();
        }
    }
}

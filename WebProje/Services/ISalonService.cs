public interface ISalonService
{
    Task<List<Salon>> GetAllSalonsAsync();
    Task<Salon> GetSalonByIdAsync(int id);
    Task AddSalonAsync(Salon salon);
    Task UpdateSalonAsync(Salon salon);
    Task DeleteSalonAsync(int id);
}

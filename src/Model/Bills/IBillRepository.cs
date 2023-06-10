namespace Model.Bills
{
    public interface IBillRepository
    {
        Task<Bill> AddAsync(Bill bill);
        Task UpdateAsync(Bill bill);
        Task<IReadOnlyList<Bill>> GetAllAsync();
        Task<Bill?> GetByAsync(string code);
        Task DeleteAsync(Bill bill);
    }
}

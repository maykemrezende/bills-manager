namespace Model.Bills
{
    public interface IBillRepository
    {
        Task<Bill> AddAsync(Bill bill);
        Task<Bill> UpdateAsync(Bill bill);
        IQueryable<Bill> GetAllAsync();
        Bill? GetBy(string code, bool includeTags = false);
        Task DeleteAsync(Bill bill);
    }
}

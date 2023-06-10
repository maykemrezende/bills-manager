using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Bills;

namespace Infra.Persistence.Repositories
{
    public class BillRepository : IBillRepository
    {
        public BillsContext Context { get; }
        public ILogger<BillRepository> Logger { get; }

        public BillRepository(BillsContext context,
            ILogger<BillRepository> logger)
        {
            Context = context;
            Logger = logger;
        }

        public async Task<Bill> AddAsync(Bill bill)
        {
            try
            {
                var savedBill = await Context.Bills.AddAsync(bill);
                var saved = await Context.SaveChangesAsync() > 0;

                if (saved)
                    return savedBill.Entity;
            } catch (Exception e)
            {
                Logger.LogError(e, e.Message);
            }

            return default;
        }

        public async Task UpdateAsync(Bill bill)
        {
            Context.Bills.Update(bill);
            await Context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Bill>> GetAllAsync()
        {
            return await Context.Bills.ToListAsync();
        }

        public async Task<Bill?> GetByAsync(string code)
        {
            return await Context.Bills.Where(b => b.Code.ToUpper() == code.ToUpper()).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Bill bill)
        {
            Context.Bills.Remove(bill);
            await Context.SaveChangesAsync();
        }
    }
}

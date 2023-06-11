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

        public async Task<Bill> UpdateAsync(Bill bill)
        {
            try
            {
                var updatedBill = Context.Bills.Update(bill);
                var saved = await Context.SaveChangesAsync() > 0;

                if (saved)
                    return updatedBill.Entity;
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
            }

            return default;
        }

        public async Task<IReadOnlyList<Bill>> GetAllAsync()
        {
            return await Context.Bills.ToListAsync();
        }

        public async Task<Bill?> GetByAsync(string code)
        {
            return await Context
                .Bills
                .AsNoTracking()
                .Where(b => b.Code.ToUpper() == code.ToUpper())
                .FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Bill bill)
        {
            try
            {
                Context.Bills.Remove(bill);
                await Context.SaveChangesAsync();
            } catch (Exception e)
            {
                Logger.LogError(e, e.Message);
            }
        }
    }
}

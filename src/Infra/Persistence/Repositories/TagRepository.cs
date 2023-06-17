using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Bills;
using Model.Tags;

namespace Infra.Persistence.Repositories
{
    public class TagRepository : ITagRepository
    {
        public BillsContext Context { get; }
        public ILogger<TagRepository> Logger { get; }

        public TagRepository(BillsContext context,
            ILogger<TagRepository> logger)
        {
            Context = context;
            Logger = logger;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            try
            {
                var savedTag = await Context.Tags.AddAsync(tag);
                var saved = await Context.SaveChangesAsync() > 0;

                if (saved)
                    return savedTag.Entity;
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
            }

            return default;
        }

        public async Task DeleteAsync(Tag tag)
        {
            try
            {
                Context.Tags.Remove(tag);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
            }
        }

        public async Task<IReadOnlyList<Tag>> GetAllAsync()
        {
            return await Context
                .Tags
                .AsNoTracking()
                .Include(b => b.Bills)
                .ToListAsync();
        }

        public Tag? GetBy(string code, bool includeBills = false)
        {
            var queryableTag = Context
                .Tags
                .AsNoTracking()
                .Include(b => b.Bills)
                .Where(b => b.Code.ToUpper() == code.ToUpper());

            if (includeBills)
                queryableTag = queryableTag.Include(b => b.Bills);

            return queryableTag.FirstOrDefault();
        }

        public async Task<Tag> UpdateAsync(Tag tag)
        {
            try
            {
                var updatedTag = Context.Tags.Update(tag);
                var saved = await Context.SaveChangesAsync() > 0;

                if (saved)
                    return updatedTag.Entity;
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
            }

            return default;
        }
    }
}

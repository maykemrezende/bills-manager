using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        public IQueryable<Tag> GetAll()
        {
            return Context
                .Tags
                .AsNoTracking();
        }

        public Tag? GetBy(string code)
        {
            return Context
                .Tags
                .AsNoTracking()
                .Include(b => b.Bills)
                .Where(b => b.Code.ToUpper() == code.ToUpper())
                .FirstOrDefault();
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

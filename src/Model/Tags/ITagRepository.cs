namespace Model.Tags
{
    public interface ITagRepository
    {
        Task<Tag> AddAsync(Tag tag);
        Task<Tag> UpdateAsync(Tag tag);
        IQueryable<Tag> GetAll();
        Tag? GetBy(string code);
        Task DeleteAsync(Tag tag);
    }
}

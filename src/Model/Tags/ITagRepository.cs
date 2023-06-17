namespace Model.Tags
{
    public interface ITagRepository
    {
        Task<Tag> AddAsync(Tag tag);
        Task<Tag> UpdateAsync(Tag tag);
        Task<IReadOnlyList<Tag>> GetAllAsync();
        Tag? GetBy(string code, bool includeBills = false);
        Task DeleteAsync(Tag tag);
    }
}

namespace Model.Tags
{
    public interface ITagRepository
    {
        Task<Tag> AddAsync(Tag tag);
        Task<Tag> UpdateAsync(Tag tag);
        Task<IReadOnlyList<Tag>> GetAllAsync();
        Task<Tag?> GetByAsync(string code);
        Task DeleteAsync(Tag tag);
    }
}

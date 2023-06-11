using Application.Dtos.Tags;

namespace Application.Services.Tags
{
    public interface ITagService
    {
        Task<CreatedTagResponse> CreateTagAsync(CreateTagRequest createBillDto);
        Task<UpdatedTagResponse> UpdateTagAsync(UpdateTagRequest createBillDto, string code);
        Task<IReadOnlyList<TagResponse>> GetTagsAsync();
        Task<TagResponse> GetTagByCodeAsync(string code);
        Task DeleteTagAsync(string code);
    }
}

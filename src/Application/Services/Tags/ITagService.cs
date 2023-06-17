using Application.Dtos.Tags;

namespace Application.Services.Tags
{
    public interface ITagService
    {
        Task<CreatedTagResponse> CreateTagAsync(CreateTagRequest createBillDto);
        Task<UpdatedTagResponse> UpdateTagAsync(UpdateTagRequest createBillDto, string code);
        Task<IReadOnlyList<TagResponse>> GetTagsAsync();
        TagResponse GetTagByCode(string code, bool includeBills = false);
        Task DeleteTagAsync(string code);
    }
}

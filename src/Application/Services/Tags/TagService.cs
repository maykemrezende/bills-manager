using Application.Dtos.Bills;
using Application.Dtos.Tags;
using Infra.Persistence.Repositories;
using Model.Bills;
using Model.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Tags
{
    public class TagService : ITagService
    {
        public ITagRepository TagRepository { get; }

        public TagService(ITagRepository tagRepository)
        {
            TagRepository = tagRepository;
        }

        public async Task<CreatedTagResponse> CreateTagAsync(CreateTagRequest createTagDto)
        {
            var tag = new Tag(
                createTagDto.Name);

            var savedTag = await TagRepository.AddAsync(tag);

            if (savedTag is null)
                return default;

            return new CreatedTagResponse(
                savedTag.Name,
                savedTag.Code);
        }

        public async Task<UpdatedTagResponse> UpdateTagAsync(UpdateTagRequest createTagDto, string code)
        {
            var tag = await TagRepository.GetByAsync(code);

            if (tag is null)
                return default;

            tag.Update(createTagDto.Name);

            var updatedTag = await TagRepository.UpdateAsync(tag);

            if (updatedTag is null)
                return default;

            return new UpdatedTagResponse(
                tag.Name,
                tag.Code);
        }

        public async Task<IReadOnlyList<TagResponse>> GetTagsAsync()
        {
            var tags = await TagRepository.GetAllAsync();

            return tags
                .Select(b => new TagResponse(b.Name, b.Code))
                .ToList();
        }

        public async Task<TagResponse> GetTagByCodeAsync(string code)
        {
            var tag = await TagRepository.GetByAsync(code);

            if (tag is null)
                return default;

            return new TagResponse(
                tag.Name,
                tag.Code);
        }

        public async Task DeleteTagAsync(string code)
        {
            var tag = await TagRepository.GetByAsync(code);

            if (tag is null)
                return;

            await TagRepository.DeleteAsync(tag);
        }
    }
}

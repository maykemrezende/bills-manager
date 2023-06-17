using Application.Dtos.Bills;
using Application.Dtos.Tags;
using Application.Services.Tags;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {
        public ITagService TagService { get; }

        public TagController(ITagService tagService)
        {
            TagService = tagService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedTagResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateTag(CreateTagRequest request)
        {
            var tagToReturn = await TagService.CreateTagAsync(request);

            if (tagToReturn is null)
                return BadRequest();

            return Created(string.Empty, tagToReturn);
        }

        [HttpGet("{code}")]
        [ProducesResponseType(typeof(TagResponse), (int)HttpStatusCode.OK)]
        public IActionResult GetTagBy(string code)
        {
            var tagToReturn = TagService.GetTagByCode(code);

            if (tagToReturn is null)
                return NotFound();

            return Ok(tagToReturn);
        }

        [HttpGet]
        [ProducesResponseType(typeof(TagResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTags()
        {
            var tagToReturn = await TagService.GetTagsAsync();

            if (tagToReturn.Any() is false)
                return NotFound();

            return Ok(tagToReturn);
        }

        [HttpDelete("{code}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteTag(string code)
        {
            await TagService.DeleteTagAsync(code);

            return NoContent();
        }

        [HttpPut("{code}")]
        [ProducesResponseType(typeof(UpdatedBillResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateTag([FromBody] UpdateTagRequest dto, string code)
        {
            var tagToReturn = await TagService.UpdateTagAsync(dto, code);

            if (tagToReturn is null)
                return NotFound();

            return Ok(tagToReturn);
        }
    }
}

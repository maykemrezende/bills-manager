using Application.Dtos.Tags;
using Application.Services.Tags;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateTag(CreateTagRequest request)
        {
            var tagToReturn = await TagService.CreateTagAsync(request);

            if (tagToReturn is null)
                return BadRequest();

            return Created(string.Empty, tagToReturn);
        }

        [HttpGet("{code}")]
        public IActionResult GetTagBy(string code, [FromQuery] bool includeBills)
        {
            var tagToReturn = TagService.GetTagByCode(code, includeBills);

            if (tagToReturn is null)
                return NotFound();

            return Ok(tagToReturn);
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteTag(string code)
        {
            await TagService.DeleteTagAsync(code);

            return NoContent();
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> UpdateTag([FromBody] UpdateTagRequest dto, string code)
        {
            var tagToReturn = await TagService.UpdateTagAsync(dto, code);

            if (tagToReturn is null)
                return NotFound();

            return Ok(tagToReturn);
        }
    }
}

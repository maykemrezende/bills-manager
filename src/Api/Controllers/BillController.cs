using Application.Dtos.Bills;
using Application.Services.Bills;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Route("api/bills")]
    [ApiController]
    public class BillController : ControllerBase
    {
        public IBillService BillService { get; }

        public BillController(IBillService billService)
        {
            BillService = billService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedBillResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateBill(CreateBillRequest request)
        {
            var billToReturn = await BillService.CreateBillAsync(request);

            if (billToReturn is null)
                return BadRequest();

            return Created(string.Empty, billToReturn);
        }

        [HttpGet("{code}")]
        [ProducesResponseType(typeof(BillResponse), (int)HttpStatusCode.OK)]
        public IActionResult GetBillBy(string code, [FromQuery] bool includeTags)
        {
            var billToReturn = BillService.GetBillByCode(code, includeTags);

            if (billToReturn is null)
                return NotFound();

            return Ok(billToReturn);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(IReadOnlyList<BillResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBills([FromQuery] GetBillsFiltersRequest filters)
        {
            var billToReturn = await BillService.GetBillsAsync(filters);

            if (billToReturn.Any() is false)
                return NotFound();

            return Ok(billToReturn);
        }

        [HttpDelete("{code}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteBill(string code)
        {
            await BillService.DeleteBillAsync(code);

            return NoContent();
        }

        [HttpPut("{code}/payments")]
        [ProducesResponseType(typeof(PaidBillResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PayBill(string code)
        {
            var billToReturn = await BillService.PayBillAsync(code);

            if (billToReturn is null)
                return NotFound();

            return Ok(billToReturn);
        }

        [HttpPut("{code}")]
        [ProducesResponseType(typeof(IReadOnlyList<BillResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBill([FromBody] UpdateBillRequest dto, string code)
        {
            var billToReturn = await BillService.UpdateBillAsync(dto, code);

            if (billToReturn is null)
                return NotFound();

            return Ok(billToReturn);
        }

        [HttpPut("{code}/tag-assignment")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AssignTag([FromBody] AssignTagRequest dto, string code)
        {
            await BillService.AssignTagAsync(dto, code);

            return Ok();
        }
    }
}

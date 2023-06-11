using Application.Dtos;
using Application.Services.Bills;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateBill(CreateBillRequest request)
        {
            var billToReturn = await BillService.CreateBillAsync(request);

            if (billToReturn is null)
                return BadRequest();

            return Created(string.Empty, billToReturn);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetBillBy(string code)
        {
            var billToReturn = await BillService.GetBillByCodeAsync(code);

            if (billToReturn is null)
                return NotFound();

            return Ok(billToReturn);
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteBill(string code)
        {
            await BillService.DeleteBillAsync(code);

            return NoContent();
        }

        [HttpPut("{code}/payments")]
        public async Task<IActionResult> PayBill(string code)
        {
            var billToReturn = await BillService.PayBillAsync(code);

            if (billToReturn is null)
                return NotFound();

            return Ok(billToReturn);
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> UpdateBill([FromBody] UpdateBillRequest dto, string code)
        {
            var billToReturn = await BillService.UpdateBillAsync(dto, code);

            if (billToReturn is null)
                return NotFound();

            return Ok(billToReturn);
        }
    }
}

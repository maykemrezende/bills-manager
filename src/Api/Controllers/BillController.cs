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
    }
}

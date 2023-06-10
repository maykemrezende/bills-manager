using Application.Dtos;

namespace Application.Services.Bills
{
    public interface IBillService
    {
        Task<CreatedBillResponse> CreateBillAsync(CreateBillRequest createBillDto);
        Task PayBillAsync(string code);
    }
}

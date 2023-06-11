using Application.Dtos.Bills;

namespace Application.Services.Bills
{
    public interface IBillService
    {
        Task<CreatedBillResponse> CreateBillAsync(CreateBillRequest createBillDto);
        Task<UpdatedBillResponse> UpdateBillAsync(UpdateBillRequest createBillDto, string code);
        Task<IReadOnlyList<BillResponse>> GetBillsAsync();
        Task<BillResponse> GetBillByCodeAsync(string code);
        Task<PaidBillResponse> PayBillAsync(string code);
        Task DeleteBillAsync(string code);
        Task AssignTagAsync(AssignTagRequest dto, string code);
    }
}

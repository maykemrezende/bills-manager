using Application.Dtos.Bills;

namespace Application.Services.Bills
{
    public interface IBillService
    {
        Task<CreatedBillResponse> CreateBillAsync(CreateBillRequest createBillDto);
        Task<UpdatedBillResponse> UpdateBillAsync(UpdateBillRequest createBillDto, string code);
        Task<IReadOnlyList<BillResponse>> GetBillsAsync(GetBillsFiltersRequest filters);
        BillResponse GetBillByCode(string code, bool includeTags);
        Task<PaidBillResponse> PayBillAsync(string code);
        Task DeleteBillAsync(string code);
        Task AssignTagAsync(AssignTagRequest dto, string code);
    }
}

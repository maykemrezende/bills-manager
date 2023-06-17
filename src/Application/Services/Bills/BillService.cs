using Application.Dtos.Bills;
using Application.Services.Tags;
using Model.Bills;
using Model.Tags;

namespace Application.Services.Bills
{
    public class BillService : IBillService
    {
        public IBillRepository BillRepository { get; }
        public ITagRepository TagRepository { get; }

        public BillService(IBillRepository billRepository, ITagRepository tagRepository)
        {
            BillRepository = billRepository;
            TagRepository = tagRepository;
        }

        public async Task<CreatedBillResponse> CreateBillAsync(CreateBillRequest createBillDto)
        {
            var bill = new Bill(
                createBillDto.Name, 
                new Money(createBillDto.Currency.ToString(), 
                            createBillDto.Amount),
                createBillDto.Month,
                createBillDto.Year,
                createBillDto.IsPaid);

            var savedBill = await BillRepository.AddAsync(bill);

            if (savedBill is null)
                return default;

            return new CreatedBillResponse(
                savedBill.Name, 
                savedBill.Code, 
                savedBill.Price.Amount, 
                savedBill.Price.Currency,
                savedBill.Period.MonthName,
                savedBill.Period.Year,
                savedBill.IsPaid);
        }

        public async Task<PaidBillResponse> PayBillAsync(string code)
        {
            var bill = BillRepository.GetBy(code);

            if (bill is null)
                return default;

            bill.Pay();
            var paidBill = await BillRepository.UpdateAsync(bill);

            if (paidBill is null)
                return default;

            return new PaidBillResponse(bill.Name,
                bill.Code,
                bill.IsPaid);
        }

        public async Task DeleteBillAsync(string code)
        {
            var bill = BillRepository.GetBy(code);

            if (bill is null)
                return;

            await BillRepository.DeleteAsync(bill);
        }

        public async Task<IReadOnlyList<BillResponse>> GetBillsAsync()
        {
            var bills =  await BillRepository.GetAllAsync();

            return bills.Select(b => new BillResponse(b.Name,
                b.Code,
                b.Price.Amount,
                b.Price.Currency,
                b.Period.MonthName,
                b.Period.Year,
                b.IsPaid)).ToList();
        }

        public BillResponse? GetBillByCode(string code, bool includeTags)
        {
            var bill = BillRepository.GetBy(code, includeTags);

            if (bill is null)
                return default;

            return new BillResponse(bill.Name,
                bill.Code,
                bill.Price.Amount,
                bill.Price.Currency,
                bill.Period.MonthName,
                bill.Period.Year,
                bill.IsPaid);
        }

        public async Task<UpdatedBillResponse> UpdateBillAsync(UpdateBillRequest updateBillDto, string code)
        {
            var bill = BillRepository.GetBy(code);

            if (bill is null)
                return default;

            bill.Update(
                updateBillDto.Name, 
                updateBillDto.Month, 
                updateBillDto.Year, 
                new Money(updateBillDto.Currency.ToString(), updateBillDto.Amount));

            var updatedBill = await BillRepository.UpdateAsync(bill);

            if (updatedBill is null)
                return default;

            return new UpdatedBillResponse(bill.Name,
                bill.Code,
                bill.Price.Amount,
                bill.Price.Currency,
                bill.Period.MonthName,
                bill.Period.Year,
                bill.IsPaid);
        }

        public async Task AssignTagAsync(AssignTagRequest dto, string code)
        {
            var bill = BillRepository.GetBy(code);

            if (bill is null)
                return;

            var tag = TagRepository.GetBy(dto.TagCode);

            if (tag is null)
                return;

            bill.AssignTag(tag);

            await BillRepository.UpdateAsync(bill);
        }
    }
}

using Application.Dtos.Bills;
using Microsoft.EntityFrameworkCore;
using Model.Bills;
using Model.Tags;
using System.Collections.Immutable;

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

        public async Task<IReadOnlyList<BillResponse>> GetBillsAsync(GetBillsFiltersRequest filters)
        {
            var queryableBills = BillRepository.GetAllAsync();

            if (queryableBills.Any() is false)
            {
                return new List<BillResponse>();
            }

            queryableBills = CheckIncludeTags(filters, queryableBills);
            queryableBills = UseYearFilter(filters, queryableBills);
            queryableBills = UseMonthFilter(filters, queryableBills);

            var bills = queryableBills
                .Where(q => q.IsPaid == filters.IsPaid)
                .ToImmutableList();

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

        private static IQueryable<Bill> UseMonthFilter(GetBillsFiltersRequest filters, IQueryable<Bill> queryableBills)
        {
            if (filters.MonthOfBills > 0)
            {
                queryableBills = queryableBills.Where(q => q.Period.Year == filters.YearOfBills);
            }

            return queryableBills;
        }

        private static IQueryable<Bill> UseYearFilter(GetBillsFiltersRequest filters, IQueryable<Bill> queryableBills)
        {
            if (filters.YearOfBills > 0)
            {
                queryableBills = queryableBills.Where(q => q.Period.Month == filters.MonthOfBills);
            }

            return queryableBills;
        }

        private static IQueryable<Bill> CheckIncludeTags(GetBillsFiltersRequest filters, IQueryable<Bill> queryableBills)
        {
            if (filters.IncludeTags)
            {
                queryableBills = queryableBills.Include(q => q.Tags);
            }

            return queryableBills;
        }
    }
}

using Application.Dtos;
using Model.Bills;

namespace Application.Services.Bills
{
    public class BillService : IBillService
    {
        public IBillRepository BillRepository { get; }

        public BillService(IBillRepository billRepository)
        {
            BillRepository = billRepository;
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
            var bill = await BillRepository.GetByAsync(code);

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
            var bill = await BillRepository.GetByAsync(code);

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

        public async Task<BillResponse> GetBillByCodeAsync(string code)
        {
            var bill = await BillRepository.GetByAsync(code);

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
            var bill = await BillRepository.GetByAsync(code);

            if (bill is null)
                return default;

            bill.Update(
                updateBillDto.Name, 
                updateBillDto.Month, 
                updateBillDto.Year, 
                new Money(updateBillDto.Currency.ToString(), updateBillDto.Amount));

            var paidBill = await BillRepository.UpdateAsync(bill);

            if (paidBill is null)
                return default;

            return new UpdatedBillResponse(bill.Name,
                bill.Code,
                bill.Price.Amount,
                bill.Price.Currency,
                bill.Period.MonthName,
                bill.Period.Year,
                bill.IsPaid);
        }
    }
}

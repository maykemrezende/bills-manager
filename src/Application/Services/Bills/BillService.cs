using Application.Dtos;
using Model.Bills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                new Money(createBillDto.Currency, 
                            createBillDto.Amount), 
                createBillDto.IsPaid);

            var savedBill = await BillRepository.AddAsync(bill);

            if (savedBill is null)
                return default;

            return new CreatedBillResponse(
                savedBill.Name, 
                savedBill.Code, 
                savedBill.Price.Amount, 
                savedBill.Price.Currency, 
                savedBill.IsPaid);
        }

        public Task PayBillAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}

namespace Application.Dtos.Bills
{
    public record GetBillsFiltersRequest(
        bool IncludeTags, 
        int YearOfBills,
        int MonthOfBills,
        bool IsPaid);
}

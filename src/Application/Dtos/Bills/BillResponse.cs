namespace Application.Dtos.Bills
{
    public record BillResponse(string Name,
        string Code,
        decimal Amount,
        string Currency,
        string MonthName,
        int Year,
        bool IsPaid);
}

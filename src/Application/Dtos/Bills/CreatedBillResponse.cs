namespace Application.Dtos.Bills
{
    public record CreatedBillResponse(
        string Name,
        string Code,
        decimal Amount,
        string Currency,
        string Month,
        int Year,
        bool IsPaid);
}

namespace Application.Dtos.Bills
{
    public record UpdatedBillResponse(
        string Name,
        string Code,
        decimal Amount,
        string Currency,
        string Month,
        int Year,
        bool IsPaid
        );
}

namespace Application.Dtos
{
    public record CreatedBillResponse(
        string Name, 
        string Code, 
        decimal Amount, 
        string Currency,
        bool IsPaid);
}

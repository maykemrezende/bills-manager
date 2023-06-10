using System.Diagnostics.CodeAnalysis;

namespace Application.Dtos
{
    public record CreateBillRequest(
        [NotNull]
        string Name,
        decimal Amount,
        [NotNull]
        string Currency, 
        bool IsPaid);
}

using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Bills
{
    public record CreateBillRequest(
        [Required]
        string Name,
        decimal Amount,
        bool IsPaid,
        [Range(1, 12)]
        int Month,
        int Year,
        CurrencyType Currency
        );
}

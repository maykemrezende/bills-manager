using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public record UpdateBillRequest(
        [Required]
        string Name,
        decimal Amount,
        [Range(1, 12)]
        int Month,
        int Year,
        CurrencyType Currency
        );
}

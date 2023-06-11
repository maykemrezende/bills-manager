using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Tags
{
    public record CreateTagRequest([Required]string Name);
}

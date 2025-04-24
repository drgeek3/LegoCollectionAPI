using System.ComponentModel.DataAnnotations;

namespace LegoCollection.Dtos
{
    public record class UpdateOwnedDto(
        int Id, 
        [Required][StringLength(45)] string BrickId,
        [Required] int ColorId,
        [Range(1, 10000)] int Count,
        [Required][StringLength(45)] string LocationId
    );
    
}

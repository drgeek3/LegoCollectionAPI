using System.ComponentModel.DataAnnotations;

namespace LegoCollection.Dtos
{
    public record class CreateOwnedDto(
        [Required][StringLength(45)] string BrickId,
        [Required] int ColorId,
        [Range(1,10000)] int NumAvailable,
        [Range(0,10000)] int NumInUse,
        [Required][StringLength(45)] string LocationId
    );
   
}

using System.ComponentModel.DataAnnotations;

namespace LegoCollection.Dtos
{
    public record class CreateOwnedDto(
        [Required][StringLength(45)] string BrickId,
        [Required] int ColorId,
        [Range(0,100000)] int NumAvailable,
        [Range(0,100000)] int NumInUse,
        [Required][StringLength(45)] string LocationId
    );
   
}

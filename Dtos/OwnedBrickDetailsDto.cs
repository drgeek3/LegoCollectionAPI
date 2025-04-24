namespace LegoCollection.Dtos
{
    public record class OwnedBrickDetailsDto(
        int Id,
        string BrickId,
        int ColorId,
        int Count,
        string LocationId);
}

namespace LegoCollection.Dtos
{
    public record class OwnedBrickDetailsDto(
        int Id,
        string BrickId,
        int ColorId,
        int NumAvailable,
        int NumInUse,
        string LocationId);
}

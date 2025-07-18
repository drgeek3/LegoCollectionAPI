namespace LegoCollection.Dtos
{
    public record class LocationBrickListDto(
        string LocationId,
        string BrickId,
        string? Description,
        string? Category,
        string? Subcategory,
        string? Container,
        string? Unit,
        string? UnitRow,
        string? Drawer,
        string? Color,
        int NumAvailable,
        int NumInUse,
        string? AltBrickId);
}

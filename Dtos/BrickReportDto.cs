namespace LegoCollection.Dtos
{
    public record class BrickReportDto (    
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
        string? AltBrickId,
        bool Overloaded,
        bool Underfilled,
        bool LocEmpty);
}

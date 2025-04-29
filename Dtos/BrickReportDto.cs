namespace LegoCollection.Dtos
{
    public record class BrickReportDto (    
        string BrickId,
        string? Description,
        string? Category,
        string? Subcategory,
        string? Container,
        string? Unit,
        int UnitRow,
        int Drawer,
        string? Color,
        int NumAvailable,
        int NumInUse,
        string? AltBrickId,
        bool Overloaded,
        bool Underfilled,
        bool LocEmpty);
}

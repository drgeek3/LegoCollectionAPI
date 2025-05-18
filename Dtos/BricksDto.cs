namespace LegoCollection.Dtos
{
    public record class BricksDto
    (
        int Id,
        string? BrickId,
        string? Description,
        string? Category,
        string? Subcategory,
        string? AltBrickId
    );
}

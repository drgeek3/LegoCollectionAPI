namespace LegoCollection.Dtos
{
    public record class LocationDto (    
        int Id,
        string LocationId,
        string Container,
        string Unit,
        string UnitRow,
        string Drawer,
        bool Overloaded,
        bool Underfilled,
        bool LocationEmpty
    );
}

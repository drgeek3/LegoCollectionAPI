namespace LegoCollection.Dtos
{
    public record class LocationDto (    
        int Id,
        string LocationId,
        string Container,
        string Unit,
        int UnitRow,
        int Drawer,
        bool Overloaded,
        bool Underfilled,
        bool LocationEmpty
    );
}

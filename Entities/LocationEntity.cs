namespace LegoCollection.Entities
{
    public class LocationEntity
    {
        public int Id { get; set; }
        public string? LocationId { get; set; }
        public string? Container { get; set; }
        public string? Unit { get; set; }
        public int UnitRow { get; set; }
        public int Drawer { get; set; }
        public bool Overloaded { get; set; }
        public bool Underfilled { get; set; }
        public bool LocationEmpty { get; set; }


    }
}

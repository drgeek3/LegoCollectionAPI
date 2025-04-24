namespace LegoCollection.Entities
{
    public class BrickLocation
    {
        public required string BrickId { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Subcategory { get; set; }
        public string? Container { get; set; }
        public string? Unit { get; set; }
        public int UnitRow { get; set; }
        public int Drawer { get; set; }
        public string? Color { get; set; }
        public int NumAvailable { get; set; }
        public int NumInUse { get; set; }
        public string? AltBrickId { get; set; }
        public bool Overloaded { get; set; }
        public bool Underfilled { get; set; }
        public bool LocEmpty { get; set; }


    }
}

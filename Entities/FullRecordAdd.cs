namespace LegoCollection.Entities
{
    public class FullRecordAdd
    {
        public required string BrickId { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public string? AltBrickId { get; set; }
        public string? Color { get; set; }
        public string? Container { get; set; }
        public string? Unit { get; set; }
        public int UnitRow { get; set; }
        public int Drawer { get; set; }
        public bool Overloaded { get; set; }
        public bool Underfilled { get; set; }
        public bool Empty { get; set; }
    }
}

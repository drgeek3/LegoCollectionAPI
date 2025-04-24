namespace LegoCollection.Entities
{
    public class UpdateOwned
    {
        public int Id { get; set; } 
        public required string BrickId { get; set; }

        public int ColorId { get; set; }

        public int NumAvailable { get; set; }

        public int NumInUse { get; set; }

        public required string LocationId { get; set; }
    }
}

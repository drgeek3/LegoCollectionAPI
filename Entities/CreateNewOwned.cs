namespace LegoCollection.Entities
{
    public class CreateNewOwned
    {        
        public required string BrickId { get; set; }

        public int ColorId { get; set; }

        public int NumAvailable { get; set; }

        public int NumInUse { get; set; }

        public required string LocationId { get; set; }
    }
}

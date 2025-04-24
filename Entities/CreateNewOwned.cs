namespace LegoCollection.Entities
{
    public class CreateNewOwned
    {        
        public required string BrickId { get; set; }

        public int ColorId { get; set; }

        public int Count { get; set; }

        public required string LocationId { get; set; }
    }
}

namespace LegoCollection.Entities
{
    public class UpdateOwned
    {
        public int Id { get; set; } 
        public required string BrickId { get; set; }

        public int ColorId { get; set; }

        public int Count { get; set; }

        public required string LocationId { get; set; }
    }
}

namespace LegoCollection.Entities
{
    public class CategoriesEntity
    {
        public int Id { get; set; }
        public string? Category { get; set; }
        public bool IsMain { get; set; }
        public string? Subcat { get; set; }

    }
}

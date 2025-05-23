﻿namespace LegoCollection.Entities
{
    public class Owned
    {
        public int Id { get; set; }

        public string? BrickId { get; set; }

        public int ColorId { get; set; }

        public string? Color { get; set; }

        public int NumAvailable { get; set; }

        public int NumInUse { get; set; }

        public string? LocationId { get; set; }

    }
}

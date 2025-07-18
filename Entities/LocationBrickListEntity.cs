﻿namespace LegoCollection.Entities
{
    public class LocationBrickListEntity
    {
            public required string LocationId { get; set; }
            public required string BrickId { get; set; }
            public string? Description { get; set; }
            public string? Category { get; set; }
            public string? Subcategory { get; set; }
            public string? Container { get; set; }
            public string? Unit { get; set; }
            public string? UnitRow { get; set; }
            public string? Drawer { get; set; }
            public string? Color { get; set; }
            public int NumAvailable { get; set; }
            public int NumInUse { get; set; }
            public string? AltBrickId { get; set; }   

    }
}

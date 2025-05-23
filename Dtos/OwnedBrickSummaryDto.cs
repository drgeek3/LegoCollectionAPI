﻿namespace LegoCollection.Dtos
{
    public record class OwnedBrickSummaryDto (
        int Id, 
        string BrickId, 
        string Color, 
        int NumAvailable,
        int NumInUse,
        string LocationId);
   
}

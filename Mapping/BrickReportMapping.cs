using LegoCollection.Dtos;
using LegoCollection.Entities;
using System.Runtime.CompilerServices;

namespace LegoCollection.Mapping
{
    public static class BrickReportMapping
    {
        public static BrickLocation ToBrickLocationEntity(this BrickReportDto brickReport)
        {
            return new BrickLocation()
            {
                BrickId = brickReport.BrickId,
                Description = brickReport.Description,
                Category = brickReport.Category,
                Subcategory = brickReport.Subcategory,
                Container = brickReport.Container,
                Unit = brickReport.Unit,
                UnitRow = brickReport.UnitRow,
                Drawer = brickReport.Drawer,
                Color = brickReport.Color,
                NumAvailable = brickReport.NumAvailable,
                NumInUse = brickReport.NumInUse,
                AltBrickId = brickReport.AltBrickId,
                Overloaded = brickReport.Overloaded,
                Underfilled = brickReport.Underfilled,
                LocEmpty = brickReport.LocEmpty
            };
        }

        public static FullRecordAdd ToFullRecordEntity(this BrickReportDto brickReport)
        {
            return new FullRecordAdd()
            {
                BrickId = brickReport.BrickId,
                Description = brickReport.Description,
                Category = brickReport.Category,
                Subcategory = brickReport.Subcategory,
                Container = brickReport.Container,
                Unit = brickReport.Unit,
                UnitRow = brickReport.UnitRow,
                Drawer = brickReport.Drawer,
                Color = brickReport.Color,
                NumAvailable = brickReport.NumAvailable,
                NumInUse = brickReport.NumInUse,
                AltBrickId = brickReport.AltBrickId,
                Overloaded = brickReport.Overloaded,
                Underfilled = brickReport.Underfilled,
                LocEmpty = brickReport.LocEmpty
            };
        }

        public static BrickReportDto ToBrickReportDto(this BrickLocation brickLocation)
        {
            return new BrickReportDto(

                brickLocation.BrickId,
                brickLocation.Description,
                brickLocation.Category,
                brickLocation.Subcategory,
                brickLocation.Container,
                brickLocation.Unit,
                brickLocation.UnitRow,
                brickLocation.Drawer,
                brickLocation.Color,
                brickLocation.NumAvailable,
                brickLocation.NumInUse,
                brickLocation.AltBrickId,
                brickLocation.Overloaded,
                brickLocation.Underfilled,
                brickLocation.LocEmpty
            );
        }



    }
}

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
            return new BrickReportDto()
            {
                BrickId = brickLocation.BrickId,
                Description = brickLocation.Description,
                Category = brickLocation.Category,
                Subcategory = brickLocation.Subcategory,
                Container = brickLocation.Container,
                Unit = brickLocation.Unit,
                UnitRow = brickLocation.UnitRow,
                Drawer = brickLocation.Drawer,
                Color = brickLocation.Color,
                NumAvailable = brickLocation.NumAvailable,
                NumInUse = brickLocation.NumInUse,
                AltBrickId = brickLocation.AltBrickId,
                Overloaded = brickLocation.Overloaded,
                Underfilled = brickLocation.Underfilled,
                LocEmpty = brickLocation.LocEmpty
            };
        }



    }
}

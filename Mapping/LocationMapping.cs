using LegoCollection.Dtos;
using LegoCollection.Entities;
using System.Linq;

namespace LegoCollection.Mapping
{
    public static class LocationMapping
    {
        public static LocationEntity ToLocationEntity(this LocationDto location)
        {
            return new LocationEntity()
            {
                Id = location.Id,
                LocationId = location.LocationId,
                Container = location.Container,
                Unit = location.Unit,
                UnitRow = location.UnitRow,
                Drawer = location.Drawer,
                Overloaded = location.Overloaded,
                Underfilled = location.Underfilled,
                LocationEmpty = location.LocationEmpty
            };
        }

        public static LocationEntity ToLocationEntity(this LocationDto location, int id)
        {

            return new LocationEntity()
            {
                Id = id,
                LocationId = location.LocationId,
                Container = location.Container,
                Unit = location.Unit,
                UnitRow = location.UnitRow,
                Drawer = location.Drawer,
                Overloaded = location.Overloaded,
                Underfilled = location.Underfilled,
                LocationEmpty = location.LocationEmpty
            };
        }

        public static LocationDto ToLocationDto(this LocationEntity locationEntity)
        {
            if(locationEntity.LocationId == "PH")
            {
                locationEntity.LocationId = locationEntity.Container.Substring(0,1) + locationEntity.Unit + locationEntity.UnitRow + locationEntity.Drawer;
            } 

                return new LocationDto(
                    locationEntity.Id,
                    locationEntity.LocationId,
                    locationEntity.Container,
                    locationEntity.Unit,
                    locationEntity.UnitRow,
                    locationEntity.Drawer,
                    locationEntity.Overloaded,
                    locationEntity.Underfilled,
                    locationEntity.LocationEmpty);
        }

        public static List<LocationDto> ToLocationDtoList(this List<LocationEntity> locationEntity)
        {
            List<LocationDto> locationDtoOutput = new List<LocationDto>();

            foreach (var location in locationEntity)
            {
                if (location.LocationId == "PH")
                {
                    location.LocationId = location.Container.Substring(0, 1) + location.Unit + location.UnitRow + location.Drawer;
                }

                locationDtoOutput.Add(new LocationDto(
                    location.Id,
                    location.LocationId,
                    location.Container,
                    location.Unit,
                    location.UnitRow,
                    location.Drawer,
                    location.Overloaded,
                    location.Underfilled,
                    location.LocationEmpty
                ));
            }

            return locationDtoOutput;
        }

        public static List<LocationBrickListDto> ToLocationBrickListDto(this List<LocationBrickListEntity> locationBricklistEntity)
        {
            List<LocationBrickListDto> locationBricklistDtoOutput = new List<LocationBrickListDto>();

            foreach (var location in locationBricklistEntity)
            {
                locationBricklistDtoOutput.Add(new LocationBrickListDto(
                    location.LocationId,
                    location.BrickId,
                    location.Description,
                    location.Category,
                    location.Subcategory,
                    location.Container,
                    location.Unit,
                    location.UnitRow,
                    location.Drawer,
                    location.Color,
                    location.NumAvailable,
                    location.NumInUse,
                    location.AltBrickId
                ));
            }

            return locationBricklistDtoOutput;
        }

    }
}

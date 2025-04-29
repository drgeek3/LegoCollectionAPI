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
    }
}

using LegoCollection.Dtos;
using LegoCollection.Entities;

namespace LegoCollection.Mapping
{
    public static class OwnedMapping
    {
        public static Owned ToOwnedEntity(this CreateOwnedDto lego)
        {
            return new Owned()
            {
                BrickId = lego.BrickId,
                ColorId = lego.ColorId,
                Count = lego.Count,
                LocationId = lego.LocationId
            };
        }

        public static Owned ToOwnedEntity(this UpdateOwnedDto lego, int id)
        {
            return new Owned()
            {
                Id = id,
                BrickId = lego.BrickId,
                ColorId = lego.ColorId,
                Count = lego.Count,
                LocationId = lego.LocationId
            };
        }

        public static OwnedBrickSummaryDto ToOwnedBrickSummaryDto(this Owned lego)
        {
            return new OwnedBrickSummaryDto(
                lego.Id,
                lego.BrickId ?? string.Empty,
                lego.Color!,
                lego.Count,
                lego.LocationId ?? string.Empty
                );
        }
        public static List<OwnedBrickSummaryDto> ToOwnedBrickSummaryDtoList(this List<Owned> lego)
        {
            List<OwnedBrickSummaryDto> summaryDtoOutput = new List<OwnedBrickSummaryDto>();

            foreach (var brick in lego)
            {
                summaryDtoOutput.Add(new OwnedBrickSummaryDto(
                brick.Id,
                brick.BrickId ?? string.Empty,
                brick.Color!,
                brick.Count,
                brick.LocationId ?? string.Empty
                ));
            }

            return summaryDtoOutput;
        }

        public static OwnedBrickDetailsDto ToOwnedBrickDetailsDto(this Owned lego)
        {
            return new OwnedBrickDetailsDto(
                lego.Id,
                lego.BrickId ?? string.Empty,
                lego.ColorId,
                lego.Count,
                lego.LocationId ?? string.Empty
                );
        }
    }
}

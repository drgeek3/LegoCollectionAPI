using LegoCollection.Dtos;
using LegoCollection.Entities;

namespace LegoCollection.Mapping
{
    public static class BricksMapping
    {
        public static BricksEntity ToBricksEntity(this BricksDto bricks)
        {
            return new BricksEntity()
            {
                Id = bricks.Id,
                BrickId = bricks.BrickId,
                Description = bricks.Description,
                Category = bricks.Category,
                Subcategory = bricks.Subcategory,
                AltBrickId = bricks.AltBrickId
            };
        }

        public static BricksEntity ToBricksEntity(this BricksDto bricks, int id)
        {

            return new BricksEntity()
            {
                Id = id,
                BrickId = bricks.BrickId,
                Description = bricks.Description,
                Category = bricks.Category,
                Subcategory = bricks.Subcategory,
                AltBrickId = bricks.AltBrickId
            };
        }

        public static BricksDto ToBricksDto(this BricksEntity bricksEntity)
        {
            return new BricksDto(
                bricksEntity.Id,
                bricksEntity.BrickId,
                bricksEntity.Description,
                bricksEntity.Category,
                bricksEntity.Subcategory,
                bricksEntity.AltBrickId);
        }

        public static List<BricksDto> ToBricksDtoList(this List<BricksEntity> bricksEntity)
        {
            List<BricksDto> bricksDtoOutput = new List<BricksDto>();

            foreach (var brick in bricksEntity)
            {

                bricksDtoOutput.Add(new BricksDto(
                    brick.Id,
                    brick.BrickId,
                    brick.Description,
                    brick.Category,
                    brick.Subcategory,
                    brick.AltBrickId
                ));
            }

            return bricksDtoOutput;
        }
    }
}

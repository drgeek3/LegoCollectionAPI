using LegoCollection.Dtos;
using LegoCollection.Entities;

namespace LegoCollection.Mapping
{
    public static class ColorMapping
    {
        public static ColorsDto ToColorsDto(this ColorList colorList)
        {
            return new ColorsDto(colorList.Id, colorList.Name);
        }

        public static List<ColorsDto> ToColorsDtoList(this List<ColorList> colorList)
        {
            List<ColorsDto> colorsDtoOutput = new List<ColorsDto>();

            foreach (var color in colorList)
            {
                colorsDtoOutput.Add(new ColorsDto(
                color.Id,
                color.Name
                ));
            }

            return colorsDtoOutput;


        }

    }
}

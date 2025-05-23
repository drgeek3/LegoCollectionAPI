using LegoCollection.Dtos;
using LegoCollection.Entities;

namespace LegoCollection.Mapping
{
    public static class CategoriesMapping
    {
        public static List<CategoriesDto> ToCategoriesDtoList(this List<CategoriesEntity> categoriesEntity)
        {
            List<CategoriesDto> categoriesDtoOutput = new List<CategoriesDto>();

            foreach (var category in categoriesEntity)
            {

                categoriesDtoOutput.Add(new CategoriesDto(
                    category.Id,
                    category.Category,
                    category.IsMain,
                    category.Subcat
                ));
            }

            return categoriesDtoOutput;
        }
    }
}

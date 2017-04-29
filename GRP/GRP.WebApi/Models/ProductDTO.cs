using System.Collections.Generic;
namespace GRP.WebApi.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Directions { get; set; }
        public decimal Cost { get; set; }
        public decimal CostThreshold { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public decimal Calories { get; set; }
        public decimal Proteins { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Fats { get; set; }
        public string Type { get; set; }
        public int Servings { get; set; }
        public decimal RecipeYield { get; set; }
        public IEnumerable<ProductItemDTO> Items { get; set; }
    }
}
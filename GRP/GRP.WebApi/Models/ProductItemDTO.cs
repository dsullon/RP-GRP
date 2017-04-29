using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GRP.WebApi.Models
{
    public class ProductItemDTO
    {
        public decimal Qty { get; set; }
        public decimal Cost { get; set; }
        public int ProductId { get; set; }
        public int ItemId { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal Price { get; set; }
        public decimal Calories { get; set; }
        public decimal Proteins { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Fats { get; set; }
        public decimal RecipeYield { get; set; }
    }
}
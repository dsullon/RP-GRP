namespace GRP.WebApi.Models
{
    public class NutritionalFactsDTO
    {
        public int Id { get; set; }
        public int ItemID { get; set; }
        public decimal Calories { get; set; }
        public decimal Proteins { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Fats { get; set; }
    }
}

namespace GRP.WebApi.Models
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
    }
}
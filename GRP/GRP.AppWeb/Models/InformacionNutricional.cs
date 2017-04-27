using Newtonsoft.Json;

namespace GRP.AppWeb.Models
{
    public class InformacionNutricional
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("itemID")]
        public int IdArticulo { get; set; }

        [JsonProperty("calories")]
        public decimal Calorias { get; set; }

        [JsonProperty("proteins")]
        public decimal Proteinas { get; set; }

        [JsonProperty("carbohydrates")]
        public decimal Carbohidratos { get; set; }

        [JsonProperty("fats")]
        public decimal Grasas { get; set; }
    }
}

using Newtonsoft.Json;

namespace GRP.AppWeb.Models
{
    public class Articulo
    {
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Nombre { get; set; }

        [JsonProperty("description")]
        public string Descripcion { get; set; }

        [JsonProperty("unitOfMeasurement")]
        public string UnidadMedida { get; set; }

        [JsonProperty("type")]
        public string TipoArticulo { get; set; }

        [JsonProperty("price")]
        public decimal CostoxUM { get; set; }
    }
}
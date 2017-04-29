using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GRP.AppWeb.Models
{
    public class ProductoArticulo
    {
        [JsonProperty("qty")]
        public decimal Cantidad { get; set; }

        [JsonProperty("cost")]
        [Required]
        public decimal Costo { get; set; }

        [JsonProperty("productId")]
        public int IdProducto { get; set; }

        [JsonProperty("itemId")]
        public int IdArticulo { get; set; }

        [JsonProperty("description")]
        public string Descripcion { get; set; }

        [JsonProperty("unitOfMeasurement")]
        public string UnidadMedida { get; set; }

        [JsonProperty("calories")]
        public decimal Calorias { get; set; }

        [JsonProperty("proteins")]
        public decimal Proteinas { get; set; }

        [JsonProperty("carbohydrates")]
        public decimal Carbohidratos { get; set; }

        [JsonProperty("fats")]
        public decimal Grasas { get; set; }

        [JsonProperty("recipeYield")]
        public decimal Rendimiento { get; set; }

        public decimal Precio
        {
            get { return decimal.Round(Cantidad * Costo, 2); }
        }
    }
}
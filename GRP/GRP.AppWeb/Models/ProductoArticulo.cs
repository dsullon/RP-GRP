using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GRP.AppWeb.Models
{
    public class ProductoArticulo
    {
        [JsonProperty("qty")]
        [Required(ErrorMessage = "La cantidad es un dato obligatorio")]
        [Range(0.01, 100.00,
            ErrorMessage = "La cantidad debe estar entre 0.01 y 100.00")]
        public decimal Cantidad { get; set; }

        [JsonProperty("cost")]
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
        [Required(ErrorMessage = "El rendimiento es un dato obligatorio")]
        [Range(0.01, 1.00,
            ErrorMessage = "El rendimiento debe estar entre 0.01 y 1.00")]
        public decimal Rendimiento { get; set; }

        public decimal Precio
        {
            get { return decimal.Round((Cantidad / Rendimiento) * Costo, 2); }
        }
    }
}
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GRP.AppWeb.Models
{
    public class ProductoArticulo
    {
        [JsonProperty("qty")]
        public int Cantidad { get; set; }

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

        public decimal Precio
        {
            get { return Cantidad * Costo; }
        }
    }
}
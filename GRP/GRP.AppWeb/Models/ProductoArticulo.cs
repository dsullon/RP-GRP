using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web;

namespace GRP.AppWeb.Models
{
    public class ProductoArticulo
    {
        [JsonProperty("qty")]
        public int Cantidad { get; set; }

        [JsonProperty("cost")]
        public short Costo { get; set; }

        [JsonProperty("productId")]
        public int IdProducto { get; set; }

        [JsonProperty("itemId")]
        public int IdArticulo { get; set; }

        [JsonProperty("description")]
        public string Descripcion { get; set; }

        [JsonProperty("unitOfMeasurement")]
        public string UnidadMedida { get; set; }

        [JsonProperty("price")]
        public decimal Precio { get; set; }        
    }
}
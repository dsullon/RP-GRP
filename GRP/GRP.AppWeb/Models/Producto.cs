using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web;

namespace GRP.AppWeb.Models
{
    public class Producto
    {
        public Producto()
        {
            ProductoArticulo = new List<ProductoArticulo>();
        }
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Nombre { get; set; }

        [JsonProperty("directions")]
        public string Elaboracion { get; set; }

        [JsonProperty("cost")]
        public decimal Costo { get; set; }

        [JsonProperty("costThreshold")]
        public decimal UmbralCosto { get; set; }

        [JsonProperty("price")]
        public decimal Precio { get; set; }

        [JsonProperty("status")]
        public bool Estado { get; set; }

        [JsonProperty("calories")]
        public decimal Calorias { get; set; }

        [JsonProperty("proteins")]
        public decimal Proteinas { get; set; }

        [JsonProperty("carbohydrates")]
        public decimal Carbohidratos { get; set; }

        [JsonProperty("fats")]
        public decimal Grasas { get; set; }

        [JsonProperty("type")]
        public string Tipo { get; set; }

        [JsonProperty("servings")]
        public int Porciones { get; set; }

        [JsonProperty("recipeYield")]
        public decimal Rendimiento { get; set; }

        public List<ProductoArticulo> ProductoArticulo { get; set; }

        //public virtual ICollection<T_Combo> T_Combo { get; set; }
    }
}
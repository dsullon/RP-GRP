using Newtonsoft.Json;
using System.Collections.Generic;

namespace GRP.AppWeb.Models
{
    public class Producto
    {
        public Producto()
        {
            Ingredientes = new List<ProductoArticulo>();
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

        public decimal Utilidad { get { return 20; } }

        [JsonProperty("items")]
        public IEnumerable<ProductoArticulo> Ingredientes { get; set; }

        //public virtual ICollection<T_Combo> T_Combo { get; set; }
    }
}
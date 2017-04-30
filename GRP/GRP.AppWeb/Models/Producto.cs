using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage ="El dato de elaboración es obligatorio")]
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
        [Required(ErrorMessage ="La clasificación es una dato obligatorio")]
        public string Tipo { get; set; }

        [JsonProperty("servings")]
        [Required(ErrorMessage = "El n° de porciones es obligatorio")]
        [Range(1.00,20.00,ErrorMessage = "La cantidad debe estar entre 1 y 20")]
        public int Porciones { get; set; }

        [JsonProperty("recipeYield")]
        public decimal Rendimiento { get; set; }

        public decimal Utilidad { get { return 20; } }

        [JsonProperty("items")]
        public IEnumerable<ProductoArticulo> Ingredientes { get; set; }

        //public virtual ICollection<T_Combo> T_Combo { get; set; }
    }
}
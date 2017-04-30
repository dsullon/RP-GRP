using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GRP.AppWeb.Models
{
    public class Combo
    {
        public Combo()
        {
            Productos = new List<Producto>();
        }
        public int Id { get; set; }

        [JsonProperty("name")]
        [Required(ErrorMessage = "El dato de nombre es obligatorio")]
        public string Nombre { get; set; }

        [JsonProperty("description")]
        [Required(ErrorMessage = "El dato de descripción es obligatorio")]
        public string Descripcion { get; set; }

        [JsonProperty("price")]
        [Required(ErrorMessage = "El dato de precio es obligatorio")]
        [Range(1.00, 100.00, ErrorMessage = "La cantidad debe estar entre 1.00 y 100.")]
        public decimal PrecioActual { get; set; }

        [JsonProperty("products")]
        public IEnumerable<Producto> Productos { get; set; }
    }
}
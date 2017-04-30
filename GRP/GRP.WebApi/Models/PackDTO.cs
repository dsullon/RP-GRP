using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GRP.WebApi.Models
{
    public class PackDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}
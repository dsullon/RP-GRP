using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GRP.WebApi.Models
{
    public class ProductItemDTO
    {
        public int Qty { get; set; }
        public short Cost { get; set; }
        public int ProductId { get; set; }
        public int ItemId { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal Price { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GRP.Entidades;
using GRP.Negocio;

namespace GRP.WebApi.Controllers
{
    public class ProductsController : ApiController
    {
        public IEnumerable<T_Producto> GetAll()
        {
            return LNProducto.ListarTodos();
        }
        public IHttpActionResult GetProduct(int id)
        {
            var product = LNProducto.Obtener(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        
    }
}
using GRP.Entidades;
using GRP.Negocio;
using System.Collections.Generic;
using System.Web.Http;

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
using GRP.Negocio;
using GRP.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GRP.WebApi.Controllers
{
    public class ItemsController : ApiController
    {
        public List<ItemDTO> GetAll()
        {
            var items = from b in LNArticulo.ListarTodos()
                        select new ItemDTO()
                        {
                            Id = b.codArticulo,
                            Name = b.nombre,
                            Description = b.descripcion,
                            UnitOfMeasurement = b.unidadMedida,
                            Type = b.tipoArticulo,
                            Price = b.costoxUM
                        };
            return items.ToList();
        }

        public IHttpActionResult GetItem(int id)
        {
            var item = LNArticulo.Obtener(id);
            if (item == null)
                return NotFound();
            else
            {
                var newItem = new ItemDTO()
                {
                    Id = item.codArticulo,
                    Name = item.nombre,
                    Description = item.descripcion,
                    UnitOfMeasurement = item.unidadMedida,
                    Type = item.tipoArticulo,
                    Price = item.costoxUM
                };
                return Ok(newItem);
            }
        }
    }
}

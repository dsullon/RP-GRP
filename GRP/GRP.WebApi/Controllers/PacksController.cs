using GRP.Entidades;
using GRP.Negocio;
using GRP.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GRP.WebApi.Controllers
{
    [RoutePrefix("api/packs")]
    public class PacksController : ApiController
    {
        [Route("")]
        public List<PackDTO> GetAll()
        {
            try
            {
                var packs = from b in LNCombo.ListarTodos()
                            select new PackDTO()
                            {
                                Id = b.codCombo,
                                Name = b.nombre,
                                Description = b.descripcion,
                                Price = b.precio
                            };
                return packs.ToList();
            }
            catch (System.Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(ex.Message),
                    ReasonPhrase = "There was an error processing the request"
                };
                throw new HttpResponseException(resp);
            }

        }

        [Route("{id}")]
        [HttpGet]
        public PackDTO GetPack(int id)
        {
            var pack = LNCombo.Obtener(id);
            if (pack == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No pack with ID = {0}", id)),
                    ReasonPhrase = "Pack ID Not Found"
                };
                throw new HttpResponseException(resp);
            }
            else
            {
                var newPack = new PackDTO()
                {
                    Id = pack.codCombo,
                    Name = pack.nombre,
                    Description = pack.descripcion,
                    Price = pack.precio
                };
                return newPack;
            }
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Create([FromBody]PackDTO pack)
        {
            var nuevoCombo = new  T_Combo();
            nuevoCombo.nombre = pack.Name;
            nuevoCombo.descripcion = pack.Description;
            nuevoCombo.precio = pack.Price;
            T_Producto prod = null;
            foreach (var item in pack.Products)
            {
                prod = new T_Producto();
                prod.codProducto = item.Id;
                prod.nombre = item.Name;
                nuevoCombo.T_Producto.Add(prod);
            }
            var status = LNCombo.Grabar(nuevoCombo);
            if (status)
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}

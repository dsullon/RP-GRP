using GRP.Negocio;
using GRP.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GRP.WebApi.Controllers
{
    [RoutePrefix("api/items")]
    public class ItemsController : ApiController
    {
        [Route("")]
        public List<ItemDTO> GetAll()
        {
            try
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
        public IHttpActionResult GetItem(int id)
        {
            var item = LNArticulo.Obtener(id);
            if (item == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No item with ID = {0}", id)),
                    ReasonPhrase = "Items ID Not Found"
                };
                throw new HttpResponseException(resp);
            }
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

        [Route("{id}/info")]
        public IHttpActionResult GetInformation(int id)
        {
            var info = LNArticulo.ObtenerInformacionNutricional(id);
            if (info == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No info with ID = {0}", id)),
                    ReasonPhrase = "Info ID Not Found"
                };
                throw new HttpResponseException(resp);
            }
            else
            {
                var newItem = new NutritionalFactsDTO()
                {
                    Id = info.codigoInfNut,
                    ItemID = info.codArticulo,
                    Calories = info.calorias,
                    Proteins = info.proteinas,
                    Carbohydrates = info.carbohidratos,
                    Fats = info.grasas
                };
                return Ok(newItem);
            }
        }
    }
}

using GRP.Negocio;
using GRP.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GRP.WebApi.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        [Route("")]
        public List<ProductDTO> GetAll()
        {
            try
            {
                var products = from b in LNProducto.ListarTodos()
                            select new ProductDTO()
                            {
                                Id = b.codProducto,
                                Name = b.nombre,
                                Directions = b.elaboracion,
                                Cost = b.costo,
                                CostThreshold = b.umbralCosto,
                                Price = b.precio,
                                Status = b.estado,
                                Calories = b.calorias,
                                Proteins = b.proteinas,
                                Carbohydrates = b.carbohidratos,
                                Fats = b.grasas,
                                Type = b.tipo,
                                Servings = b.porciones,
                                RecipeYield = b.rendimiento
                            };
                return products.ToList();
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
        public ProductDTO GetItem(int id)
        {
            var product = LNProducto.Obtener(id);
            if (product == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product with ID = {0}", id)),
                    ReasonPhrase = "Product ID Not Found"
                };
                throw new HttpResponseException(resp);
            }
            else
            {
                var newProduct = new ProductDTO()
                {
                    Id = product.codProducto,
                    Name = product.nombre,
                    Directions = product.elaboracion,
                    Cost = product.costo,
                    CostThreshold = product.umbralCosto,
                    Price = product.precio,
                    Status = product.estado,
                    Calories = product.calorias,
                    Proteins = product.proteinas,
                    Carbohydrates = product.carbohidratos,
                    Fats = product.grasas,
                    Type = product.tipo,
                    Servings = product.porciones,
                    RecipeYield = product.rendimiento
                };
                return newProduct;
            }
        }
    }
}
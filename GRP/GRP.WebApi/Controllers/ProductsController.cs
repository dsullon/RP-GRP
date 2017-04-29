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
        public ProductDTO GetProduct(int id)
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

        [Route("{id}/items")]
        [HttpGet]
        public List<ProductItemDTO> GetItems(int id)
        {
            try
            {
                var products = from b in LNProducto.ListarArticulos(id)
                               select new ProductItemDTO()
                               {
                                   Qty = b.cantidad,
                                   Cost = b.costo,
                                   ProductId = b.codProducto,
                                   ItemId = b.codArticulo,
                                   Description = b.T_Articulo.descripcion,
                                   UnitOfMeasurement = b.T_Articulo.unidadMedida,
                                   Price = b.T_Articulo.costoxUM,

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

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Create([FromBody]ProductDTO product)
        {
            var nuevoProducto = new T_Producto();
            nuevoProducto.nombre = product.Name;
            nuevoProducto.elaboracion = product.Directions;
            nuevoProducto.costo = product.Cost;
            nuevoProducto.umbralCosto = product.CostThreshold;
            nuevoProducto.precio = product.Price;
            nuevoProducto.estado = true;
            nuevoProducto.calorias = product.Calories;
            nuevoProducto.proteinas = product.Proteins;
            nuevoProducto.carbohidratos = product.Carbohydrates;
            nuevoProducto.grasas = product.Fats;
            nuevoProducto.tipo = product.Type;
            nuevoProducto.porciones = product.Servings;
            nuevoProducto.rendimiento = product.RecipeYield;
            T_ArticuloProducto nuevoIngrediente = null;
            foreach (var item in product.Items)
            {
                nuevoIngrediente = new T_ArticuloProducto();
                nuevoIngrediente.
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
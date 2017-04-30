using GRP.AppWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GRP.AppWeb.Controllers
{
    public class ProductItemController : Controller
    {
        // GET: ProductItem
        public ActionResult Index()
        {
            List<ProductoArticulo> listado = Session["Ingredientes"] as List<ProductoArticulo>;
            return PartialView("_Index", listado);
        }

        [ChildActionOnly]
        public ActionResult List(int id)
        {
            ViewBag.ProductoID = id;
            List<ProductoArticulo> listado = new List<ProductoArticulo>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlApi"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = null;
                Task.Factory.StartNew(async () =>
                {
                    Res = await client.GetAsync(string.Format("products/items/{0}", id)).ConfigureAwait(false);
                });
                if (Res != null && Res.IsSuccessStatusCode)
                {
                    var responseData = Res.Content.ReadAsStringAsync().Result;
                    listado = JsonConvert.DeserializeObject<List<ProductoArticulo>>(responseData);
                }
                return PartialView("_List", listado);
            }
        }

        public ActionResult Create()
        {
            ProductoArticulo articulo = new ProductoArticulo();
            //articulo.IdProducto = ProductoID;
            ModelState.Clear();
            return PartialView("_Create", articulo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArticulo,Cantidad,Costo,UnidadMedida,Descripcion,Calorias,Proteinas,Grasa,Carbohidratos,Rendimiento")] ProductoArticulo articulo)
        {
            if (ModelState.IsValid)
            {
                List<ProductoArticulo> listado = Session["Ingredientes"] as List<ProductoArticulo>;
                articulo.Calorias = articulo.Calorias * articulo.Cantidad;
                articulo.Carbohidratos = articulo.Carbohidratos * articulo.Cantidad;
                articulo.Grasas = articulo.Grasas * articulo.Cantidad;
                articulo.Proteinas = articulo.Proteinas * articulo.Cantidad;
                listado.Add(articulo);
                Session["Ingredientes"] = listado;
                string url = Url.Action("Index", "ProductItem");
                return Json(new { success = true, url = url });
            }
            else
            {
                return PartialView("_Create", articulo);
            }
        }

        public async Task<ActionResult> ItemPartial()
        {
            List<Articulo> listado = new List<Articulo>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlApi"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("items");
                if (Res.IsSuccessStatusCode)
                {
                    var responseData = Res.Content.ReadAsStringAsync().Result;
                    listado = JsonConvert.DeserializeObject<List<Articulo>>(responseData);
                }
                return PartialView("_ItemPartial", listado);
            }

            //return PartialView("_ItemPartial");
        }

        private List<Articulo> Listado()
        {
            var webClient = new WebClient();
            var json = webClient.DownloadString(string.Format("{0}/items", ConfigurationManager.AppSettings["UrlApi"]));
            List<Articulo> lista = JsonConvert.DeserializeObject<List<Articulo>>(json);
            //var js = new JavaScriptSerializer();
            //var lista = js.Deserialize<List<Articulo>>(json);
            return lista;

            //try
            //{
            //    HttpWebRequest request = WebRequest.Create(string.Format("/items", ConfigurationManager.AppSettings["UrlApi"])) as HttpWebRequest;
            //    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //    {
            //        if (response.StatusCode != HttpStatusCode.OK)
            //        {
            //            throw new Exception(String.Format("Server error (HTTP {0}: {1}).", 
            //                response.StatusCode, response.StatusDescription));
            //        }
            //        //return JsonConvert.DeserializeObject<List<Articulo>>(response.ToString());
            //        JavaScriptSerializer serializer = new JavaScriptSerializer();
            //        var responseObject = serializer.Deserialize<List<Articulo>>(response);

            //        return responseObject;
            //    }
            //}
            //catch (Exception e)
            //{
            //    // catch exception and log it
            //    return null;
            //}



            //List<Articulo> listado = new List<Articulo>();

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlApi"]);
            //    client.DefaultRequestHeaders.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    HttpResponseMessage Res = client.Get("items");
            //    if (Res != null && Res.IsSuccessStatusCode)
            //    {
            //        var responseData = Res.Content.ReadAsStringAsync().Result;
            //        listado = JsonConvert.DeserializeObject<List<Articulo>>(responseData);
            //    }
            //    return listado;
            //}
        }

        public JsonResult ListaPaginada(int offset, int limit, string search, string sort, string order)
        {
            var people = Listado() as List<Articulo>;
            var model = new
            {
                total = people.Count(),
                rows = people.Skip((offset / limit) * limit).Take(limit),
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListadoBusqueda(int offset, int limit, string search, string sort, string order)
        {
            var people = Listado().AsQueryable()
                .WhereIf(!string.IsNullOrEmpty(search), o =>
                    o.Nombre.Contains(search, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(sort ?? "Nombre", order)
                .ToList();

            var model = new
            {
                total = people.Count(),
                rows = people.Skip((offset / limit) * limit).Take(limit),
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

    }
}
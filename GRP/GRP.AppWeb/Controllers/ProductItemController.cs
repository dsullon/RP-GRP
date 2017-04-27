using GRP.AppWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GRP.AppWeb.Controllers
{
    public class ProductItemController : Controller
    {
        // GET: ProductItem
        public ActionResult Index()
        {
            List<ProductoArticulo> listado = Session["Ingredientes"] as List<ProductoArticulo>;
            return PartialView("_Index", listado);
            //ViewBag.ProductoID = id;
            //List<ProductoArticulo> listado = new List<ProductoArticulo>();
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlApi"]);
            //    client.DefaultRequestHeaders.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    HttpResponseMessage Res = null;
            //    Task.Factory.StartNew(async () =>
            //    {
            //        Res = await client.GetAsync(string.Format("products/items/{0}", id)).ConfigureAwait(false);
            //    });
            //    if (Res != null && Res.IsSuccessStatusCode)
            //    {
            //        var responseData = Res.Content.ReadAsStringAsync().Result;
            //        listado = JsonConvert.DeserializeObject<List<ProductoArticulo>>(responseData);
            //    }
            //    return PartialView("_Index", listado);
            //}
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
        public ActionResult Create([Bind(Include = "Cantidad,Costo,UnidadMedida,Descripcion")] ProductoArticulo articulo)
        {
            if (ModelState.IsValid)
            {
                List<ProductoArticulo> listado = Session["Ingredientes"] as List<ProductoArticulo>;
                listado.Add(articulo);
                Session["Ingredientes"] = listado;
                string url = Url.Action("Index", "ProductItem");
                return Json(new { success = true, url = url });
            }

            return PartialView("_Create");
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
        }

    }
}
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
using System.Text;
using System.Web.Script.Serialization;

namespace GRP.AppWeb.Controllers
{
    public class PackController : Controller
    {

        private List<Combo> Listado()
        {
            var webClient = new WebClient();
            var json = webClient.DownloadString(string.Format("{0}/packs", ConfigurationManager.AppSettings["UrlApi"]));
            List<Combo> lista = JsonConvert.DeserializeObject<List<Combo>>(json);
            return lista;
        }

        private List<Producto> ListadoProductos()
        {
            var webClient = new WebClient();
            var json = webClient.DownloadString(string.Format("{0}/products", ConfigurationManager.AppSettings["UrlApi"]));
            List<Producto> lista = JsonConvert.DeserializeObject<List<Producto>>(json);
            return lista;
        }

        public JsonResult ListadoBusqueda(int offset, int limit, string search, string sort, string order)
        {
            var lista = Listado().AsQueryable()
                .WhereIf(!string.IsNullOrEmpty(search), o =>
                    o.Nombre.Contains(search, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(sort ?? "Nombre", order)
                .ToList();

            var model = new
            {
                total = lista.Count(),
                rows = lista.Skip((offset / limit) * limit).Take(limit),
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: Pack
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var p = new Combo();
            Session["Productos"] = p.Productos;
            return View(p);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Combo combo)
        {
            List<Producto> listado = Session["Productos"] as List<Producto>;
            if (ModelState.IsValid && listado.Count > 0)
            {
                combo.Productos = listado;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlApi"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    StringContent content = new StringContent(JsonConvert.SerializeObject(combo), Encoding.UTF8, "application/json");
                    // HTTP POST

                    HttpResponseMessage response = await client.PostAsync("packs", content);
                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        combo = JsonConvert.DeserializeObject<Combo>(data);
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(combo);
        }

        public ActionResult ProductsDetail()
        {
            List<Producto> listado = Session["Productos"] as List<Producto>;
            return PartialView("_PackDetails", listado);
        }

        public ActionResult AddProducto()
        {
            Producto producto = new Producto();
            ModelState.Clear();
            return PartialView("_AddProducto", producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProducto([Bind(Include = "Id,Nombre,Precio")] Producto producto)
        {
            List<Producto> listado = Session["Productos"] as List<Producto>;
            listado.Add(producto);
            Session["Productos"] = listado;
            string url = Url.Action("ProductsDetail", "Pack");
            return Json(new { success = true, url = url });
        }

        public ActionResult ProductPartial()
        {
            var webClient = new WebClient();
            var json = webClient.DownloadString(string.Format("{0}/products", ConfigurationManager.AppSettings["UrlApi"]));
            List<Producto> lista = JsonConvert.DeserializeObject<List<Producto>>(json);
            return PartialView("_ProductPartial", lista);
        }
        
        public JsonResult ListadoBusquedaProducto(int offset, int limit, string search, string sort, string order)
        {
            var people = ListadoProductos().AsQueryable()
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
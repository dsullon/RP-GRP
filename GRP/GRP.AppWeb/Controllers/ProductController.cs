using GRP.AppWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Text;

namespace GRP.AppWeb.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public async Task<ActionResult> Index()
        {
            List<Producto> listado = new List<Producto>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlApi"]);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("products");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var responseData = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    listado = JsonConvert.DeserializeObject<List<Producto>>(responseData);

                }
                //returning the employee list to view  
                return View(listado);
            }
        }

        public ActionResult Create()
        {
            var listaClasificacion = new List<SelectListItem>();
            listaClasificacion.Add(new SelectListItem { Text = "Principal", Value = "Principal" });
            listaClasificacion.Add(new SelectListItem { Text = "Ensaladas", Value = "Ensaladas" });
            listaClasificacion.Add(new SelectListItem { Text = "Sopas", Value = "Sopa" });
            listaClasificacion.Add(new SelectListItem { Text = "Postres", Value = "Postres" });

            ViewBag.Clasificacion = listaClasificacion;
            var p = new Producto();
            p.UmbralCosto = 10;
            Session["Ingredientes"] = p.Ingredientes;
            return View(p);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Producto producto)
        {
            List<ProductoArticulo> listado = Session["Ingredientes"] as List<ProductoArticulo>;
            if (ModelState.IsValid && listado.Count > 0)
            {
                producto.Ingredientes = listado;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlApi"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    StringContent content = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");
                    // HTTP POST

                    HttpResponseMessage response = await client.PostAsync("products", content);
                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        producto = JsonConvert.DeserializeObject<Producto>(data);
                    }
                }
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        public ViewResult BlankEditorRow()
        {
            return View("ProductEditRow", new ProductoArticulo());
        }
    }
}
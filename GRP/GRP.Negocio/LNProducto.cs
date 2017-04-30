using GRP.Datos;
using GRP.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace GRP.Negocio
{
    public class LNProducto
    {
        public static List<T_Producto> ListarTodos()
        {
            BD_CRPEntities db = new BD_CRPEntities();
            return db.T_Producto.ToList();
        }

        public static T_Producto Obtener(int id)
        {
            BD_CRPEntities db = new BD_CRPEntities();
            return db.T_Producto.FirstOrDefault(x => x.codProducto == id);
        }

        public static List<T_ArticuloProducto> ListarArticulos(int idProducto)
        {
            BD_CRPEntities db = new BD_CRPEntities();
            return db.T_ArticuloProducto.Where(x=> x.codProducto == idProducto).ToList();
        }

        public static bool Grabar(T_Producto producto)
        {
            var estado = false;
            BD_CRPEntities db = new BD_CRPEntities();
            var prod = new T_Producto();
            prod.nombre = producto.nombre;
            prod.elaboracion = producto.elaboracion;
            prod.costo = producto.costo;
            prod.umbralCosto = producto.umbralCosto;
            prod.precio = producto.precio;
            prod.estado = true;
            prod.calorias = producto.calorias;
            prod.proteinas = producto.proteinas;
            prod.carbohidratos = producto.carbohidratos;
            prod.grasas = producto.grasas;
            prod.tipo = producto.tipo;
            prod.porciones = producto.porciones;
            prod.rendimiento = producto.rendimiento;
            db.T_Producto.Add(prod);
            var ingr = new T_ArticuloProducto();
            foreach (var item in producto.T_ArticuloProducto)
            {
                ingr = new T_ArticuloProducto();
                ingr.cantidad = item.cantidad;
                ingr.costo = item.costo;
                ingr.codArticulo = item.codArticulo;
                ingr.calorias = item.calorias;
                ingr.proteinas = item.proteinas;
                ingr.carbohidratos = item.carbohidratos;
                ingr.grasas = item.grasas;
                ingr.rendimiento = item.rendimiento;
                ingr.unidadMedida = item.unidadMedida;
                ingr.T_Producto = prod;
                db.T_ArticuloProducto.Add(ingr);
            }
            db.SaveChanges();
            estado = true;
            return estado;
        }
    }
}
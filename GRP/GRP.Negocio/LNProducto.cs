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
    }
}
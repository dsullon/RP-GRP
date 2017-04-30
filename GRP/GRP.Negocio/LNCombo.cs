using GRP.Datos;
using GRP.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace GRP.Negocio
{
    public class LNCombo
    {
        public static List<T_Combo> ListarTodos()
        {
            BD_CRPEntities db = new BD_CRPEntities();
            return db.T_Combo.ToList();
        }

        public static T_Combo Obtener(int id)
        {
            BD_CRPEntities db = new BD_CRPEntities();
            return db.T_Combo.FirstOrDefault(x => x.codCombo == id);
        }

        //public static List<T_Producto> ListarArticulos(int idProducto)
        //{
        //    BD_CRPEntities db = new BD_CRPEntities();
        //    return db.T_Producto.Where(x => x.T_Combo. == idProducto).ToList();
        //}

        public static bool Grabar(T_Combo combo)
        {
            var estado = false;
            BD_CRPEntities db = new BD_CRPEntities();
            var cmb = new T_Combo();
            cmb.nombre = combo.nombre;
            cmb.descripcion = combo.descripcion;
            cmb.precio = combo.precio;
            db.T_Combo.Add(cmb);
            var ingr = new T_ArticuloProducto();
            foreach (var item in combo.T_Producto)
            {
                var prod = db.T_Producto.FirstOrDefault(x => x.codProducto == item.codProducto);
                cmb.T_Producto.Add(prod);
            }
            db.SaveChanges();
            estado = true;
            return estado;
        }
    }
}

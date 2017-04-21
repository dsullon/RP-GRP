using GRP.Datos;
using GRP.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace GRP.Negocio
{
    public class LNArticulo
    {
        public static List<T_Articulo> ListarTodos()
        {
            BD_CRPEntities db = new BD_CRPEntities();
            return db.T_Articulo.ToList();
        }

        public static T_Articulo Obtener(int id)
        {
            BD_CRPEntities db = new BD_CRPEntities();
            return db.T_Articulo.FirstOrDefault(x => x.codArticulo == id);
        }
    }
}

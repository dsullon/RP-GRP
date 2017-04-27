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

        public static T_InformacionNutricional ObtenerInformacionNutricional(int idArticulo)
        {
            BD_CRPEntities db = new BD_CRPEntities();
            return db.T_InformacionNutricional.FirstOrDefault(x => x.codArticulo == idArticulo);
        }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GRP.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Producto()
        {
            this.T_ArticuloProducto = new HashSet<T_ArticuloProducto>();
            this.T_Combo = new HashSet<T_Combo>();
        }
    
        public int codProducto { get; set; }
        public string nombre { get; set; }
        public string elaboracion { get; set; }
        public decimal costo { get; set; }
        public decimal umbralCosto { get; set; }
        public decimal precio { get; set; }
        public bool estado { get; set; }
        public decimal calorias { get; set; }
        public decimal proteinas { get; set; }
        public decimal carbohidratos { get; set; }
        public decimal grasas { get; set; }
        public string tipo { get; set; }
        public int porciones { get; set; }
        public decimal rendimiento { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_ArticuloProducto> T_ArticuloProducto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Combo> T_Combo { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppRestaurantSiglo21.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class INSUMO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public INSUMO()
        {
            this.BODEGAMOVIMIENTO = new HashSet<BODEGAMOVIMIENTO>();
            this.DETPEDIDOINS = new HashSet<DETPEDIDOINS>();
            this.INSUMOSTOCK = new HashSet<INSUMOSTOCK>();
        }
    
        public int IDINSUMO { get; set; }
        public string DESCINSUMO { get; set; }
        public int PRECIOINSUMO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BODEGAMOVIMIENTO> BODEGAMOVIMIENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETPEDIDOINS> DETPEDIDOINS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INSUMOSTOCK> INSUMOSTOCK { get; set; }
    }
}

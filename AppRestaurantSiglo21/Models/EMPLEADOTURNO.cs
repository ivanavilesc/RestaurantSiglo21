
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
    
public partial class EMPLEADOTURNO
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public EMPLEADOTURNO()
    {

        this.ORDEN = new HashSet<ORDEN>();

    }


    public decimal IDEMPTURNO { get; set; }

    public Nullable<System.DateTime> FECHATURNO { get; set; }

    public Nullable<decimal> HORADESDE { get; set; }

    public Nullable<decimal> HORAHASTA { get; set; }

    public decimal IDUSUARIO { get; set; }



    public virtual USUARIO USUARIO { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ORDEN> ORDEN { get; set; }

}

}

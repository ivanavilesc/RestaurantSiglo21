
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
    
public partial class MESA
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public MESA()
    {

        this.RESERVA = new HashSet<RESERVA>();

    }


    public decimal IDMESA { get; set; }

    public string DESCMESA { get; set; }

    public Nullable<decimal> CAPACIDAD { get; set; }

    public decimal IDLOCAL { get; set; }

    public decimal IDESTADOMESA { get; set; }



    public virtual ESTADOMESA ESTADOMESA { get; set; }

    public virtual RESTAURANT RESTAURANT { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<RESERVA> RESERVA { get; set; }

}

}


//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace AppRestaurantSiglo21.Models
{

using System;
    using System.Collections.Generic;
    
public partial class ESTADORESERVA
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public ESTADORESERVA()
    {

        this.RESERVA = new HashSet<RESERVA>();

    }


    public decimal IDESTADORESRVA { get; set; }

    public string DESCESTRESERVA { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<RESERVA> RESERVA { get; set; }

}

}

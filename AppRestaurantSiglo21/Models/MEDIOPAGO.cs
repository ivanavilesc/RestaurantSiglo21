
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
    
public partial class MEDIOPAGO
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public MEDIOPAGO()
    {

        this.DOCUMENTOPAGO = new HashSet<DOCUMENTOPAGO>();

        this.MEDIOPAGOTX = new HashSet<MEDIOPAGOTX>();

    }


    public decimal IDMEDIOPAGO { get; set; }

    public string DESCMEDIOPAGO { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<DOCUMENTOPAGO> DOCUMENTOPAGO { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<MEDIOPAGOTX> MEDIOPAGOTX { get; set; }

}

}

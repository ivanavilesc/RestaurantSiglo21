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

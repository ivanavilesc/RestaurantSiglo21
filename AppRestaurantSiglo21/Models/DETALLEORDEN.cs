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
    
    public partial class DETALLEORDEN
    {
        public decimal IDDETALLEORDEN { get; set; }
        public Nullable<decimal> CANTIDAD { get; set; }
        public decimal IDORDEN { get; set; }
        public decimal IDESTADO { get; set; }
    
        public virtual ESTADOORDEN ESTADOORDEN { get; set; }
        public virtual ORDEN ORDEN { get; set; }
    }
}

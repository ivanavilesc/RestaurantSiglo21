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
    
    public partial class INSUMOSTOCK
    {
        public decimal IDPRODUCTOENBODEGA { get; set; }
        public Nullable<decimal> STOCKACTUAL { get; set; }
        public Nullable<decimal> STOCKMINIMO { get; set; }
        public decimal IDINSUMO { get; set; }
    
        public virtual INSUMO INSUMO { get; set; }
    }
}

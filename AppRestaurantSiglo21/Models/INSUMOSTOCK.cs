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
        public int IDINSUMOSTOCK { get; set; }
        public Nullable<short> STOCKACTUAL { get; set; }
        public Nullable<short> STOCKMINIMO { get; set; }
        public int IDINSUMO { get; set; }
    
        public virtual INSUMO INSUMO { get; set; }
    }
}

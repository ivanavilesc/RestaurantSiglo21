
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
    
public partial class PEDIDOINSUMO
{

    public decimal IDPEDIDOINSUMO { get; set; }

    public Nullable<System.DateTime> FECHAPEDIDO { get; set; }

    public decimal IDPEDIDO { get; set; }

    public decimal IDINSUMO { get; set; }



    public virtual INSUMO INSUMO { get; set; }

    public virtual PEDIDO PEDIDO { get; set; }

}

}

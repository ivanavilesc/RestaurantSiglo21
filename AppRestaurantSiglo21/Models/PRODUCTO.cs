
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
    
public partial class PRODUCTO
{

    public decimal IDPRODUCTO { get; set; }

    public string DESCPRODUCTO { get; set; }

    public Nullable<decimal> PRECIOPRODUCTO { get; set; }

    public decimal IDRECPRODUCTO { get; set; }

    public decimal IDPRODPREPARACION { get; set; }

    public decimal IDESTADOPRODUCTO { get; set; }

    public decimal IDTIPOPRODUCTO { get; set; }



    public virtual ESTADOPRODUCTO ESTADOPRODUCTO { get; set; }

    public virtual TIPOPRODUCTO TIPOPRODUCTO { get; set; }

    public virtual PRODUCTOPREPARACION PRODUCTOPREPARACION { get; set; }

    public virtual RECETAPRODUCTO RECETAPRODUCTO { get; set; }

}

}

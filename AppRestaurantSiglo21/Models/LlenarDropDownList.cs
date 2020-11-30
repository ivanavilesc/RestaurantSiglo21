using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AppRestaurantSiglo21.Models
{
    public class LlenarDropDownList
    {
        private int Id;
        private string Descripcion;

        public int Id1 { get => Id; set => Id = value; }
        public string Descripcion1 { get => Descripcion; set => Descripcion = value; }

        private RestaurantEntities db = new RestaurantEntities();

        public List<LlenarDropDownList> ReadAllDoctPagoTipo()
        {
            return db.DOCTPAGOTIPO.Select(c => new LlenarDropDownList()
            {
                Id1 = c.IDDOCTPAGOTIPO,
                Descripcion1 = c.DESCDOCTPAGOTIPO
            }).ToList();

        }
    }

    public class LlenarDropDownListMedioPago
    {
        private int IdMedioPago;
        private string DescMedioPago;

        public int IdMedioPago1 { get => IdMedioPago; set => IdMedioPago = value; }
        public string DescMedioPago1 { get => DescMedioPago; set => DescMedioPago = value; }

        private RestaurantEntities db = new RestaurantEntities();
        public List<LlenarDropDownListMedioPago> ReadAllDoctMedioPago()
        {
            return db.MEDIOPAGO.Select(c => new LlenarDropDownListMedioPago()
            {
                IdMedioPago1 = c.IDMEDIOPAGO,
                DescMedioPago1 = c.DESCMEDIOPAGO
            }).ToList();

        }
    }

    public class LlenarDropDownListEstProveedor
    {
        
        private RestaurantEntities db = new RestaurantEntities();
        public List<DropDownList> ReadAllEstadoProveedor()
        {
            return db.ESTADOPROVEEDOR.Select(c => new DropDownList()
            {
                Id = c.IDESTPROVEEDOR,
                Descripcion = c.DESCESTPRO
            }).ToList();

        }
    }

    public class LlenarDropDownListEstMesas
    {

        private RestaurantEntities db = new RestaurantEntities();
        public List<DropDownList> ReadAllEstadoMesas()
        {
            return db.ESTADOMESA.Select(c => new DropDownList()
            {
                Id = c.IDESTADOMESA,
                Descripcion = c.DESCESTADOMESA
            }).ToList();

        }
    }

    public class LlenarDropDownListaLocal
    {

        private RestaurantEntities db = new RestaurantEntities();
        public List<DropDownList> ReadAllLocal()
        {
            return db.RESTAURANT.Select(c => new DropDownList()
            {
                Id = c.IDLOCAL,
                Descripcion = c.DIRECCIONLOCAL
            }).ToList();

        }
    }

    public class ObtenerTipoInsumos
    {
        public List<DropDownList> ListadoTipoInsumo()
        {
            List<DropDownList> TipoInsumos = new List<DropDownList>();

            DropDownList TipoInsumo1 = new DropDownList();
            //Objeto Tipo Insumo
            TipoInsumo1.Id = 1;
            TipoInsumo1.Descripcion = "BEBESITBLES";
            TipoInsumos.Add(TipoInsumo1);

            DropDownList TipoInsumo2 = new DropDownList();
            //Objeto Tipo Insumo
            TipoInsumo2.Id = 2;
            TipoInsumo2.Descripcion = "CARNES";
            TipoInsumos.Add(TipoInsumo2);

            DropDownList TipoInsumo3 = new DropDownList();
            //Objeto Tipo Insumo
            TipoInsumo3.Id = 3;
            TipoInsumo3.Descripcion = "VERDURAS";
            TipoInsumos.Add(TipoInsumo3);

            DropDownList TipoInsumo4 = new DropDownList();
            //Objeto Tipo Insumo
            TipoInsumo4.Id = 4;
            TipoInsumo4.Descripcion = "LACTEOS";
            TipoInsumos.Add(TipoInsumo4);

            DropDownList TipoInsumo5 = new DropDownList();
            //Objeto Tipo Insumo
            TipoInsumo5.Id = 5;
            TipoInsumo5.Descripcion = "MASAS";
            TipoInsumos.Add(TipoInsumo5);

            DropDownList TipoInsumo6 = new DropDownList();
            //Objeto Tipo Insumo
            TipoInsumo6.Id = 6;
            TipoInsumo6.Descripcion = "CONGELADOS";
            TipoInsumos.Add(TipoInsumo6);

            return TipoInsumos ;
        }
    }
    
}
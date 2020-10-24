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
        
        public List<LlenarDropDownList> ReadAll()
        {
            return db.DOCTPAGOTIPO.Select(c => new LlenarDropDownList()
            {
                Id1 = c.IDDOCTPAGOTIPO,
                Descripcion1 = c.DESCDOCTPAGOTIPO
            }).ToList();

        }
    }
}
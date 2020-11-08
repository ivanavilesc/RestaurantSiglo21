using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AppRestaurantSiglo21.Models
{
    public class RestaurantContext : DbContext
    {
        public DbSet<COLACOCINA> colaCocinaContext { get; set; }
        
    }
}
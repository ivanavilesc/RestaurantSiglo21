using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRestaurantSiglo21.Models
{
    public class VistaAlertaStock
    {
        int IdInsumoStock;
        int IdInsumo;
        string DescInsumo;
        int StockActual;
        int StockMinimo;
        int ColorEstadoAlerta;      //Sin Color, Amarillo, Naranjo, Rojo
        string DescAlerta;

        public VistaAlertaStock()
        {
            
        }

        public VistaAlertaStock(int IdInsumoStock1, int idInsumo, string descInsumo, int stockActual, int stockMinimo, int colorEstadoAlerta, string descAlerta)
        {
            IdInsumoStock = IdInsumoStock1;
            IdInsumo = idInsumo;
            DescInsumo = descInsumo;
            StockActual = stockActual;
            StockMinimo = stockMinimo;
            ColorEstadoAlerta = colorEstadoAlerta;
            DescAlerta = descAlerta;
        }

        public int IdInsumoStock1 { get => IdInsumoStock; set => IdInsumoStock = value; }
        public int IdInsumo1 { get => IdInsumo; set => IdInsumo = value; }
        public string DescInsumo1 { get => DescInsumo; set => DescInsumo = value; }
        public int StockActual1 { get => StockActual; set => StockActual = value; }
        public int StockMinimo1 { get => StockMinimo; set => StockMinimo = value; }
        public int ColorEstadoAlerta1 { get => ColorEstadoAlerta; set => ColorEstadoAlerta = value; }
        public string DescAlerta1 { get => DescAlerta; set => DescAlerta = value; }
        

        // AlertaACortoPlazo        Estas propenso a quedar alcanzar stock minimo
        // AlertaStockMinimo        Estas quedando solo resrva 
        // AlertabajoStockMinimo    Estas bajo la resrva minima;
        // AlertaSinStock           Estas sin stock


    }
}
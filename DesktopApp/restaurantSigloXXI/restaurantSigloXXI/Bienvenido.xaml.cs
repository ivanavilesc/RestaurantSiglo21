using Oracle.ManagedDataAccess.Client;
using restaurantSigloXXI.Formularios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace restaurantSigloXXI
{
    /// <summary>
    /// Interaction logic for Bienvenido.xaml
    /// </summary>
    public partial class Bienvenido : Window
    {
        //OracleConnection conn = null;

        public Bienvenido()
        {
            InitializeComponent();
        }

        private void formIngresos(object sender, RoutedEventArgs e)
        {
            Ingresos ingreso = new Ingresos();
            ingreso.Show();
            this.Close();
        }

        private void formEgresos(object sender, RoutedEventArgs e)
        {
            Egresos egreso = new Egresos();
            egreso.Show();
            this.Close();
        }

        private void formMovimientosCuenta(object sender, RoutedEventArgs e)
        {
            MovimientosCuenta movimientos = new MovimientosCuenta();
            movimientos.Show();
            this.Close();
        }

        private void formPagoConsumo(object sender, RoutedEventArgs e)
        {
            PagoConsumo pago = new PagoConsumo();
            pago.Show();
            this.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void formCierreCaja(object sender, RoutedEventArgs e)
        {
            CierreCaja cierre = new CierreCaja();
            cierre.Show();
            this.Close();
        }
    }
}

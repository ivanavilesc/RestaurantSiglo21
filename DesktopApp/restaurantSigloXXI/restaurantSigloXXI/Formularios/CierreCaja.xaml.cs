using System;
using System.Collections.Generic;
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

namespace restaurantSigloXXI.Formularios
{
    /// <summary>
    /// Interaction logic for CierreCaja.xaml
    /// </summary>
    public partial class CierreCaja : Window
    {
        public CierreCaja()
        {
            InitializeComponent();
        }

        private void TxtResumenCierre_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtResumenCierre.Clear();
        }
    }
}

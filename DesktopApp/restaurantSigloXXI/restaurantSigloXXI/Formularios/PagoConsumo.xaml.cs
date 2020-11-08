using CapaAccesoDatos;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for PagoConsumo.xaml
    /// </summary>
    public partial class PagoConsumo : Window
    {

        OracleConnection conn = null;

        public PagoConsumo()
        {
            this.abrirConexion();
            InitializeComponent();
            conn.Close();
        }

        private void abrirConexion()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["oracleDB"].ConnectionString;
            conn = new OracleConnection(connectionString);

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexion " + ex.Message);
            }
        }


        //SE MANTIENE
        private void actualizarGridPagos()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            

            try
            {
                if (Regex.IsMatch(txtIdPago.Text, @"^\D*$"))
                {
                    MessageBox.Show("No estás ingresando numeros, vuelve a intentar");
                    txtIdPago.Text = "";
                }
                else
                {
                    OracleCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM ORDEN WHERE IDORDEN = '" + txtIdPago.Text + "'";
                    cmd.CommandType = System.Data.CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dgvDetalleOrden.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ", ex.Message);
            }


            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

        }


        private void DgvDetalleOrden_Loaded(object sender, RoutedEventArgs e)
        {
            this.actualizarGrilla(); //ESTA ACTUALIZA AUTOMATICAMENTE LA GRILLA SIN NECESIDAD DE INGRESAR UN ID
        }


        private void actualizarGrilla()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            OracleCommand com = new OracleCommand("SP_LISTARORDENESPORPAGAR", conn);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add("REGISTROS", OracleDbType.RefCursor).Direction=ParameterDirection.Output;

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = com;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dgvDetalleOrden.ItemsSource = tabla.DefaultView;

            conn.Close();

        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //conn.Open();
            try
            {
                if (txtIdPago.Text != null)
                {
                    if (Regex.IsMatch(txtIdPago.Text, @"^\D*$"))
                    {
                        MessageBox.Show("Se está ingresando valores no permitidos ");
                    }
                    else
                    {
                        this.actualizarGridPagos();
                    }
                }

                OracleCommand cmd = conn.CreateCommand();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                else
                {
                    conn.Open();

                    cmd.CommandText = "SELECT  sum(precioprod * cantidad) FROM detalleorden WHERE idorden = '" + txtIdPago.Text + "'";



                    //cmd.CommandText = "SELECT iddetalleorden, cantidad, idorden, idproducto, " +
                    //    "(SELECT b.descproducto FROM producto b WHERE b.idproducto = aidproducto), " +
                    //    "precioprod, (cantidad * precioprod) FROM detalleorden";




                    cmd.CommandType = System.Data.CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    txtTotalPago.Text = dr.GetValue(0).ToString();
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ", ex.Message);
            }
            
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

        }

        ////PRUEBA 1
        //private void actualizarGridPagosEfectivo()
        //{
        //    conn.Open();
        //    DataTable tabla = new DataTable();
        //    OracleCommand cmd = new OracleCommand("FN_LISTAPAGOSEFECTIVO", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
            

        //    //List<Ingreso> listaIngreso = new List<Ingreso>();

        //    OracleParameter output = cmd.Parameters.Add("l_cursor", OracleDbType.RefCursor); //Esto es lo mismo que hubieramos puesto %rowtype en la funcion
        //    output.Direction = ParameterDirection.ReturnValue;
        //    cmd.ExecuteNonQuery();
        //    OracleDataReader dr = ((OracleRefCursor)output.Value).GetDataReader();
        //    tabla.Load(dr);
        //    //IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true); //intento de convertir la fecha de la lina 121
        //    //https://docs.microsoft.com/en-us/dotnet/api/system.datetime.getdatetimeformats?view=netcore-3.1

        //    //while (dr.Read())
        //    //{
        //    //    Ingreso ingreso = new Ingreso();
        //    //    ingreso.IDINGRESO = dr.GetInt32(0);
        //    //    ingreso.MONTO = dr.GetInt32(1);
        //    //    ingreso.DESCINGRESO = dr.GetString(2);
        //    //    ingreso.FECHAMOVIMIENTO = dr.GetDateTime(3); //Hay que ver como convierto esta fecha a dd-mm-yyyy

        //    //    listaIngreso.Add(ingreso);
        //    //}
        //    //dgvIngresos.ItemsSource = listaIngreso;

        //    //dgvIngresos.AllowDrop = false;

        //    dr.Close(); //Cierra la lectura de datos
        //    conn.Close(); //Cierra la conexion (decia conn.Clonne();)
            
        //}


        //PRUEBA 2
        //public DataTable actualizaGridPagar()
        //{
        //    DataTable tabla = new DataTable();
        //    conn.Open();
        //    OracleCommand cmd = new OracleCommand("FN_LISTAPAGOSEFECTIVO", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    OracleParameter output = cmd.Parameters.Add("l_cursor", OracleDbType.RefCursor); //Esto es lo mismo que hubieramos puesto %rowtype en la funcion
        //    output.Direction = ParameterDirection.ReturnValue;
        //    cmd.ExecuteNonQuery();
        //    OracleDataReader dr = ((OracleRefCursor)output.Value).GetDataReader();
        //    dr.Read();
        //    tabla.Load(dr);
        //    dgvDetalleOrden.ItemsSource = dr.Read().ToString();
        //    //dgvDetalleOrden.ItemsSource = dr.GetValue(0).ToString();
        //    dr.Close();
        //    conn.Close();
        //    return tabla;
        //}

       //private void actualizaLaGrid()
       // {
       //     try
       //     {
       //         conn.Open();

       //         OracleCommand cmd = new OracleCommand("FN_LISTAINGRESOS", conn);
       //         cmd.CommandType = CommandType.StoredProcedure;
       //         OracleDataReader dr = cmd.ExecuteReader();
       //         dr.Read();

       //     }
       //     catch (Exception ex)
       //     {
       //         MessageBox.Show("Error ", ex.Message);
       //     }
       // }









        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtIdPago.Text = "";
        }

        private void BtnLimpiarPago_Click(object sender, RoutedEventArgs e)
        {
            txtPagoTotal.Text = "";
            txtPropina.Text = "";
            txtVuelto.Text = "";
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Bienvenido menu = new Bienvenido();
            menu.Owner = this; //Devuelve la vista al menu cuanso se sale apretando este boton
            this.Hide(); //Cierra la ventana actual
            menu.ShowDialog();

            conn.Close();//Cierra la conexión a la BD.
        }

        private void BtnPagar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((txtTotalPago.Text == null) || (txtTotalPago.Text == ""))
                {
                    MessageBox.Show("Para registrar un pago primero debe buscar una orden");
                    txtPagoTotal.Text = "";
                }
                else
                {
                    if (Regex.IsMatch(txtPagoTotal.Text, @"^\D*$"))
                    {
                        MessageBox.Show("Se está ingresando caracteres no válidos ");
                        txtPagoTotal.Text = "";
                    }
                    else
                    {
                        if ((txtPagoTotal.Text == null) || (txtPagoTotal.Text == "") || (int.Parse(txtPagoTotal.Text) <= 0))
                        {
                            MessageBox.Show("No está ingresando valores ");
                        }
                        else
                        {
                            
                            if (conn.State == ConnectionState.Closed)//SI LA CONEXIÓN ESTÁ CERRADA SE ABRE.
                            {
                                conn.Open();
                            }

                            //CALCULA EL VUELTO QUE DEBE ENTREGAR Y/O AVISA SI FALTA DINERO

                            int pago = int.Parse(txtPagoTotal.Text); //Campo donde se ingresa el valor que se está pagando.
                            int cuenta = int.Parse(txtTotalPago.Text); //Campo que indica el total de consumo correspondiente a la orden.
                            

                            if ((txtPropina.Text == "") || (txtPropina.Text == null) || (int.Parse(txtPropina.Text) <= 0))
                            {
                                int vuelto = (pago - cuenta);

                                if (vuelto < 0)
                                {
                                    int falta = (vuelto * -1);
                                    MessageBox.Show("El monto cancelado no cubre el consumo, saldo negativo de " + falta + MessageBoxButton.OK + MessageBoxImage.Error);
                                }
                                else
                                {
                                    txtVuelto.Text = vuelto.ToString();
                                    MessageBox.Show("Vuelto ", vuelto.ToString());

                                    //INGRESA AL SP SIN PROPINA
                                }


                            }
                            else
                            {
                                int propina = int.Parse(txtPropina.Text); //Campo para registrar la propina

                                int vuelto = (pago - cuenta) - propina;

                                if (vuelto < 0)
                                {
                                    int falta = (vuelto * -1);
                                    MessageBox.Show("El monto cancelado no cubre el consumo y la propina sugerida, saldo negativo de " + falta + MessageBoxButton.OK + MessageBoxImage.Error);
                                }
                                else
                                {
                                    txtVuelto.Text = vuelto.ToString();
                                    MessageBox.Show("Vuelto ", vuelto.ToString());

                                    //INGRESA AL SP CON LA PROPINA
                                }

                            }



                            //AQUI EMPIEZA A UTILIZAR EL SP
                            var result = MessageBox.Show("¿Registrar pago?", "Información", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                            if (result == MessageBoxResult.Yes)
                            {
                                OracleCommand com = new OracleCommand("SP_GRABARPAGOORDEN", conn); //IDENTIFICA EL SP Y LA CONEXIÓN
                                com.CommandType = System.Data.CommandType.StoredProcedure; //INDICA QUE SERÁ UN PROCEDIMIENTO ALMACENADO EL QUE SE VA A EJECUTAR

                                //SETEANDO LOS DATOS DE ENTRADA AL SP

                                com.Parameters.Add("PE_idOrden", OracleDbType.Int32, 10).Value = Int32.Parse(txtIdPago.Text); //ID DE LA ORDEN

                                //LA PROPINA PODRÍA IR EN CERO, YA QUE ES OPCIONAL

                                if ((txtPropina.Text == "") || (txtPropina.Text == null))
                                {
                                    //com.Parameters.Add("PE_propina", OracleDbType.Int32, 10).Value = Int32.Parse(txtPropina.Text);
                                    com.Parameters.Add("PE_propina", OracleDbType.Int32, 10).Value = null;
                                }
                                else
                                {
                                    //com.Parameters.Add("PE_propina", OracleDbType.Int32, 10).Value = null;
                                    com.Parameters.Add("PE_propina", OracleDbType.Int32, 10).Value = Int32.Parse(txtPropina.Text);
                                }

                                com.Parameters.Add("PE_tipopago", OracleDbType.Int32, 2).Value = 2; // 2 ES EL TIPO DE PAGO QUE PARA ESTE DESARROLLO SOLO ES CASH COMO ESTÁ DESCRITO EN LA BD
                                com.Parameters.Add("PE_mediopago", OracleDbType.Int32, 2).Value = 1; //1 ES EL MEDIO DE PAGO QUE SIEMPRE SERÁ EFECTIVO  

                                com.ExecuteNonQuery();
                                MessageBox.Show("Ingreso insertado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                            }
                            else
                            {
                                MessageBox.Show("Proceso de pago cancelado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                                txtPagoTotal.Text = "";
                                txtTotalPago.Text = "";
                                txtPropina.Text = "";
                            }



                        }
                    }
                }

                //CAE AQUI CUANDO NO PASA LA PRIMERA VALIDACIÓN DE TODAS


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en el pago ", ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }




    


}

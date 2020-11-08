using CapaAccesoDatos;
using CapaAccesoDatos.ConexionDB;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    /// Interaction logic for Ingresos.xaml
    /// </summary>
    public partial class Ingresos : Window
    {
        OracleConnection conn = null;
        

        public Ingresos()
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
        //Este metodo es para la ventana de Egresos, ya que alli es donde tengo que usar el combobox.


        private void llenarCombo()
        {
            conn.Open();//No es necesario abrir la conexión ya que ya está abierta
            try
            {
                OracleCommand com = new OracleCommand("SELECT DESCINGRESO FROM INGRESO", conn);
                OracleDataReader myReader = com.ExecuteReader();
                myReader = com.ExecuteReader();
                while (myReader.Read())
                {
                    string sname = myReader.GetOracleString(0).ToString();
                    cbxTipoIngreso.Items.Add(sname.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar Combobox ", ex.Message + "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            conn.Close();
        }


        private void actualizarGridIngresos()
        {
            conn.Open();

            OracleCommand cmd = new OracleCommand("FN_LISTAINGRESOS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            List<Ingreso> listaIngreso = new List<Ingreso>();

            OracleParameter output = cmd.Parameters.Add("l_cursor", OracleDbType.RefCursor); //Esto es lo mismo que hubieramos puesto %rowtype en la funcion
            output.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            OracleDataReader dr = ((OracleRefCursor)output.Value).GetDataReader();
            //IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true); //intento de convertir la fecha de la lina 121
            //https://docs.microsoft.com/en-us/dotnet/api/system.datetime.getdatetimeformats?view=netcore-3.1

            while (dr.Read())
            {
                Ingreso ingreso = new Ingreso();
                ingreso.IDINGRESO = dr.GetInt32(0);
                ingreso.MONTO = dr.GetInt32(1);
                ingreso.DESCINGRESO = dr.GetString(2);
                ingreso.FECHAMOVIMIENTO = dr.GetDateTime(3); //Hay que ver como convierto esta fecha a dd-mm-yyyy

                listaIngreso.Add(ingreso);
            }
            dgvIngresos.ItemsSource = listaIngreso;

            dgvIngresos.AllowDrop = false;

            dr.Close(); //Cierra la lectura de datos
            conn.Close(); //Cierra la conexion (decia conn.Clonne();)
        }

        private void DgvIngresos_Loaded(object sender, RoutedEventArgs e)
        {
            this.actualizarGridIngresos(); //Abre y cierra la conexión el mismo método.
        }

        

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                OracleCommand com = new OracleCommand("SP_INSERTARINGRESO", conn);

                //SE ESTA BUSCANDO QUE AL SELECCIONAR UNA FILA EN EL GRID PUEDAN LLENARSE LOS CAMPOS PARA INGRESAR UNA ACTUALIZACIÓN DE DATOS CON EL ID DESHABILITADO
                if (txtIdIngreso.Text.Length > 0) //Se controla que no se ingrese el IDINGRESO, ya que es autoincremental en la BD
                {
                    MessageBox.Show("No puede ingresar ID Ingreso al agregar un nuevo Ingreso");
                    this.Limpiar();
                    conn.Close();
                }
                else
                {
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.Add("PE_MONTO", OracleDbType.Int32, 10).Value = Int32.Parse(txtMontoIngreso.Text);
                    com.Parameters.Add("PE_DESCINGRESO", OracleDbType.Varchar2, 250).Value = txtDescIngreso.Text;
                    com.Parameters.Add("PE_FECHA", OracleDbType.Date).Value = dpFecha.SelectedDate;
                    com.ExecuteNonQuery();
                    MessageBox.Show("Ingreso insertado");
                }

                this.Limpiar();
                this.actualizarGridIngresos();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Insertar ", ex.Message + "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CbxTipoIngreso_Loaded(object sender, RoutedEventArgs e)
        {
            this.llenarCombo();
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //SE ESTA BUSCANDO QUE AL SELECCIONAR UNA FILA EN EL GRID PUEDAN LLENARSE LOS CAMPOS PARA INGRESAR UNA ACTUALIZACIÓN DE DATOS CON EL ID DESHABILITADO
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                else
                {
                    OracleCommand com = new OracleCommand("SP_ACTUALIZARINGRESO", conn);
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.Add("PE_IDINGRESO", OracleDbType.Int32, 10).Value = Int32.Parse(txtIdIngreso.Text);
                    com.Parameters.Add("PE_MONTO", OracleDbType.Int32, 10).Value = Int32.Parse(txtMontoIngreso.Text);
                    com.Parameters.Add("PE_DESCINGRESO", OracleDbType.Varchar2, 100).Value = txtDescIngreso.Text;
                    com.Parameters.Add("PE_FECHAMOVIMIENTO", OracleDbType.Date).Value = dpFecha.SelectedDate;
                    com.ExecuteNonQuery();
                    MessageBox.Show("REGISTRO ACTUALIZADO", "Alerta", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.Limpiar();
                    this.actualizarGridIngresos();
                }
                
                
                if(conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Actualizar ", ex.Message + "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }
        //        else
        //        {
        //            if (Int32.Parse(txtIdIngreso.Text) < 0)
        //            {
        //                MessageBox.Show("ID INGRESO INVALIDO", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
        //                this.Limpiar();
        //            }
        //            else
        //            {
        //                OracleCommand com = new OracleCommand("SP_ELIMINARINGRESO", conn);
        //                com.CommandType = System.Data.CommandType.StoredProcedure;
        //                com.Parameters.Add("PE_IDINGRESO", OracleDbType.Int32, 10).Value = Int32.Parse(txtIdIngreso.Text);
        //                com.ExecuteNonQuery();
        //                MessageBox.Show("REGISTRO ACTUALIZADO", "Alerta", MessageBoxButton.OK, MessageBoxImage.Information);
        //            }
        //            this.Limpiar();
        //            this.actualizarGridIngresos();
        //        }

        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al Eliminar ", ex.Message + "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            this.Limpiar();
        }


        private void Limpiar()
        {
            txtIdIngreso.Text = "";
            txtMontoIngreso.Text = "";
            txtIdIngreso.Text = "";
            dpFecha.SelectedDate = null;
            conn.Close();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Bienvenido menu = new Bienvenido();
            menu.Owner = this; //Devuelve la vista al menu cuanso se sale apretando este boton
            this.Hide(); //Cierra la ventana de los egresos
            menu.ShowDialog();

            conn.Close();//Cierra la conexión a la BD.
            //System.Windows.Application.Current.Shutdown(); //Cierra el programa completo.
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                
                
                    if (Int32.Parse(txtIdIngreso.Text) < 0)
                    {
                        MessageBox.Show("ID INGRESO INVALIDO", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Limpiar();
                    }
                    else
                    {
                        OracleCommand com = new OracleCommand("SP_ELIMINARINGRESO", conn);
                        com.CommandType = System.Data.CommandType.StoredProcedure;
                        com.Parameters.Add("PE_IDINGRESO", OracleDbType.Int32, 10).Value = Int32.Parse(txtIdIngreso.Text);
                        com.ExecuteNonQuery();
                        MessageBox.Show("REGISTRO ACTUALIZADO", "Alerta", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    this.Limpiar();
                    this.actualizarGridIngresos();
                

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Eliminar ", ex.Message + "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

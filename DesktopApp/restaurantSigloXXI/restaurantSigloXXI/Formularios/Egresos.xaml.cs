using CapaAccesoDatos;
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
    /// Interaction logic for Egresos.xaml
    /// </summary>
    public partial class Egresos : Window
    {

        OracleConnection conn = null;

        public Egresos()
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




        private void llenarCombo()
        {
            conn.Open();//No es necesario abrir la conexión ya que ya está abierta
            OracleCommand com = new OracleCommand("SELECT DESCMOVIMIENTO FROM EGRESO", conn);
            try
            {
                OracleDataReader myReader = com.ExecuteReader();
                myReader = com.ExecuteReader();
                while (myReader.Read())
                {
                    string sname = myReader.GetOracleString(0).ToString();
                    cbxTipoEgreso.Items.Add(sname.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar Combobox ", ex.Message + "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            conn.Close();
        }

        //Egresos en realidad
        private void actualizarGridPagos()
        {
            conn.Open();

            OracleCommand cmd = new OracleCommand("FN_LISTAEGRESOS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            List<Egreso> listaEgreso = new List<Egreso>();

            OracleParameter output = cmd.Parameters.Add("l_cursor", OracleDbType.RefCursor); //Esto es lo mismo que hubieramos puesto %rowtype en la funcion
            output.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            OracleDataReader dr = ((OracleRefCursor)output.Value).GetDataReader();
            //IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true); //intento de convertir la fecha de la lina 121
            //https://docs.microsoft.com/en-us/dotnet/api/system.datetime.getdatetimeformats?view=netcore-3.1

            while (dr.Read())
            {
                Egreso egreso = new Egreso();
                egreso.IDEGRESO = dr.GetInt32(0);
                egreso.MONTO = dr.GetInt32(1);
                egreso.DESCMOVIMIENTO = dr.GetString(2);
                egreso.FECHAMOVIMIENTO = dr.GetDateTime(3); //Hay que ver como convierto esta fecha a dd-mm-yyyy

                listaEgreso.Add(egreso);
            }
            dgvPagos.ItemsSource = listaEgreso;


            

            //////////////FORMA 1 DE HACER LA CONSULTA EN LA BD, FUNCIONAL PERO DESCARTADA.
            //OracleCommand cmd = conn.CreateCommand();
            //cmd.CommandText = "SELECT IDCOMPRA, TO_CHAR(FECHAMOVIMIENTO, 'dd-mm-yyyy'), MONTO, " +
            //                  "DESCMOVIMIENTO FROM EGRESO";
            //cmd.CommandType = System.Data.CommandType.Text;
            //OracleDataReader dr = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //dt.Load(dr);
            //dgvPagos.ItemsSource = dt.DefaultView;


            //dgvPagos.Columns[0].Width = 120;
            //dgvPagos.Columns[0].Header = "ID COMPRA";
            //dgvPagos.Columns[1].Width = 50;
            //dgvPagos.Columns[1].Header = "FECHA";
            //dgvPagos.Columns[1].Width = 50;
            //dgvPagos.Columns[1].Header = "MONTO";
            //dgvPagos.Columns[1].Width = 160;
            //dgvPagos.Columns[1].Header = "DESCRIPCION";


            dgvPagos.AllowDrop = false;


            dr.Close(); //Cierra la lectura de datos
            conn.Close(); //Cierra la conexion (decia conn.Clonne();)
        }

        private void DgvPagos_Loaded(object sender, RoutedEventArgs e)
        {
            this.actualizarGridPagos();
        }
             

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();

                OracleCommand com = new OracleCommand("SP_INSERTAREGRESO", conn);

                //SE ESTA BUSCANDO QUE AL SELECCIONAR UNA FILA EN EL GRID PUEDAN LLENARSE LOS CAMPOS PARA INGRESAR UNA ACTUALIZACIÓN DE DATOS CON EL ID DESHABILITADO
                if (txtIdEgreso.Text.Length > 0) //Se controla que no se ingrese el IDEGRESO, ya que es autoincremental en la BD
                {
                    MessageBox.Show("No puede ingresar ID Egreso al agregar un nuevo Egreso");
                    this.Limpiar();
                    conn.Close();
                }
                else
                {
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.Add("PE_monto", OracleDbType.Int32, 6).Value = Int32.Parse(txtMonto.Text);
                    com.Parameters.Add("PE_descmovimiento", OracleDbType.Varchar2, 25).Value = txtDescripcion.Text;
                    com.Parameters.Add("PE_fecha", OracleDbType.Date).Value = dpFecha.SelectedDate;
                    com.ExecuteNonQuery();
                    MessageBox.Show("Egreso insertado");
                }

                this.Limpiar();
                this.actualizarGridPagos();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Insertar ", ex.Message + "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
//SE ESTA BUSCANDO QUE AL SELECCIONAR UNA FILA EN EL GRID PUEDAN LLENARSE LOS CAMPOS PARA INGRESAR UNA ACTUALIZACIÓN DE DATOS CON EL ID DESHABILITADO
                conn.Open();
                OracleCommand com = new OracleCommand("SP_ACTUALIZAREGRESO", conn);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.Add("PE_IDEGRESO", OracleDbType.Int32, 10).Value = Int32.Parse(txtIdEgreso.Text);
                com.Parameters.Add("PE_MONTO", OracleDbType.Int32, 10).Value = Int32.Parse(txtMonto.Text);
                com.Parameters.Add("PE_DESCMOVIMIENTO", OracleDbType.Varchar2, 100).Value = txtDescripcion.Text;
                com.Parameters.Add("PE_FECHAMOVIMIENTO", OracleDbType.Date).Value = dpFecha.SelectedDate;
                com.ExecuteNonQuery();
                MessageBox.Show("REGISTRO ACTUALIZADO", "Alerta", MessageBoxButton.OK, MessageBoxImage.Information);
                
                this.Limpiar();
                this.actualizarGridPagos();
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Actualizar ", ex.Message + "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                if (Int32.Parse(txtIdEgreso.Text) < 0)
                {
                    MessageBox.Show("ID EGRESO INVALIDO", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Limpiar();
                }
                else
                {
                    OracleCommand com = new OracleCommand("SP_ELIMINAREGRESO", conn);
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.Add("PE_IDEGRESO", OracleDbType.Int32, 10).Value = Int32.Parse(txtIdEgreso.Text);
                    com.ExecuteNonQuery();
                    MessageBox.Show("REGISTRO ACTUALIZADO", "Alerta", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                

                this.Limpiar();
                this.actualizarGridPagos();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Eliminar ", ex.Message + "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            conn.Close();
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            this.Limpiar();
            btnAgregar.IsEnabled = true;
            btnActualizar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            conn.Close();

        }

        private void Limpiar()
        {
            txtMonto.Text = "";
            txtDescripcion.Text = "";
            txtIdEgreso.Text = "";
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

        private void CbxTipoEgreso_Loaded(object sender, RoutedEventArgs e)
        {
            llenarCombo();
            conn.Close();
        }

        /*Esta es una forma poco funcional, necesito que se actualice el modelo de la BD para que ya 
         * no exista la relacion entre las tablas EGRESO e INGRESO con otras tablas
         * Además voy a necesitar que se creen los SP para reemplazar estas lineas de codigo que tengo
         * puestas aqui abajo. Es poco funcional y no va a funcionar por que estamos utilizando 
         * autoincremental en los ID.
        */


        //private void AUD(String sql_stmt, int state)
        //{
        //    String msg = "";
        //    OracleCommand cmd = conn.CreateCommand();
        //    cmd.CommandText = sql_stmt;
        //    cmd.CommandType = CommandType.Text;

        //    switch (state)
        //    {
        //        case 0:
        //            try
        //            {
        //                msg = "Fila insertada de forma correcta";
        //                // cmd.Parameters.Add("IDEGRESO", OracleDbType.Int32, 6).Value = Int32.MaxValue; //intentando insertar el ultimo ID en la tabla
        //                cmd.Parameters.Add("MONTO", OracleDbType.Int32, 6).Value = Int32.Parse(txtMonto.Text);
        //                cmd.Parameters.Add("DESCMOVIMIENTO", OracleDbType.Varchar2, 25).Value = txtDescripcion.Text;
        //                cmd.Parameters.Add("FECHAMOVIMIENTO", OracleDbType.Date, 7).Value = dpFecha.SelectedDate;
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Error al insertar ", ex.Message);
        //            }

        //            break;
        //        case 1:
        //            try
        //            {
        //                msg = "Fila actualizada de forma correcta";
        //                //cmd.Parameters.Add("IDEGRESO", OracleDbType.Int32, 10).Value = Int32.MaxValue; //intentando insertar el ultimo ID en la tabla

        //                cmd.Parameters.Add("MONTO", OracleDbType.Int32, 10).Value = Int32.Parse(txtMonto.Text);
        //                cmd.Parameters.Add("DESCMOVIMIENTO", OracleDbType.Varchar2, 100).Value = txtDescripcion.Text;
        //                cmd.Parameters.Add("FECHAMOVIMIENTO", OracleDbType.Date).Value = dpFecha.SelectedDate;
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Error al actualizar los datos ", ex.Message);
        //            }


        //            break;
        //        case 2:
        //            try
        //            {
        //                cmd.Parameters.Add("IDEGRESO", OracleDbType.Int32, 6).Value = Int32.MaxValue; //intentando insertar el ultimo ID en la tabla
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Error al actualizar los datos ", ex.Message);
        //            }
        //            msg = "Fila eliminada de forma correcta";
        //            break;

        //        default:
        //            break;
        //    }

        //    try
        //    {
        //        OracleTransaction txn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
        //        cmd.ExecuteNonQuery();
        //        txn.Commit();


        //            MessageBox.Show(msg);
        //            //this.updateDataGrid(); AQUI EL SOCIO CREO UN METODO QUE ACTUALIZA EL DATAGRID, TENGO QUE SEGUIRLO
        //            this.actualizarGridPagos();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error", ex.Message);
        //    }
        //}




        //SI QUEDA ALGO DE TIEMPO VER ESTE METODO. LO QUE TRATA DE HACER ES QUE CUANDO SE SELECCIONA UNA FILA DEL DATAGRID
        //PASE LOS VALORES A LOS CAMPOS TXT Y DATAPICKER PARA MODIFICARLOS
        //
        private void DgvPagos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                txtIdEgreso.Text = row_selected[0].ToString();

            }


            //DataGrid dg = new DataGrid();
            
            //DataRowView dr = dg.SelectedItem as DataRowView;

            //if (dgvPagos.SelectedCells.Count > 0)
            //{
            //    //NOSE COMO HACER ESTO DESPUES LO VEO
            //    string ID = dgvPagos.SelectedCells.ToString();
            //    //string DES = dr.Row["PE_IDEGRESO"].ToString().Trim();
            //    //string DES = dgvPagos.SelectedValue(dg.sele ["PE_IDEGRESO"]).ToString();
            //    string MON = dgvPagos.SelectionMode.ToString();

            //    txtIdEgreso.Text = ID;
            //    //txtDescripcion.Text = DES;
            //    txtMonto.Text = MON;
            //}

            //if (dr != null)
            //{
            //    //txtIdEgreso.Text = dr.Row["PE_IDEGRESO"].ToString(); //INTENTO DIFERENTE FALLIDO
            //    txtIdEgreso.Text = dr.Row["PE_IDEGRESO"].ToString();
            //    txtIdEgreso.Text = dr["IDEGRESO"].ToString();
            //    //txtMonto.Text = dr["MONTO"].ToString();
            //    //txtDescripcion.Text = dr["DESCMOVIMIENTO"].ToString();
            //    //dpFecha.SelectedDate = DateTime.Parse(dr["FECHAMOVIMIENTO"].ToString());

            //    btnAgregar.IsEnabled = false;
            //    btnActualizar.IsEnabled = true;
            //    btnEliminar.IsEnabled = true;

            //}
        }

    }
}


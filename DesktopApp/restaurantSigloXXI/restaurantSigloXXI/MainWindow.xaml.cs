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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Configuration;
using System.Data;
using System.ComponentModel;
using restaurantSigloXXI.Validaciones;
using FluentValidation;
using CapaAccesoDatos;
using FluentValidation.Results; //Permite acceder a las validaciones de error que puede procesar fluent.
using CapaAccesoDatos.ConexionDB;

namespace restaurantSigloXXI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OracleConnection conn = null; //reemplazado en clase OraConnection

        //Lista de errores Validaciones con Fluent
        

        BindingList<string> errores = new BindingList<string>();
    

        public MainWindow()
        {
            abrirConexion();
            InitializeComponent();
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

        private void GvListado_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Usuario user = new Usuario();

                OracleCommand com = new OracleCommand("SELECT * FROM USUARIO WHERE USERID = :userid AND PASSWORD = :password", conn);

                //user.IDPERSONA = (1); //La consulta de todas formas no está preguntando por el ID, por lo que no importa que no vaya seteado
                user.USERID = txtUsuario.Text.ToUpper();
                user.PASSWORD = txtPassword.Password;

                com.Parameters.Add(":userid", user.USERID);
                com.Parameters.Add(":password", user.PASSWORD);
                OracleDataReader lector = com.ExecuteReader();

                UsuarioValidator validator = new UsuarioValidator();
                FluentValidation.Results.ValidationResult resultados = validator.Validate(user);

                if (resultados.IsValid == false)
                {
                    foreach (ValidationFailure item in resultados.Errors)
                    {
                        MessageBox.Show("Error", item.ErrorMessage, MessageBoxButton.OK, MessageBoxImage.Error); //Este mensaje arrojará todos los errores que hayan en el formulario que estén controlados
                    }
                }
                else
                {
                    if (lector.Read()) // Si no hay errores esto entra aquí y carga la siguiente ventana
                    {
                        MessageBox.Show("Ha ingresado al sistema ");
                        Bienvenido menu = new Bienvenido();
                        conn.Close(); //Cierra la conexión a la BD.
                        this.Close(); //cierra la ventana del login para no poder logear de nuevo.
                        menu.Show(); //Carga la pagina con el menú principal
                    }
                    else //Si la autenticación es erronea entra aqui
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos ", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

                //errores.Clear(); //Si se equivoca limpiará los registros ingresados en el formulario
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexion " + ex.Message, "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.LimpiarUserPass(); //Si se equivoca limpiará los registros ingresados en el formulario
            this.abrirConexion();
            this.InitializeComponent();
        }

        private void LimpiarUserPass()
        {
            txtUsuario.Text = string.Empty;
            txtPassword.Password = string.Empty;
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            conn.Close();
            System.Windows.Application.Current.Shutdown();
        }
    }
}

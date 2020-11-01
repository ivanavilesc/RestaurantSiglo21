using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos.ConexionDB
{
    public class OraConexion
    {

        OracleConnection conn = null;


        public bool abrirConexion()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["oracleDB"].ConnectionString;
            conn = new OracleConnection(connectionString);

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
        }


        public bool cerrarConexion()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["oracleDB"].ConnectionString;
            conn = new OracleConnection(connectionString);

            try
            {
                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
        }

    }
}

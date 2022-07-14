using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombonesPP2022.Datos
{
    public class ConexionBD
    {
        private readonly string cadenaConexion;
        private SqlConnection cn;

        public ConexionBD()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }

        public SqlConnection AbrirConexion()
        {
            try
            {
                cn = new SqlConnection(cadenaConexion);
                cn.Open();
                return cn;
            }
            catch (Exception)
            {
                throw new Exception("no se establecio la conexion");
            }
        }

        public void CerrarConexion()
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
        }

    }
}

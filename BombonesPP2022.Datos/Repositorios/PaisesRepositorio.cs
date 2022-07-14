using BombonesPP2022.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombonesPP2022.Datos.Repositorios
{
    public class PaisesRepositorio
    {
        private readonly ConexionBD conexionBd;


        public PaisesRepositorio()
        {
            conexionBd = new ConexionBD();
        }

        public List<Pais> GetLista()
        {
            var lista = new List<Pais>();
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "SELECT PaisId, NombrePais, RowVersion FROM Paises ORDER BY NombrePais";
                    var comando = new SqlCommand(cadenaComando, cn);
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pais = ConstruirPais(reader);
                            lista.Add(pais);
                        }
                    }
                    return lista;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private Pais ConstruirPais(SqlDataReader reader)
        {
            return new Pais()
            {
                PaisId = reader.GetInt32(0),
                NombrePais = reader.GetString(1),
                RowVersion = (byte[])reader[2]
            };
        }
    }
}

using BombonesPP2022.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombonesPP2022.Datos.Repositorios
{
    public class TiposRellenosRepositorio
    {
        private readonly ConexionBD conexionBd;


        public TiposRellenosRepositorio()
        {
            conexionBd = new ConexionBD();
        }

        public List<TipoDeRelleno> GetLista()
        {
            var lista = new List<TipoDeRelleno>();
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "SELECT TipoRellenoId , Relleno , RowVersion FROM TiposDeRelleno";
                    var comando = new SqlCommand(cadenaComando, cn);
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tipoRelleno = ConstruirTipoRelleno(reader);
                            lista.Add(tipoRelleno);
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

        private TipoDeRelleno ConstruirTipoRelleno(SqlDataReader reader)
        {
            return new TipoDeRelleno()
            {
                TipoRellenoId = reader.GetInt32(0),
                Relleno = reader.GetString(1),
                RowVersion = (byte[])reader[2]
            };
        }

    }
}

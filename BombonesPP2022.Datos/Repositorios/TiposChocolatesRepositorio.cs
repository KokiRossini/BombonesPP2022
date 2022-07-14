using BombonesPP2022.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombonesPP2022.Datos.Repositorios
{
    public class TiposChocolatesRepositorio
    {
        private readonly ConexionBD conexionBd;


        public TiposChocolatesRepositorio()
        {
            conexionBd = new ConexionBD();
        }

        public List<TipoChocolate> GetLista()
        {
            var lista = new List<TipoChocolate>();
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "SELECT TipoChocolateId, Chocolate , RowVersion FROM TiposDeChocolate";
                    var comando = new SqlCommand(cadenaComando, cn);
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tipoChocolate = ConstruirTipoChocolate(reader);
                            lista.Add(tipoChocolate);
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

        private TipoChocolate ConstruirTipoChocolate(SqlDataReader reader)
        {
            return new TipoChocolate()
            {
                TipoChocolateId = reader.GetInt32(0),
                Chocolate = reader.GetString(1),
                RowVersion = (byte[])reader[2]
            };
        }

    }
}

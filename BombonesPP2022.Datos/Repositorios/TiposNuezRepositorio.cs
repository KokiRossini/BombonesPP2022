using BombonesPP2022.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombonesPP2022.Datos.Repositorios
{
    public class TiposNuezRepositorio
    {
        private readonly ConexionBD conexionBd;


        public TiposNuezRepositorio()
        {
            conexionBd = new ConexionBD();
        }

        public List<TipoNuez> GetLista()
        {
            var lista = new List<TipoNuez>();
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "SELECT TipoNuezId, Nuez , RowVersion FROM TipoDeNuez";
                    var comando = new SqlCommand(cadenaComando, cn);
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tipoNuez = ConstruirTipoNuez(reader);
                            lista.Add(tipoNuez);
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

        private TipoNuez ConstruirTipoNuez(SqlDataReader reader)
        {
            return new TipoNuez()
            {
                TipoNuezId = reader.GetInt32(0),
                Nuez = reader.GetString(1),
                RowVersion = (byte[])reader[2]
            };
        }


    }
}

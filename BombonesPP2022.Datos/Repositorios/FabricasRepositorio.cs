using BombonesPP2022.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombonesPP2022.Datos.Repositorios
{
    public class FabricasRepositorio
    {
        private readonly ConexionBD conexionBd;


        public FabricasRepositorio()
        {
            conexionBd = new ConexionBD();
        }

        public List<Fabrica> GetLista()
        {
            var lista = new List<Fabrica>();
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "SELECT FabricaId, NombreFabrica, Direccion, GerenteDeVentas, PaisId, RowVersion FROM Fabricas ORDER BY NombreFabrica";
                    var comando = new SqlCommand(cadenaComando, cn);
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var fabrica = ConstruirFabrica(reader);
                            lista.Add(fabrica);
                        }
                    }
                    SetPais(lista, cn);
                }
                return lista;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int Editar(Fabrica fabrica)
        {
            int registrosAfectados = 0;
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "UPDATE Fabricas SET NombreFabrica=@nom , Direccion=@Dir , GerenteDeVentas=@ger , PaisID=@Pid WHERE FabricaId=@id AND RowVersion=@r";
                    var comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nom", fabrica.NombreFabrica);
                    comando.Parameters.AddWithValue("@dir", fabrica.Direccion);
                    comando.Parameters.AddWithValue("@ger", fabrica.GerenteDeVentas);
                    comando.Parameters.AddWithValue("@Pid", fabrica.PaisId);
                    comando.Parameters.AddWithValue("@id", fabrica.FabricaId);
                    comando.Parameters.AddWithValue("@r", fabrica.RowVersion);
                    registrosAfectados = comando.ExecuteNonQuery();
                    if (registrosAfectados > 0)
                    {
                        cadenaComando = "SELECT RowVersion FROM Fabricas WHERE FabricaId=@id";
                        comando = new SqlCommand(cadenaComando, cn);
                        comando.Parameters.AddWithValue("@id", fabrica.FabricaId);
                        fabrica.RowVersion = (byte[])comando.ExecuteScalar();
                    }
                }
                return registrosAfectados;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("IX_NombreFabrica"))
                {
                    throw new Exception("Fabrica ya existente");
                }
                throw new Exception(e.Message);
            }
        }

        public int Borrar(Fabrica fabrica)
        {
            int registrosAfectados = 0;
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "DELETE FROM Fabricas WHERE FabricaId=@id AND RowVersion=@r";
                    var comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@id", fabrica.FabricaId);
                    comando.Parameters.AddWithValue("@r", fabrica.RowVersion);
                    registrosAfectados = comando.ExecuteNonQuery();
                }
                return registrosAfectados;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("REFERENCE"))
                {
                    throw new Exception("No se puede borrar este registro");
                }
                throw new Exception(e.Message);
            }
        }

        public int Agregar(Fabrica fabrica)
        {
            int registrosAfectados = 0;
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "INSERT into Fabricas (NombreFabrica , Direccion , GerenteDeVentas , PaisId) VALUES (@nom , @Dir , @ger , @id)";
                    var comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nom", fabrica.NombreFabrica);
                    comando.Parameters.AddWithValue("@Dir", fabrica.Direccion);
                    comando.Parameters.AddWithValue("@ger", fabrica.GerenteDeVentas);
                    comando.Parameters.AddWithValue("@id", fabrica.PaisId);
                    registrosAfectados = comando.ExecuteNonQuery();
                    if (registrosAfectados > 0)
                    {
                        cadenaComando = "SELECT @@IDENTITY";
                        comando = new SqlCommand(cadenaComando, cn);
                        fabrica.FabricaId = (int)(decimal)comando.ExecuteScalar();
                        cadenaComando = "SELECT RowVersion FROM Fabricas WHERE FabricaId=@id";
                        comando = new SqlCommand(cadenaComando, cn);
                        comando.Parameters.AddWithValue("@id", fabrica.FabricaId);
                        fabrica.RowVersion = (byte[])comando.ExecuteScalar();
                    }
                }
                return registrosAfectados;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("IX_NombreFabrica"))
                {
                    throw new Exception("Fabrica Repetida");
                }
                throw new Exception(e.Message);
            }
        }

        private void SetPais(List<Fabrica> lista, SqlConnection cn)
        {
            foreach (var fabrica in lista)
            {
                fabrica.Pais = SetPais(fabrica.PaisId, cn);
            }

        }

        private Pais SetPais(int fabricaPaisId, SqlConnection cn)
        {
            Pais pais = null;
            var cadenaComando = "SELECT PaisId, NombrePais, RowVersion FROM Paises WHERE PaisId=@id";
            var comando = new SqlCommand(cadenaComando, cn);
            comando.Parameters.AddWithValue("@id", fabricaPaisId);
            using (var reader = comando.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    pais = ConstruirPais(reader);
                }   
            }
            return pais;
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

        private Fabrica ConstruirFabrica(SqlDataReader reader)
        {
            return new Fabrica()
            {
                FabricaId = reader.GetInt32(0),
                NombreFabrica = reader.GetString(1),
                Direccion = reader.GetString(2),
                GerenteDeVentas = reader.GetString(3),
                PaisId = reader.GetInt32(4),
                RowVersion = (byte[])reader[5]
            };
        }
    }
}

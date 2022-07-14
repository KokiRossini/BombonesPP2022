using BombonesPP2022.Entidades;
using BombonesPP2022.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombonesPP2022.Datos.Repositorios
{
    public class BombonesRepositorio
    {
        private readonly ConexionBD conexionBd;


        public BombonesRepositorio()
        {
            conexionBd = new ConexionBD();
        }

        public List<Bombon> GetLista()
        {
            var lista = new List<Bombon>();
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "SELECT BombonId, NombreBombon, TipoChocolateId, TipoNuezId, TipoRellenoId, PrecioVenta, Stock, FabricaId, RowVersion FROM Bombones";
                    var comando = new SqlCommand(cadenaComando, cn);
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var bombon = ConstruirBombon(reader);
                            lista.Add(bombon);
                        }
                    }
                    SetTipoChocolate(lista, cn);
                    SetTipoNuez(lista, cn);
                    SetTipoRelleno(lista, cn);
                    SetFabrica(lista, cn);
                }
                return lista;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int Borrar(Bombon bombon)
        {
            int registrosAfectados = 0;
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "DELETE FROM Bombones WHERE BombonId=@id AND RowVersion=@r";
                    var comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@id", bombon.BombonId);
                    comando.Parameters.AddWithValue("@r", bombon.RowVersion);
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

        private void SetFabrica(List<Bombon> lista, SqlConnection cn)
        {
            foreach (var bombon in lista)
            {
                bombon.Fabrica = SetFabrica(bombon.FabricaId, cn);
            }
        }

        private Fabrica SetFabrica(int id, SqlConnection cn)
        {
            Fabrica fabrica = null;
            var cadenaComando = "SELECT FabricaId, NombreFabrica, Direccion, GerenteDeVentas, PaisId, RowVersion FROM Fabricas WHERE FabricaId=@id";
            var comando = new SqlCommand(cadenaComando, cn);
            comando.Parameters.AddWithValue("@id", id);
            using (var reader = comando.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    fabrica = ConstruirFabrica(reader);
                }
            }
            return fabrica;
        }

        private Fabrica ConstruirFabrica(SqlDataReader reader)
        {
            return new Fabrica()
            {
                FabricaId = reader.GetInt32(0),
                NombreFabrica = reader.GetString(1),
                Direccion=reader.GetString(2),
                GerenteDeVentas=reader.GetString(3),
                PaisId=reader.GetInt32(4),
                RowVersion = (byte[])reader[5]
            };
        }

        private void SetTipoRelleno(List<Bombon> lista, SqlConnection cn)
        {
            foreach (var bombon in lista)
            {
                bombon.TipoRelleno = SetTipoRelleno(bombon.TipoRellenoId, cn);
            }

        }

        private TipoDeRelleno SetTipoRelleno(int id, SqlConnection cn)
        {
            TipoDeRelleno tipoRelleno = null;
            var cadenaComando = "SELECT TipoRellenoId, Relleno, RowVersion FROM TiposDeRelleno WHERE TipoRellenoId=@id";
            var comando = new SqlCommand(cadenaComando, cn);
            comando.Parameters.AddWithValue("@id", id);
            using (var reader = comando.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    tipoRelleno = ConstruirTipoRelleno(reader);
                }
            }
            return tipoRelleno;
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

        private void SetTipoNuez(List<Bombon> lista, SqlConnection cn)
        {
            foreach (var bombon in lista)
            {
                bombon.TipoNuez = SetTipoNuez(bombon.TipoNuezId, cn);
            }
        }

        private TipoNuez SetTipoNuez(int id, SqlConnection cn)
        {
            TipoNuez tipoNuez = null;
            var cadenaComando = "SELECT TipoNuezId, Nuez, RowVersion FROM TipoDeNuez WHERE TipoNuezId=@id";
            var comando = new SqlCommand(cadenaComando, cn);
            comando.Parameters.AddWithValue("@id", id);
            using (var reader = comando.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    tipoNuez = ConstruirTipoNuez(reader);
                }
            }
            return tipoNuez;
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

        private void SetTipoChocolate(List<Bombon> lista, SqlConnection cn)
        {
            foreach (var bombon in lista)
            {
                bombon.TipoChocolate = SetTipoChocolate(bombon.TipoChocolateId, cn);
            }
        }

        private TipoChocolate SetTipoChocolate(int id, SqlConnection cn)
        {
            TipoChocolate tipoChocolate = null;
            var cadenaComando = "SELECT TipoChocolateId, Chocolate, RowVersion FROM TiposDeChocolate WHERE TipoChocolateId=@id";
            var comando = new SqlCommand(cadenaComando, cn);
            comando.Parameters.AddWithValue("@id", id);
            using (var reader = comando.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    tipoChocolate = ConstruirTipoChocolate(reader);
                }
            }
            return tipoChocolate;
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

        private Bombon ConstruirBombon(SqlDataReader reader)
        {
            return new Bombon()
            {
                BombonId = reader.GetInt32(0),
                NombreBombon = reader.GetString(1),
                TipoChocolateId = reader.GetInt32(2),
                TipoNuezId = reader.GetInt32(3),
                TipoRellenoId = reader.GetInt32(4),
                PrecioVenta = (double)reader.GetDecimal(5),
                Stock=reader.GetInt16(6),
                FabricaId=reader.GetInt32(7),
                RowVersion = (byte[])reader[8]
            };
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

        public int Agregar(Bombon bombon)
        {
            int registrosAfectados = 0;
            try
            {
                using (var cn = conexionBd.AbrirConexion())
                {
                    var cadenaComando = "INSERT into Bombones (NombreBombon , TipoChocolateId , TipoNuezId , TipoRellenoId, PrecioVenta, Stock, FabricaId) VALUES (@nom , @TCid , @TNid , @TRid, @pv, @s, @Fid)";
                    var comando = new SqlCommand(cadenaComando, cn);
                    comando.Parameters.AddWithValue("@nom", bombon.NombreBombon);
                    comando.Parameters.AddWithValue("@TCid", bombon.TipoChocolateId);
                    comando.Parameters.AddWithValue("@TNid", bombon.TipoNuezId);
                    comando.Parameters.AddWithValue("@TRid", bombon.TipoRellenoId);
                    comando.Parameters.AddWithValue("@pv", bombon.PrecioVenta);
                    comando.Parameters.AddWithValue("@s", bombon.Stock);
                    comando.Parameters.AddWithValue("@Fid", bombon.FabricaId);
                    registrosAfectados = comando.ExecuteNonQuery();
                    if (registrosAfectados > 0)
                    {
                        cadenaComando = "SELECT @@IDENTITY";
                        comando = new SqlCommand(cadenaComando, cn);
                        bombon.BombonId = (int)(decimal)comando.ExecuteScalar();
                        cadenaComando = "SELECT RowVersion FROM Bombones WHERE BombonId=@id";
                        comando = new SqlCommand(cadenaComando, cn);
                        comando.Parameters.AddWithValue("@id", bombon.BombonId);
                        bombon.RowVersion = (byte[])comando.ExecuteScalar();
                    }
                }
                return registrosAfectados;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("IX_NombreBombon"))
                {
                    throw new Exception("Bombon Repetido");
                }
                throw new Exception(e.Message);
            }
        }



    }

}


using BombonesPP2022.Datos.Repositorios;
using BombonesPP2022.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombonesPP2022.Servicios
{
    public class PaisesServicios
    {
        private readonly PaisesRepositorio repositorio;
        public PaisesServicios()
        {
            repositorio = new PaisesRepositorio();
        }

        public List<Pais> GetPais()
        {
            try
            {
                return repositorio.GetLista();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

    }
}

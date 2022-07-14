using BombonesPP2022.Datos.Repositorios;
using BombonesPP2022.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombonesPP2022.Servicios.Servicios
{
    public class TiposRellenoServicios
    {
        private readonly TiposRellenosRepositorio repositorio;
        public TiposRellenoServicios()
        {
            repositorio = new TiposRellenosRepositorio();
        }

        public List<TipoDeRelleno> GetLista()
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

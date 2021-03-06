using BombonesPP2022.Datos.Repositorios;
using BombonesPP2022.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombonesPP2022.Servicios.Servicios
{
    public class TiposNuezServicios
    {
        private readonly TiposNuezRepositorio repositorio;
        public TiposNuezServicios()
        {
            repositorio = new TiposNuezRepositorio();
        }

        public List<TipoNuez> GetLista()
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

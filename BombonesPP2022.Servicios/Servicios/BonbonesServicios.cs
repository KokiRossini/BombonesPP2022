using BombonesPP2022.Datos.Repositorios;
using BombonesPP2022.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombonesPP2022.Servicios.Servicios
{
    public class BonbonesServicios
    {
        private readonly BombonesRepositorio repositorio;
        public BonbonesServicios()
        {
            repositorio = new BombonesRepositorio();
        }

        public List<Bombon> GetLista()
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


        public int Agregar(Bombon bombon)
        {
            try
            {
                return repositorio.Agregar(bombon);
            }
            catch (Exception)
            {

                throw;
            }

        }


        public int Borrar(Bombon bombon)
        {
            try
            {
                return repositorio.Borrar(bombon);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }


    }
}

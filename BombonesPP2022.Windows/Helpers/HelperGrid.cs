using BombonesPP2022.Entidades;
using BombonesPP2022.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombonesPP2022.Windows.Helpers
{
    public class HelperGrid
    {
        public static void LimpiarGrilla(DataGridView dataGrid)
        {
            dataGrid.Rows.Clear();

        }

        public static DataGridViewRow ConstruirFila(DataGridView dataGrid)
        {
            var r = new DataGridViewRow();
            r.CreateCells(dataGrid);
            return r;
        }
        public static void AgregarFila(DataGridView dataGrid, DataGridViewRow r)
        {
            dataGrid.Rows.Add(r);
        }
        public static void SetearFila(DataGridViewRow r, Object obj)
        {
            if (obj is Pais)
            {
                r.Cells[0].Value = ((Pais)obj).NombrePais;
            }
            else if (obj is Fabrica)
            {
                r.Cells[0].Value = ((Fabrica)obj).NombreFabrica;
                r.Cells[1].Value = ((Fabrica)obj).Direccion;
                r.Cells[2].Value = ((Fabrica)obj).GerenteDeVentas;
                r.Cells[3].Value = ((Fabrica)obj).Pais.NombrePais;
            }
            else if (obj is TipoChocolate)
            {
                r.Cells[0].Value = ((TipoChocolate)obj).Chocolate;
            }
            else if (obj is Bombon)
            {
                r.Cells[0].Value = ((Bombon)obj).NombreBombon;
                r.Cells[1].Value = ((Bombon)obj).TipoRelleno.Relleno;
                r.Cells[2].Value = ((Bombon)obj).TipoChocolate.Chocolate;
                r.Cells[3].Value = ((Bombon)obj).TipoNuez.TipoNuezId;
                r.Cells[4].Value = ((Bombon)obj).PrecioVenta;
                r.Cells[5].Value = ((Bombon)obj).Fabrica.NombreFabrica;
            }

            r.Tag = obj;

        }

    }
}

using BombonesPP2022.Entidades.Entidades;
using BombonesPP2022.Servicios.Servicios;
using BombonesPP2022.Windows.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombonesPP2022.Windows
{
    public partial class frmTipoChocolate : Form
    {
        public frmTipoChocolate()
        {
            InitializeComponent();
        }
        private TiposChocolatesServicios servicios;
        private List<TipoChocolate> lista;

        private void RecargarGrilla()
        {
            try
            {
                lista = servicios.GetLista();
                MostrarDatosEnGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void MostrarDatosEnGrilla()
        {
            HelperGrid.LimpiarGrilla(DatosDataGridView);
            foreach (var tipoChocolate in lista)
            {
                var r = HelperGrid.ConstruirFila(DatosDataGridView);
                HelperGrid.SetearFila(r, tipoChocolate);
                HelperGrid.AgregarFila(DatosDataGridView, r);
            }
        }

        private void frmTipoChocolate_Load(object sender, EventArgs e)
        {
            servicios = new TiposChocolatesServicios();
            RecargarGrilla();
        }
    }
}

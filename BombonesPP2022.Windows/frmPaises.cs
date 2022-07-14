using BombonesPP2022.Entidades;
using BombonesPP2022.Servicios;
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
    public partial class frmPaises : Form
    {
        public frmPaises()
        {
            InitializeComponent();
        }

        private PaisesServicios servicios;
        private List<Pais> lista;

        private void RecargarGrilla()
        {
            try
            {
                lista = servicios.GetPais();
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
            foreach (var pais in lista)
            {
                var r = HelperGrid.ConstruirFila(DatosDataGridView);
                HelperGrid.SetearFila(r, pais);
                HelperGrid.AgregarFila(DatosDataGridView, r);
            }
        }

        private void frmPaises_Load(object sender, EventArgs e)
        {
            servicios = new PaisesServicios();
            RecargarGrilla();
        }
    }
}

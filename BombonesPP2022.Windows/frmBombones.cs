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
    public partial class frmBombones : Form
    {
        public frmBombones()
        {
            InitializeComponent();
        }
        private BonbonesServicios servicios;
        private List<Bombon> lista;


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
            foreach (var bombon in lista)
            {
                var r = HelperGrid.ConstruirFila(DatosDataGridView);
                HelperGrid.SetearFila(r, bombon);
                HelperGrid.AgregarFila(DatosDataGridView, r);
            }
        }

        private void frmBombones_Load(object sender, EventArgs e)
        {
            servicios = new BonbonesServicios();
            RecargarGrilla();

        }

        private void NuevoIconButton_Click(object sender, EventArgs e)
        {
            frmBombonAE frm = new frmBombonAE() { Text = "Agregar Bombon" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                Bombon bombon = frm.GetBombon();
                int registrosAfectados = servicios.Agregar(bombon);
                if (registrosAfectados == 0)
                {
                    MessageBox.Show("No se agregaron registros...",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RecargarGrilla();

                }
                else
                {
                    var r = HelperGrid.ConstruirFila(DatosDataGridView);
                    HelperGrid.SetearFila(r, bombon);
                    HelperGrid.AgregarFila(DatosDataGridView, r);
                    MessageBox.Show("Registro Agregado",
                         "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

            }

        }

        private void BorrarIconButton_Click(object sender, EventArgs e)
        {
            if (DatosDataGridView.SelectedRows.Count == 0)
            {
                return;
            }
            try
            {
                var r = DatosDataGridView.SelectedRows[0];
                Bombon bombon = (Bombon)r.Tag;
                DialogResult dr = MessageBox.Show("Desea Borrar el registro seleccionado?", "confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No)
                {
                    return;
                }
                int registrosAfectados = servicios.Borrar(bombon);
                if (registrosAfectados == 0)
                {
                    MessageBox.Show("No se borraron registros...",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RecargarGrilla();
                }
                else
                {
                    DatosDataGridView.Rows.Remove(r);
                    MessageBox.Show("Registro eliminado", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error"
                                , MessageBoxButtons.OK, MessageBoxIcon.Error);
                RecargarGrilla();

            }

        }
    }
}

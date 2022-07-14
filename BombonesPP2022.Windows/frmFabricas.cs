using BombonesPP2022.Entidades;
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
    public partial class frmFabricas : Form
    {
        public frmFabricas()
        {
            InitializeComponent();
        }
        private FabricasServicios servicios;
        private List<Fabrica> lista;


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
            foreach (var fabrica in lista)
            {
                var r = HelperGrid.ConstruirFila(DatosDataGridView);
                HelperGrid.SetearFila(r, fabrica);
                HelperGrid.AgregarFila(DatosDataGridView, r);
            }
        }

        private void frmFabricas_Load(object sender, EventArgs e)
        {
            servicios = new FabricasServicios();
            RecargarGrilla();

        }

        private void NuevoIconButton_Click(object sender, EventArgs e)
        {
            frmFabricasAE frm = new frmFabricasAE() { Text = "Agregar Fabrica" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                Fabrica fabrica = frm.GetFabrica();
                int registrosAfectados = servicios.Agregar(fabrica);
                if (registrosAfectados == 0)
                {
                    MessageBox.Show("No se agregaron registros...",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RecargarGrilla();

                }
                else
                {
                    var r = HelperGrid.ConstruirFila(DatosDataGridView);
                    HelperGrid.SetearFila(r, fabrica);
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
                Fabrica fabrica = (Fabrica)r.Tag;
                DialogResult dr = MessageBox.Show("Desea Borrar el registro seleccionado?", "confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No)
                {
                    return;
                }
                int registrosAfectados = servicios.Borrar(fabrica);
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

        private void EditarIconButton_Click(object sender, EventArgs e)
        {
            if (DatosDataGridView.SelectedRows.Count == 0)
            {
                return;
            }
            var r = DatosDataGridView.SelectedRows[0];
            Fabrica fabrica = (Fabrica)r.Tag;
            Fabrica fabricaAux = (Fabrica)fabrica.Clone();
            try
            {
                frmFabricasAE frm = new frmFabricasAE() { Text = "Modificar Fabrica" };
                frm.SetFabrica(fabrica);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                fabrica = frm.GetFabrica();
                int registrosAfectados = servicios.Editar(fabrica);
                if (registrosAfectados == 0)
                {
                    MessageBox.Show("No se editarion registros...",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RecargarGrilla();

                }
                else
                {
                    HelperGrid.SetearFila(r, fabrica);
                    MessageBox.Show("Registro editado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception exception)
            {
                HelperGrid.SetearFila(r, fabricaAux);
                MessageBox.Show(exception.Message, "Error"
                                , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}

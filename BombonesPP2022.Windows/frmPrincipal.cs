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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void CerrarButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PaisesButton_Click(object sender, EventArgs e)
        {
            frmPaises frm = new frmPaises() { Text = "Formulario Paises" };
            frm.Show();
        }

        private void FabricasButton_Click(object sender, EventArgs e)
        {
            frmFabricas frm = new frmFabricas() { Text = "Formulario Fabrica" };
            frm.Show();
        }

        private void ChocolatesButton_Click(object sender, EventArgs e)
        {
            frmTipoChocolate frm = new frmTipoChocolate() { Text = "Tipos de chocolate" };
            frm.Show();
        }

        private void BombonesButton_Click(object sender, EventArgs e)
        {
            frmBombones frm = new frmBombones() { Text = "Bombones" };
            frm.Show();
        }
    }
}

using BombonesPP2022.Entidades;
using BombonesPP2022.Servicios;
using BombonesPP2022.Servicios.Servicios;
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
    public partial class frmFabricasAE : Form
    {
        public frmFabricasAE()
        {
            InitializeComponent();
        }
        private Fabrica fabrica;
        private PaisesServicios servicio;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            servicio = new PaisesServicios();
            CargarDatosComboPaises(ref PaisesComboBox);
            //if (cliente != null)
            //{
            //    CiudadTextBox.Text = cliente.NombreCiudad;
            //    //Falta cargar comboBox
            //}
        }

        private void CargarDatosComboPaises(ref ComboBox combo)
        {
            var lista = servicio.GetPais();
            var defaultPais = new Pais()
            {
                PaisId = 0,
                NombrePais = "Seleccione País"
            };
            lista.Insert(0, defaultPais);
            combo.DataSource = lista;
            combo.DisplayMember = "NombrePais";
            combo.ValueMember = "PaisId";
            combo.SelectedIndex = 0;
        }

        internal Fabrica GetFabrica()
        {
            return fabrica;
        }

        private void OKIconButton_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (fabrica == null)
                {
                    fabrica = new Fabrica();
                }
                fabrica.NombreFabrica = FabricaTextBox.Text;
                fabrica.Direccion = DireccionTextBox.Text;
                fabrica.GerenteDeVentas = GerenteTextBox.Text;
                fabrica.PaisId = ((Pais)PaisesComboBox.SelectedItem).PaisId;
                fabrica.Pais = ((Pais)PaisesComboBox.SelectedItem);
                DialogResult = DialogResult.OK;
            }

        }

        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (PaisesComboBox.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(PaisesComboBox, "debe seleccionar un pais");
            }

            if (string.IsNullOrEmpty(FabricaTextBox.Text))
            {
                valido = false;
                errorProvider1.SetError(FabricaTextBox, "Nombre no valido");
            }
            return valido;
        }

        internal void SetFabrica(Fabrica fabrica)
        {
            this.fabrica = fabrica;
        }
    }
}

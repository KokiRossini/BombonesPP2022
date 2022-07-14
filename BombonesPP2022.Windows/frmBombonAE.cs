using BombonesPP2022.Entidades;
using BombonesPP2022.Entidades.Entidades;
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
    public partial class frmBombonAE : Form
    {
        public frmBombonAE()
        {
            InitializeComponent();
        }
        private Bombon bombon;
        private TiposChocolatesServicios servicioTC;
        private TiposNuezServicios serviciosTN;
        private TiposRellenoServicios serviciosTR;
        private FabricasServicios serviciosF;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            servicioTC = new TiposChocolatesServicios();
            serviciosTN = new TiposNuezServicios();
            serviciosTR = new TiposRellenoServicios();
            serviciosF = new FabricasServicios();

            CargarDatosComboTC(ref ChocolateComboBox);
            CargarDatosComboTN(ref NuezComboBox);
            CargarDatosComboTR(ref RellenoComboBox);
            CargarDatosComboFabricas(ref FabricaComboBox);
        }

        private void CargarDatosComboFabricas(ref ComboBox combo)
        {
            var lista = serviciosF.GetLista();
            var defaultF = new Fabrica()
            {
                FabricaId = 0,
                NombreFabrica = "Seleccione Fabrica"
            };
            lista.Insert(0, defaultF);
            combo.DataSource = lista;
            combo.DisplayMember = "NombreFabrica";
            combo.ValueMember = "FabricaId";
            combo.SelectedIndex = 0;
        }

        private void CargarDatosComboTR(ref ComboBox combo)
        {
            var lista = serviciosTR.GetLista();
            var defaultTR = new TipoDeRelleno()
            {
                TipoRellenoId = 0,
                Relleno = "Seleccione Tipo Relleno"
            };
            lista.Insert(0, defaultTR);
            combo.DataSource = lista;
            combo.DisplayMember = "Relleno";
            combo.ValueMember = "TipoRellenoId";
            combo.SelectedIndex = 0;
        }

        private void CargarDatosComboTN(ref ComboBox combo)
        {
            var lista = serviciosTN.GetLista();
            var defaultTN = new TipoNuez()
            {
                TipoNuezId = 0,
                Nuez = "Seleccione Tipo Nuez"
            };
            lista.Insert(0, defaultTN);
            combo.DataSource = lista;
            combo.DisplayMember = "Nuez";
            combo.ValueMember = "TipoNuezId";
            combo.SelectedIndex = 0;
        }

        private void CargarDatosComboTC(ref ComboBox combo)
        {
            var lista = servicioTC.GetLista();
            var defaultTC = new TipoChocolate()
            {
                TipoChocolateId = 0,
                Chocolate = "Seleccione Tipo Chocolate"
            };
            lista.Insert(0, defaultTC);
            combo.DataSource = lista;
            combo.DisplayMember = "Chocolate";
            combo.ValueMember = "TipoChocolateId";
            combo.SelectedIndex = 0;
        }

        internal Bombon GetBombon()
        {
            return bombon ;
        }

        private void OKIconButton_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (bombon == null)
                {
                    bombon = new Bombon();
                }
                bombon.NombreBombon = BombonTextBox.Text;
                bombon.TipoChocolateId = ((TipoChocolate)ChocolateComboBox.SelectedItem).TipoChocolateId;
                bombon.TipoChocolate = ((TipoChocolate)ChocolateComboBox.SelectedItem);
                bombon.TipoNuezId = ((TipoNuez)NuezComboBox.SelectedItem).TipoNuezId;
                bombon.TipoNuez = ((TipoNuez)NuezComboBox.SelectedItem);
                bombon.TipoRellenoId = ((TipoDeRelleno)RellenoComboBox.SelectedItem).TipoRellenoId;
                bombon.TipoRelleno = ((TipoDeRelleno)RellenoComboBox.SelectedItem);
                bombon.PrecioVenta = double.Parse(PrecioTextBox.Text);
                bombon.Stock = int.Parse(StockTextBox.Text);
                bombon.FabricaId = ((Fabrica)FabricaComboBox.SelectedItem).FabricaId;
                bombon.Fabrica = ((Fabrica)FabricaComboBox.SelectedItem);

                DialogResult = DialogResult.OK;
            }

        }

        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (ChocolateComboBox.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(ChocolateComboBox, "debe seleccionar un Tipo");
            }

            if (NuezComboBox.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(NuezComboBox, "debe seleccionar un Tipo");
            }

            if (RellenoComboBox.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(RellenoComboBox, "debe seleccionar un Tipo");
            }

            if (FabricaComboBox.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(FabricaComboBox, "debe seleccionar un Tipo");
            }

            if (string.IsNullOrEmpty(BombonTextBox.Text))
            {
                valido = false;
                errorProvider1.SetError(BombonTextBox, "Nombre no valido");
            }
            if (string.IsNullOrEmpty(StockTextBox.Text))
            {
                valido = false;
                errorProvider1.SetError(StockTextBox, "Valor no valido");
            }
            if (string.IsNullOrEmpty(PrecioTextBox.Text))
            {
                valido = false;
                errorProvider1.SetError(PrecioTextBox, "Valor no valido");
            }
            return valido;
        }

        private void CancelarIconButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

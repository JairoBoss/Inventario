using SistemaDeInventarios.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeInventarios
{
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dSInventario.Usuarios' table. You can move, or remove it, as needed.
            this.usuariosTableAdapter.Fill(this.dSInventario.Usuarios);


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txtUsuario.Text != "" && txtContraseña.Text != "")
            {
                string user = txtUsuario.Text;
                string contraseña= Encriptar.GetMD5(txtContraseña.Text);
                MessageBox.Show(contraseña);
                MessageBox.Show(txtContraseña.Text);

            }
            else
            {
                Error error = new Error();
                error.ShowDialog();
            }
        }

        private void btnReporteIndividual_Click(object sender, EventArgs e)
        {

        }

        private void btnReporteGeneral_Click(object sender, EventArgs e)
        {

        }
    }
}

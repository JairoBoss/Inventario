using SistemaDeInventarios.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeInventarios
{
    public partial class Login : Form
    {
        private bool banderita = false;
        public Login()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                // Enter is pressed
                ingresar();
                userName = this.txtUsuario.Text;
                return true; //return true if you want to suppress the key.
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private String userName;

        public String getUserName()
        {
            return this.userName;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ingresar();
            userName = this.txtUsuario.Text;
            
        }

        private void ingresar()
        {
            string cadena = Properties.Settings.Default.BD_InvetarioConnectionString;
            SqlConnection conexion = new SqlConnection(cadena);

            try
            {
                conexion.Open();
                string usuario = txtUsuario.Text;
                string contraseña = Encriptar.GetMD5(txtPassword.Text);



                SqlCommand comando = new SqlCommand("Select id, rol from Usuarios " +
                    "where Usuario = @usuario and Password = @password", conexion);
                comando.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;
                comando.Parameters.Add("@password", SqlDbType.VarChar).Value = contraseña;
                SqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    banderita = true;
                    this.Close();
                }
                else
                {
                    Error error = new Error();
                    error.ShowDialog();
                }

                conexion.Close();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);

            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(banderita == false)
            {
                Application.Exit();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}

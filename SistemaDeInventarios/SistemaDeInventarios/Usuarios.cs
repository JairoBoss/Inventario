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
    public partial class Usuarios : Form
    {
        private string usuario = "";
        private string cadenita = Properties.Settings.Default.BD_InvetarioConnectionString;
        public Usuarios(string user)
        {
            this.usuario = user;
            InitializeComponent();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dSInventario.Usuarios' table. You can move, or remove it, as needed.
            this.usuariosTableAdapter.Fill(this.dSInventario.Usuarios);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 13);
            this.dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

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

                try
                {
                    this.usuariosTableAdapter.addNewUser(user, contraseña);
                        
                    this.usuariosTableAdapter.Fill(this.dSInventario.Usuarios); //Actualiza

                    SqlConnection conexion = new SqlConnection(cadenita);
                    conexion.Open();
                    SqlCommand nuevoComando = new SqlCommand("insert into UsuariosAcciones(Usuario, Accion, fecha) " +
                            "values (@User, @Accion, @fecha)", conexion);
                    nuevoComando.Parameters.Add("@User", SqlDbType.VarChar).Value = usuario;
                    nuevoComando.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "Agrego un nuevo usuario: " + user;
                    nuevoComando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                    nuevoComando.ExecuteNonQuery();
                    conexion.Close();
                    


                    cleanCategoria();
                }
                catch (ExecutionEngineException e1)
                {
                    //MessageBox.Show(e1.Message);
                }

            }
            else
            {
                Error error = new Error();
                error.ShowDialog();
            }
        }

        private void cleanCategoria()
        {
            this.txtContraseña.Clear();
            this.txtUsuario.Clear();
        }

        private void btnReporteIndividual_Click(object sender, EventArgs e)
        {

        }

        private void btnReporteGeneral_Click(object sender, EventArgs e)
        {

        }

        private void btnAztualizar_Click(object sender, EventArgs e)
        {

        }

        private int idSeleccionado;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView temp = (DataGridView)sender;
            if (temp == null)
                return;

            try
            {
                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    
                    idSeleccionado = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    MessageBox.Show("" + idSeleccionado);
                }
                else
                {

                }
            }
            catch (Exception e1)
            {
                //MessageBox.Show(e1.Message);
            }

        }
    }
}

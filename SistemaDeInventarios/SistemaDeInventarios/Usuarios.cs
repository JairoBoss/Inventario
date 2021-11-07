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
        private int idUsuario = -1;
        public Usuarios(string user)
        {
            this.usuario = user;
            InitializeComponent();
            SqlConnection conexion = new SqlConnection(cadenita);
            conexion.Open();
            SqlCommand obtenerIdUsuario = new SqlCommand("select id from Usuarios where Usuario = @nombre", conexion);
            obtenerIdUsuario.Parameters.Add("@nombre", SqlDbType.VarChar).Value = usuario;
            SqlDataReader lector = obtenerIdUsuario.ExecuteReader();
            while (lector.Read())
            {
                idUsuario = Convert.ToInt32(lector["id"]);
            }
            conexion.Close();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dSInventario.Usuarios' table. You can move, or remove it, as needed.
            this.usuariosTableAdapter.Fill(this.dSInventario.Usuarios);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 13);
            this.dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

            limpiarControles();
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
                    SqlCommand nuevoComando = new SqlCommand("insert into UsuariosAcciones(IdUsuario, Accion, fecha) " +
                            "values (@User, @Accion, @fecha)", conexion);
                    nuevoComando.Parameters.Add("@User", SqlDbType.Int).Value = idUsuario;
                    nuevoComando.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "Agrego un nuevo usuario: " + user;
                    nuevoComando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                    nuevoComando.ExecuteNonQuery();
                    conexion.Close();
                    


                    limpiarControles();
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

        
        private void btnReporteIndividual_Click(object sender, EventArgs e)
        {
            
        }

        private void btnReporteGeneral_Click(object sender, EventArgs e)
        {
            ExportarReporteGenreal a = new ExportarReporteGenreal();
            a.ShowDialog();
        }

        private void btnAztualizar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text != "" && txtContraseña.Text != "")
            {
                string nombre = txtUsuario.Text;
                string contraseña = Encriptar.GetMD5(txtContraseña.Text);

                this.usuariosTableAdapter.actualizarUsuario(idSeleccionado, nombre, contraseña);
                this.usuariosTableAdapter.Fill(this.dSInventario.Usuarios); //Actualiza
                limpiarControles();

                /*
                 SqlConnection conexion = new SqlConnection(cadenita);
                conexion.Open();                
                SqlCommand obtenerMarca = new SqlCommand("select Usuario from Usuarios where id = @id", conexion);
                obtenerMarca.Parameters.Add("@id", SqlDbType.Int).Value = idSeleccionado;
                SqlDataReader lector = obtenerMarca.ExecuteReader();
                string nombreAnterior = "";
                while (lector.Read())
                {
                    nombreAnterior = lector["Usuario"].ToString();
                }
                conexion.Close();
                
                conexion.Open();
                SqlCommand nuevoComando = new SqlCommand("insert into UsuariosAcciones(Usuario, Accion, fecha) " +
                        "values (@User, @Accion, @fecha)", conexion);
                nuevoComando.Parameters.Add("@User", SqlDbType.VarChar).Value = usuario;
                nuevoComando.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "Actualizo el nombre de un usuario: " + nombreAnterior + " -> " + nombre;
                nuevoComando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                nuevoComando.ExecuteNonQuery();
                conexion.Close();
                 */
                limpiarControles();
            }
            else
            {
                Error error = new Error();
                error.ShowDialog();
            }
        }

        private int idSeleccionado;
        private int contador = 0;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView temp = (DataGridView)sender;
            if (temp == null)
                return;

            if(this.contador < 3)
            {
                this.contador++;
                return;
            }
            else
            {
                try
                {
                    if (dataGridView1.CurrentRow.Cells[0].Value != null)
                    {

                        limpiarControles();
                        idSeleccionado = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        this.txtContraseña.Text = (dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        this.txtUsuario.Text = (dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        
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

        private void limpiarControles()
        {
            this.txtContraseña.Clear();
            this.txtUsuario.Clear();
        }
    }
}

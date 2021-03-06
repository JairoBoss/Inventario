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
    public partial class Categorias : Form
    {
        private string nombreUser;
        private string cadenita = Properties.Settings.Default.BD_InvetarioConnectionString;
        private int idUsuario = -1;
        public Categorias(string nombre)
        {
            this.nombreUser = nombre;
            InitializeComponent();

            SqlConnection conexion = new SqlConnection(cadenita);
            conexion.Open();
            SqlCommand obtenerIdUsuario = new SqlCommand("select id from Usuarios where Usuario = @nombre", conexion);
            obtenerIdUsuario.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombreUser;
            SqlDataReader lector = obtenerIdUsuario.ExecuteReader();
            while (lector.Read())
            {
                idUsuario = Convert.ToInt32(lector["id"]);
            }
            conexion.Close();

        }

        private void Categorias_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dSInventario.Marca' table. You can move, or remove it, as needed.
            
            // TODO: This line of code loads data into the 'dSInventario.Marca' table. You can move, or remove it, as needed.

            // TODO: This line of code loads data into the 'dSInventario.Categoria' table. You can move, or remove it, as needed.
            this.categoriaTableAdapter.Fill(this.dSInventario.Categoria);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 13);
            this.dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

            this.dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 13);
            this.dataGridView2.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            cleanCategoria();
            btnActualizarMarca.Enabled = false;
            btnAgregarMarca.Enabled = false;
            btnEliminarMarca.Enabled = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e) //Agregar categoria
        {
            try
            {

                if (txtCategoria.Text == "" && txtDescripcion.Text == "")
                {
                    Error error = new Error();
                    error.ShowDialog();
                }
                else
                {

                    
                    
                    String categoria = this.txtCategoria.Text;
                    String descripcion = this.txtDescripcion.Text;
                    this.categoriaTableAdapter.AgregarCategoria(categoria, descripcion);
                    this.categoriaTableAdapter.Fill(this.dSInventario.Categoria); //Actualiza

                    SqlConnection conexion = new SqlConnection(cadenita);
                    conexion.Open();
                    SqlCommand nuevoComando = new SqlCommand("insert into UsuariosAcciones (IdUsuario, Accion, fecha) " +
                            "values (@User, @Accion, @fecha)", conexion);
                    nuevoComando.Parameters.Add("@User", SqlDbType.Int).Value = idUsuario;
                    nuevoComando.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "Agrego una nueva categoria: " + categoria;
                    nuevoComando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                    nuevoComando.ExecuteNonQuery();
                    conexion.Close();
                }


                cleanCategoria();
            }catch(ExecutionEngineException e1)
            {
                //MessageBox.Show(e1.Message);
            }
        }

        private void cleanCategoria()
        {
            txtCategoria.Text = "";
            txtDescripcion.Text = "";
        }

        private void cleanMarca()
        {
            this.txtMarca.Clear();
        }

        private int contador = 0;

        private int idSeleccionado;
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
                        cleanMarca();
                        idSeleccionado = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        txtCategoria.Text = (dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        txtDescripcion.Text = (dataGridView1.CurrentRow.Cells[2].Value.ToString());
                        this.marcaTableAdapter.Fill(this.dSInventario.Marca);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.contador = 10;            
        }

        private void pictureBox4_Click(object sender, EventArgs e) //Eliminar
        {

            SqlConnection conexion = new SqlConnection(cadenita);
            conexion.Open();
            SqlCommand obtenerMarca = new SqlCommand("select nombre from categoria where id = @id", conexion);
            obtenerMarca.Parameters.Add("@id", SqlDbType.Int).Value = idSeleccionado;
            SqlDataReader lector = obtenerMarca.ExecuteReader();
            string marca = "";
            while (lector.Read())
            {
                marca = lector["nombre"].ToString();
            }
            conexion.Close();
            conexion.Open();

            SqlCommand nuevoComando = new SqlCommand("insert into UsuariosAcciones(IdUsuario, Accion, fecha) " +
                    "values (@User, @Accion, @fecha)", conexion);
            nuevoComando.Parameters.Add("@User", SqlDbType.Int).Value = idUsuario;
            nuevoComando.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "Elimino una categoria: " + marca;
            nuevoComando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
            nuevoComando.ExecuteNonQuery();
            conexion.Close();

            this.categoriaTableAdapter.EliminarCategoria(idSeleccionado);
            this.categoriaTableAdapter.Fill(this.dSInventario.Categoria); //Actualiza
            cleanCategoria();
        }

        private void pictureBox3_Click(object sender, EventArgs e) //Actualizar
        {
            String nombreCategoria = txtCategoria.Text;
            String descripcionCategoria = txtDescripcion.Text;

            SqlConnection conexion = new SqlConnection(cadenita);
            conexion.Open();
            SqlCommand obtenerMarca = new SqlCommand("select nombre, descripcion from categoria where id = @id", conexion);
            obtenerMarca.Parameters.Add("@id", SqlDbType.Int).Value = idSeleccionado;
            SqlDataReader lector = obtenerMarca.ExecuteReader();
            string marca = "";
            string descripcion = "";
            while (lector.Read())
            {
                marca = lector["nombre"].ToString();
                descripcion = lector["descripcion"].ToString();
            }
            conexion.Close();
            conexion.Open();

            SqlCommand nuevoComando = new SqlCommand("insert into UsuariosAcciones(IdUsuario, Accion, fecha) " +
                    "values (@User, @Accion, @fecha)", conexion);
            nuevoComando.Parameters.Add("@User", SqlDbType.Int).Value = idUsuario;
            nuevoComando.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "Actualizo una categoria: " + marca +" -> "+ nombreCategoria +". Descripcion: " + descripcion +" ->" + descripcionCategoria;
            nuevoComando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
            nuevoComando.ExecuteNonQuery();
            conexion.Close();
            
            this.categoriaTableAdapter.ActualizarCategoria(idSeleccionado, nombreCategoria, descripcionCategoria);
            this.categoriaTableAdapter.Fill(this.dSInventario.Categoria); //Actualiza
            cleanCategoria();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizarMarca_Click(object sender, EventArgs e)
        {

            String nuevoNombre = txtMarca.Text;

            SqlConnection conexion = new SqlConnection(cadenita);
            conexion.Open();
            SqlCommand obtenerMarca = new SqlCommand("select nombre from categoria where id = @id", conexion);
            obtenerMarca.Parameters.Add("@id", SqlDbType.Int).Value = idSeleccionado;
            SqlDataReader lector = obtenerMarca.ExecuteReader();
            string categoria = "";
            while (lector.Read())
            {
                categoria = lector["nombre"].ToString();
            }
            conexion.Close();

            conexion.Open();
            SqlCommand obtenerMarcaG = new SqlCommand("select marca from marca where id = @id", conexion);
            obtenerMarcaG.Parameters.Add("@id", SqlDbType.Int).Value = idMarcaSleccionado;
            SqlDataReader lector2 = obtenerMarcaG.ExecuteReader();
            string marca = "";
            while (lector2.Read())
            {
                marca = lector2["marca"].ToString();
            }
            conexion.Close();

            conexion.Open();

            SqlCommand nuevoComando = new SqlCommand("insert into UsuariosAcciones(IdUsuario, Accion, fecha) " +
                    "values (@User, @Accion, @fecha)", conexion);
            nuevoComando.Parameters.Add("@User", SqlDbType.Int).Value = idUsuario;
            nuevoComando.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "Actualizo el nombre de una marca: " + marca + " -> "+ nuevoNombre +" de la categoria: " + categoria;
            nuevoComando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
            nuevoComando.ExecuteNonQuery();
            conexion.Close();

            this.marcaTableAdapter.ActualizarNombreMarca(idMarcaSleccionado, nuevoNombre);
            this.marcaTableAdapter.Fill(this.dSInventario.Marca); //Actualiza
            cleanMarca();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnActualizarMarca.Enabled = true;
            btnAgregarMarca.Enabled = true;
            btnEliminarMarca.Enabled = true;                        
        }

        private void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            //idSeleccionado            
            String marca = txtMarca.Text;         

            SqlConnection conexion = new SqlConnection(cadenita);
            conexion.Open();
            SqlCommand obtenerMarca = new SqlCommand("select nombre from categoria where id = @id", conexion);
            obtenerMarca.Parameters.Add("@id", SqlDbType.Int).Value = idSeleccionado;
            SqlDataReader lector = obtenerMarca.ExecuteReader();
            string categoria = "";
            while (lector.Read())
            {
                categoria = lector["nombre"].ToString();
            }
            conexion.Close();
            conexion.Open();

            SqlCommand nuevoComando = new SqlCommand("insert into UsuariosAcciones(IdUsuario, Accion, fecha) " +
                    "values (@User, @Accion, @fecha)", conexion);
            nuevoComando.Parameters.Add("@User", SqlDbType.Int).Value = idUsuario;
            nuevoComando.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "Agrego una nueva marca: " + marca +" a la categoria: " + categoria;
            nuevoComando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
            nuevoComando.ExecuteNonQuery();
            conexion.Close();

            this.marcaTableAdapter.AgregarMarca(idSeleccionado, marca);
            this.marcaTableAdapter.Fill(this.dSInventario.Marca); //Actualiza
            cleanMarca();
        }
        private int idMarcaSleccionado;
                
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    idMarcaSleccionado = Convert.ToInt16(dataGridView2.CurrentRow.Cells[0].Value.ToString());
                    txtMarca.Text = (dataGridView2.CurrentRow.Cells[1].Value.ToString());
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

        private void btnEliminarMarca_Click(object sender, EventArgs e)
        {

            SqlConnection conexion = new SqlConnection(cadenita);
            conexion.Open();
            SqlCommand obtenerMarca = new SqlCommand("select nombre from categoria where id = @id", conexion);
            obtenerMarca.Parameters.Add("@id", SqlDbType.Int).Value = idSeleccionado;
            SqlDataReader lector = obtenerMarca.ExecuteReader();
            string categoria = "";
            while (lector.Read())
            {
                categoria = lector["nombre"].ToString();
            }
            conexion.Close();

            conexion.Open();
            SqlCommand obtenerMarcaG = new SqlCommand("select marca from marca where id = @id", conexion);
            obtenerMarcaG.Parameters.Add("@id", SqlDbType.Int).Value = idSeleccionado;
            SqlDataReader lector2 = obtenerMarcaG.ExecuteReader();
            string marca = "";
            while (lector2.Read())
            {
                marca = lector2["marca"].ToString();
            }
            conexion.Close();

            conexion.Open();

            SqlCommand nuevoComando = new SqlCommand("insert into UsuariosAcciones(IdUsuario, Accion, fecha) " +
                    "values (@User, @Accion, @fecha)", conexion);
            nuevoComando.Parameters.Add("@User", SqlDbType.Int).Value = idUsuario;
            nuevoComando.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "Elimino una marca: " + marca + " de la categoria: " + categoria;
            nuevoComando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
            nuevoComando.ExecuteNonQuery();
            conexion.Close();


            this.marcaTableAdapter.EliminarMarca(idMarcaSleccionado);
            this.marcaTableAdapter.Fill(this.dSInventario.Marca); //Actualiza
            //checar si depende de un artiuclo
            cleanMarca();
        }



        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnActualizarMarca.Enabled = true;
            btnAgregarMarca.Enabled = true;
            btnEliminarMarca.Enabled = true;
        }
    }
}

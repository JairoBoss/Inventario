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
    public partial class Productos : Form
    {
        private String ruta;
        private string cadenita = Properties.Settings.Default.BD_InvetarioConnectionString;
        private string nombreUser;
        private int idUsuario = -1;
        public Productos(string nombre)
        {
            InitializeComponent();
            this.nombreUser = nombre;
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

        private void Productos_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dSInventario.Marca' table. You can move, or remove it, as needed.
            //this.marcaTableAdapter.Fill(this.dSInventario.Marca);
            // TODO: This line of code loads data into the 'dSInventario.Marca' table. You can move, or remove it, as needed.
            //this.marcaTableAdapter.Fill(this.dSInventario.Marca);
            
            // TODO: This line of code loads data into the 'dSInventario.Categoria' table. You can move, or remove it, as needed.
            this.categoriaTableAdapter.Fill(this.dSInventario.Categoria);
            this.cmbCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMarca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            llenarComboCategoria();
            this.cmbCategoria.SelectedIndex = -1;
            this.cmbMarca.SelectedIndex = -1;            

        }

        private void llenarComboCategoria()
        {
            SqlConnection conexion = new SqlConnection(cadenita);
            try
            {
                conexion.Open();
                SqlDataAdapter adaptador = new SqlDataAdapter("select id, nombre from Categoria", conexion);
                DataTable tablita = new DataTable("categorias");
                adaptador.Fill(tablita);

                cmbCategoria.DataSource = tablita;
                cmbCategoria.ValueMember = "id";
                cmbCategoria.DisplayMember = "nombre";
                conexion.Close();
            }
            catch (Exception e1)
            {

            }
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:/Users/jairo/Desktop/Productos";
            openFileDialog1.Filter = "Files|*.jpg;*.jpeg;*.png;";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {                
                imagenProducto.Image = (Image)(new Bitmap(Image.FromFile(openFileDialog1.FileName), new Size(390, 196)));
                ruta = openFileDialog1.FileName;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            clean();
        }

        private void clean()
        {
            txtNombreDelProducto.Clear();
            cmbCategoria.SelectedIndex = -1;
            cmbMarca.SelectedIndex = -1;
            txtDescripcion.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
            imagenProducto.Image = (Image)(new Bitmap(Image.FromFile("C:/Users/jairo/source/repos/SistemaDeInventarios/SistemaDeInventarios/Resources/addImage.png"), new Size(390, 196)));
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            if (txtNombreDelProducto.Text == "" || cmbCategoria.SelectedIndex == -1 || cmbMarca.SelectedIndex == -1 || txtCantidad.Text == "" || txtPrecio.Text == "" || txtDescripcion.Text == "")
            {
                Error error = new Error();
                error.ShowDialog();
            }
            else
            {
                SqlConnection conexion = new SqlConnection(cadenita);
                string nombreDelProducto = txtNombreDelProducto.Text;
                string categoria = cmbCategoria.SelectedValue.ToString();
                string marca = cmbMarca.SelectedValue.ToString();
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                Double precio = Convert.ToDouble(txtPrecio.Text);
                string descripcion = txtDescripcion.Text;
                DateTime diaDeRegistro = this.cmbDate.Value;
                if (ruta != null)
                {
                    
                    string rutaDeLaImagen = ruta;
                    Producto producto = new Producto(nombreDelProducto, categoria, marca, cantidad, precio, descripcion, ruta, diaDeRegistro);
                    try
                    {
                        conexion.Open();
                        SqlCommand comando = new SqlCommand("insert into productos(nombreDelProducto," +
                            " categoria, marca, cantidad, precio, descripcion, diaDeRegistro, " +
                            "rutaDeLaImagen) " +
                            "values (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8)", conexion);
                        comando.Parameters.Add("@p1", SqlDbType.VarChar).Value = nombreDelProducto;
                        comando.Parameters.Add("@p2", SqlDbType.VarChar).Value = categoria;
                        comando.Parameters.Add("@p3", SqlDbType.VarChar).Value = marca;
                        comando.Parameters.Add("@p4", SqlDbType.Int).Value = cantidad;
                        comando.Parameters.Add("@p5", SqlDbType.Float).Value = precio;
                        comando.Parameters.Add("@p6", SqlDbType.VarChar).Value = descripcion;
                        comando.Parameters.Add("@p7", SqlDbType.DateTime).Value = diaDeRegistro;
                        comando.Parameters.Add("@p8", SqlDbType.VarChar).Value = rutaDeLaImagen;
                        comando.ExecuteNonQuery();

                        SqlCommand nuevoComando = new SqlCommand("insert into UsuariosAcciones(IdUsuario, Accion, fecha) " +
                            "values (@User, @Accion, @fecha)", conexion);
                        nuevoComando.Parameters.Add("@User", SqlDbType.Int).Value = idUsuario;
                        nuevoComando.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "Agrego un nuevo producto: "+nombreDelProducto;
                        nuevoComando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now; 
                        nuevoComando.ExecuteNonQuery();

                        conexion.Close();
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.Message);
                    }

                }
                else
                {
                    Producto producto = new Producto(nombreDelProducto, categoria, marca, cantidad, precio, descripcion, diaDeRegistro);
                    try
                    {
                        conexion.Open();
                        SqlCommand comando = new SqlCommand("insert into productos(nombreDelProducto, categoria, marca, cantidad, precio, descripcion, diaDeRegistro) " +
                            "values (@p1, @p2, @p3, @p4, @p5, @p6, @p7)", conexion);
                        comando.Parameters.Add("@p1", SqlDbType.VarChar).Value = nombreDelProducto;
                        comando.Parameters.Add("@p2", SqlDbType.VarChar).Value = categoria;
                        comando.Parameters.Add("@p3", SqlDbType.VarChar).Value = marca;
                        comando.Parameters.Add("@p4", SqlDbType.Int).Value = cantidad;
                        comando.Parameters.Add("@p5", SqlDbType.Float).Value = precio;
                        comando.Parameters.Add("@p6", SqlDbType.VarChar).Value = descripcion;
                        comando.Parameters.Add("@p7", SqlDbType.DateTime).Value = diaDeRegistro;
                        comando.ExecuteNonQuery();

                        SqlCommand nuevoComando = new SqlCommand("insert into UsuariosAcciones(IdUsuario, Accion, fecha) " +
                            "values (@User, @Accion, @fecha)", conexion);
                        nuevoComando.Parameters.Add("@User", SqlDbType.Int).Value = idUsuario;
                        nuevoComando.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "Agrego un nuevo producto: " + nombreDelProducto;
                        nuevoComando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                        nuevoComando.ExecuteNonQuery();

                        conexion.Close();
                        
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.Message);
                    }
                }
                clean();
            }
    }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 46 || e.KeyChar == Convert.ToChar(Keys.Back)))
            {

            }
            else
            {
                e.Handled = true;
                return;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

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
        public Productos()
        {
            InitializeComponent();
        }

        private void Productos_Load(object sender, EventArgs e)
        {            
            this.cmbCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMarca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
                string categoria = cmbCategoria.Text;
                string marca = cmbMarca.Text;
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

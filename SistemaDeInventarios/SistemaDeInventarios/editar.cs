using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeInventarios
{
    public partial class editar : Form
    {
        private string cadenita = Properties.Settings.Default.BD_InvetarioConnectionString;
        private int idProducto;

        public editar(int id)
        {
            InitializeComponent();
            this.idProducto = id;
        }

        private void editar_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsInventario1.Marca' table. You can move, or remove it, as needed.
            this.marcaTableAdapter.Fill(this.dsInventario1.Marca);
            // TODO: This line of code loads data into the 'dsInventario1.Categoria' table. You can move, or remove it, as needed.
            this.categoriaTableAdapter.Fill(this.dsInventario1.Categoria);
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            this.comboBox1.SelectedIndex = -1;
            this.comboBox2.SelectedIndex = -1;
            getDatos();
        }


        private void getDatos()
        {
            SqlConnection conexion = new SqlConnection(cadenita);

            try
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand("select nombreDelProducto, categoria, marca, cantidad, precio, descripcion, rutadelaimagen from Productos where id = @p1 ", conexion);
                comando.Parameters.Add("@p1", SqlDbType.Int).Value = idProducto;
                SqlDataReader lector = comando.ExecuteReader();

                string nombreDelProducto = "";
                int categoria = -1;
                int marca = -1;
                string cantidadProducto = "";
                string precio = "";
                string descripcion = "";
                string ruta = "";
                while (lector.Read())
                {
                    nombreDelProducto = lector["nombreDelProducto"].ToString();
                    categoria = Convert.ToInt32(lector["categoria"].ToString());
                    marca = Convert.ToInt32(lector["marca"].ToString());
                    cantidadProducto = (lector["cantidad"].ToString());
                    precio = (lector["precio"].ToString());
                    descripcion = lector["descripcion"].ToString();
                    ruta = lector["rutadelaimagen"].ToString();
                }
                conexion.Close();

                this.txtNombreDelProducto.Text = nombreDelProducto;
                this.comboBox1.SelectedIndex = categoria;
                this.comboBox2.SelectedIndex= marca;
                this.txtCantidad.Text = cantidadProducto;
                this.txtPrecio.Text = precio;
                this.txtDescripcion.Text = descripcion;

                if (File.Exists(ruta))
                {
                    imagenProducto.Image = (Image)(new Bitmap(Image.FromFile(ruta), new Size(390, 196)));
                }

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }    
    }


}

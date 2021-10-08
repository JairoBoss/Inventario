using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeInventarios
{
    public partial class Detalle : Form
    {
        private string cadenita = Properties.Settings.Default.BD_InvetarioConnectionString;
        private int idProducto;
        public Detalle(int id)
        {
            InitializeComponent();
            this.idProducto = id;
        }

        private void Detalle_Load(object sender, EventArgs e)
        {
            SystemSounds.Asterisk.Play();
            this.MaximizeBox = false;
            getDatos();

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
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
                string categoria = "";
                string marca = "";
                string cantidadProducto = "";
                string precio = "";
                string descripcion = "";
                string ruta = "";
                while (lector.Read())
                {
                    nombreDelProducto = lector["nombreDelProducto"].ToString();
                    categoria = lector["categoria"].ToString();
                    marca = lector["marca"].ToString();
                    cantidadProducto = (lector["cantidad"].ToString());
                    precio = (lector["precio"].ToString());
                    descripcion = lector["descripcion"].ToString();
                    ruta = lector["rutadelaimagen"].ToString();                    
                }
                conexion.Close();

                this.txtNombreDelProducto.Text = nombreDelProducto;
                this.textBox2.Text = categoria;
                this.textBox1.Text = marca;
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

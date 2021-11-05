using SistemaDeInventarios.Clases;
using SpreadsheetLight;
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
    public partial class Main : Form
    {
        private string cadenita = Properties.Settings.Default.BD_InvetarioConnectionString;
        public Main()
        {
            InitializeComponent();
        }

        private void abrirFormEnPanel(Object Formhijo)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = Formhijo as Form;

            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            fh.Show();
        }

        private string nombre;
        private void Main_Load(object sender, EventArgs e)
        {

            abrirFormEnPanel(new home());
            Login login = new Login();
            

            login.ShowDialog();

            nombre = login.getUserName();

            
            this.label2.Text = "¡Hola " + nombre + "!";
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {
            
        }        

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            abrirFormEnPanel(new home());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Importar.InitialDirectory = "C:\\";
            Importar.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
            DateTime diaActual = DateTime.Now;
            SqlConnection conexion = new SqlConnection(cadenita);
            if (Importar.ShowDialog() == DialogResult.OK)
            {                                
                SLDocument sl = new SLDocument(Importar.FileName);
                int iRow = 2;
                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
                {
                    string nombreDelProducto = sl.GetCellValueAsString(iRow, 1);
                    string categoria = sl.GetCellValueAsString(iRow, 2);
                    string marca = sl.GetCellValueAsString(iRow, 3);
                    int cantidad = Convert.ToInt32(sl.GetCellValueAsString(iRow, 4));
                    double precio = Convert.ToDouble(sl.GetCellValueAsString(iRow, 5));
                    string descripcion = sl.GetCellValueAsString(iRow, 6);
                    


                    Producto producto = new Producto(nombreDelProducto, categoria, marca, cantidad, precio, descripcion, diaActual);
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
                        comando.Parameters.Add("@p7", SqlDbType.DateTime).Value = diaActual;
                        comando.ExecuteNonQuery();
                        conexion.Close();
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.Message);
                    }
                    iRow++;
                }
                MessageBox.Show("Se importo con exito");
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            abrirFormEnPanel(new Productos(nombre));
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            abrirFormEnPanel(new ListaProductos(nombre));
        }

        private void categoriaButton_Click(object sender, EventArgs e)
        {
            abrirFormEnPanel(new Categorias(nombre));
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            abrirFormEnPanel(new Exportar());
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            abrirFormEnPanel(new Usuarios(nombre));
        }
    }
}

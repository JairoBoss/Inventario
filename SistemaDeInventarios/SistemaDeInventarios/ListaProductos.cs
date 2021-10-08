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
    public partial class ListaProductos : Form
    {
        private string cadenita = Properties.Settings.Default.BD_InvetarioConnectionString;
        public ListaProductos()
        {
            InitializeComponent();
        }

        
        private void ListaProductos_Load(object sender, EventArgs e)
        {

            actualizarItems();

        }

        private void actualizarItems()
        {
            listView1.Items.Clear();

            SqlConnection conexion = new SqlConnection(cadenita);

            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("select id, nombreDelProducto, categoria, marca, cantidad, precio, descripcion from Productos", conexion);
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    ListViewItem filita = new ListViewItem(lector["id"].ToString());
                    filita.SubItems.Add(lector["nombreDelProducto"].ToString());
                    filita.SubItems.Add(lector["categoria"].ToString());
                    filita.SubItems.Add(lector["marca"].ToString());
                    filita.SubItems.Add(lector["cantidad"].ToString());
                    filita.SubItems.Add(lector["precio"].ToString());
                    filita.SubItems.Add(lector["descripcion"].ToString());
                    listView1.Items.Add(filita);
                }

                conexion.Close();


            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private bool itemSeleccionado = false;
        private int idProducto = 0;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemSeleccionado = true;
            foreach (ListViewItem lv in listView1.SelectedItems)
            {
                idProducto = Convert.ToInt32(lv.Text);
            }

        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (itemSeleccionado)
            {

                itemSeleccionado = false;
                idProducto = 0;
                foreach (ListViewItem lv in listView1.SelectedItems)
                {                    
                    int id = Convert.ToInt32(lv.Text);

                    SqlConnection conexion = new SqlConnection(cadenita);


                    try
                    {
                        conexion.Open();
                        
                        SqlCommand comando = new SqlCommand("select cantidad from Productos where id = @p1 ", conexion);
                        comando.Parameters.Add("@p1", SqlDbType.Int).Value = id;
                        SqlDataReader lector = comando.ExecuteReader();
                        int cantidadProducto = 0;
                        while (lector.Read())
                        {
                            cantidadProducto = Convert.ToInt32(lector["cantidad"].ToString());
                        }
                        conexion.Close();

                        cantidadProducto++;

                        conexion.Open();

                        SqlCommand comando1 = new SqlCommand("update Productos set cantidad = @p2 where id = @id", conexion);                        
                        comando1.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                        comando1.Parameters.AddWithValue("@p2", SqlDbType.Int).Value = cantidadProducto;
                        SqlDataReader lector1 = comando1.ExecuteReader();                       
                        conexion.Close();

                        

                        
                        

                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.Message);
                    }
                    actualizarItems();

                }                    
            }
            else
            {
                Error error = new Error();
                error.ShowDialog();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (itemSeleccionado)
            {

                itemSeleccionado = false;
                foreach (ListViewItem lv in listView1.SelectedItems)
                {
                    int id = Convert.ToInt32(lv.Text);

                    SqlConnection conexion = new SqlConnection(cadenita);


                    try
                    {
                        conexion.Open();

                        SqlCommand comando = new SqlCommand("select cantidad from Productos where id = @p1 ", conexion);
                        comando.Parameters.Add("@p1", SqlDbType.Int).Value = id;
                        SqlDataReader lector = comando.ExecuteReader();
                        int cantidadProducto = 0;
                        while (lector.Read())
                        {
                            cantidadProducto = Convert.ToInt32(lector["cantidad"].ToString());
                        }
                        conexion.Close();
                        cantidadProducto--;

                        if(cantidadProducto <= 0)
                        {
                            conexion.Open();
                            SqlCommand comando1 = new SqlCommand("delete from Productos where id = @id", conexion);
                            comando1.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;                            
                            SqlDataReader lector1 = comando1.ExecuteReader();
                            conexion.Close();
                        }
                        else
                        {
                            conexion.Open();
                            SqlCommand comando1 = new SqlCommand("update Productos set cantidad = @p2 where id = @id", conexion);
                            comando1.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                            comando1.Parameters.AddWithValue("@p2", SqlDbType.Int).Value = cantidadProducto;
                            SqlDataReader lector1 = comando1.ExecuteReader();
                            conexion.Close();
                        }
                        

                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.Message);
                    }
                    actualizarItems();

                }
            }
            else
            {
                Error error = new Error();
                error.ShowDialog();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Detalle details = new Detalle(idProducto);
            details.ShowDialog();

            //NoListo no= new NoListo();
            //no.ShowDialog();
        }
    }
}


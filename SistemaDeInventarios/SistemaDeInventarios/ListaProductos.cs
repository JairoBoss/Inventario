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
        private string nombre;
        private int idUsuario = -1;

        public ListaProductos(String nombre)
        {
            this.nombre = nombre;
            InitializeComponent();
            SqlConnection conexion = new SqlConnection(cadenita);
            conexion.Open();
            SqlCommand obtenerIdUsuario = new SqlCommand("select id from Usuarios where Usuario = @nombre", conexion);
            obtenerIdUsuario.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
            SqlDataReader lector = obtenerIdUsuario.ExecuteReader();
            while (lector.Read())
            {
                idUsuario = Convert.ToInt32(lector["id"]);
            }
            conexion.Close();
        }

        
        private void ListaProductos_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dSInventario.Productos' table. You can move, or remove it, as needed.
            this.productosTableAdapter.Fill(this.dSInventario.Productos);

            actualizarItems();
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 13);
            this.dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

        }

        private void actualizarItems()
        {
            //listView1.Items.Clear();

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
                    //listView1.Items.Add(filita);
                }

                conexion.Close();


            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e) //Agregar
        {

            if (txtCantidad.Text != "")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow fila in dataGridView1.SelectedRows)
                    {
                        int idProducto = Convert.ToInt16(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString());
                        int cantidadProducto = Convert.ToInt16(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString());
                        

                        int cantidad = Convert.ToInt16(this.txtCantidad.Text);
                        cantidadProducto += cantidad;



                        SqlConnection conexion = new SqlConnection(cadenita);
                        conexion.Open();
                        SqlCommand obtenerMarca = new SqlCommand("select nombreDelProducto from productos where id = @id", conexion);
                        obtenerMarca.Parameters.Add("@id", SqlDbType.Int).Value = idProducto;
                        SqlDataReader lector = obtenerMarca.ExecuteReader();
                        string producto = "";
                        while (lector.Read())
                        {
                            producto = lector["nombreDelProducto"].ToString();
                        }
                        conexion.Close();

                        conexion.Open();
                        SqlCommand nuevoComando = new SqlCommand("insert into UsuariosAcciones(IdUsuario, Accion, fecha) " +
                            "values (@User, @Accion, @fecha)", conexion);
                        nuevoComando.Parameters.Add("@User", SqlDbType.Int).Value = idUsuario;
                        nuevoComando.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "Agrego " + cantidad + " " + producto;
                        nuevoComando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                        nuevoComando.ExecuteNonQuery();
                        conexion.Close();



                        txtCantidad.Clear();

                        //Sumamos uno
                        this.productosTableAdapter.ActualizarCantidad(cantidadProducto, idProducto); //Realiza la operacion
                        this.productosTableAdapter.Fill(this.dSInventario.Productos); //Actualiza

                    }



                    /*if (itemSeleccionado)
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
                    }*/
                }
            }
            else
            {
                MessageBox.Show("ERROR");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e) //Eliminar
        {
            if(this.txtCantidad.Text != "")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow fila in dataGridView1.SelectedRows)
                    {
                        int idProducto = Convert.ToInt16(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString());
                        int cantidadProducto = Convert.ToInt16(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString());
                        

                        int cantidad = Convert.ToInt16(this.txtCantidad.Text);
                        

                        txtCantidad.Clear();

                        if(cantidad <= cantidadProducto)
                        {
                            cantidadProducto -= cantidad;
                            if (cantidadProducto > 0)
                            {
                                //Restamos uno
                                this.productosTableAdapter.ActualizarCantidad(cantidadProducto, idProducto); //Realiza la operacion
                                this.productosTableAdapter.Fill(this.dSInventario.Productos); //Actualiza                                
                            }
                            else
                            {
                                //Lo eliminamos de la tabla
                                this.productosTableAdapter.EliminarProducto(idProducto); //Realiza la operacion
                                this.productosTableAdapter.Fill(this.dSInventario.Productos); //Actualiza
                            }

                            SqlConnection conexion = new SqlConnection(cadenita);
                            conexion.Open();
                            SqlCommand obtenerMarca = new SqlCommand("select nombreDelProducto from productos where id = @id", conexion);
                            obtenerMarca.Parameters.Add("@id", SqlDbType.Int).Value = idProducto;
                            SqlDataReader lector = obtenerMarca.ExecuteReader();
                            string producto = "";
                            while (lector.Read())
                            {
                                producto = lector["nombreDelProducto"].ToString();
                            }
                            conexion.Close();

                            conexion.Open();
                            SqlCommand nuevoComando = new SqlCommand("insert into UsuariosAcciones(IdUsuario, Accion, fecha) " +
                                "values (@User, @Accion, @fecha)", conexion);
                            nuevoComando.Parameters.Add("@User", SqlDbType.Int).Value = idUsuario;
                            nuevoComando.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "Elimino " + cantidad + " " + producto;
                            nuevoComando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                            nuevoComando.ExecuteNonQuery();
                            conexion.Close();

                        }
                        else
                        {
                            MessageBox.Show("ERROR NO PUEDES ELIMINAR MAS DE LO QUE HAY, solo hay: "+ cantidadProducto);
                            
                        }


                    }
                }
            }
            else
            {
                MessageBox.Show("Error");
            }
            
            
            
            
            /*if (itemSeleccionado)
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
            }*/
        }

        private void pictureBox3_Click(object sender, EventArgs e) //Detalles
        {
            Detalle details = new Detalle(idProducto);
            details.ShowDialog();            
        }


        private int idProducto;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView temp = (DataGridView)sender;
            if (temp == null)
                return;

            try
            {
                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    idProducto = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                }
            }
            catch (Exception e1)
            {

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

        /*private void pictureBox4_Click(object sender, EventArgs e)
        {
            editar details = new editar(idProducto);
            details.ShowDialog();
        }*/
    }
}


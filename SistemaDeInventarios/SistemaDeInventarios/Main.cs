using SistemaDeInventarios.Clases;
using SpreadsheetLight;
using System;
using System.Runtime.CompilerServices;
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

        private bool verificarCategoria(String categoria)       
        {
            //Verificar si la categoria de ese producto existe
            bool existe = false;

            SqlConnection conexion = new SqlConnection(cadenita);

            try
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand("select id, nombre from Categoria where nombre = @p1 ", conexion);
                comando.Parameters.Add("@p1", SqlDbType.VarChar).Value = categoria;
                SqlDataReader lector = comando.ExecuteReader();

                int idCategoria = -1;
                string nombreCategoria = "";
                
                while (lector.Read())
                {
                    idCategoria = Convert.ToInt32(lector["Id"].ToString());                    
                    categoria = lector["nombre"].ToString();
                }
                conexion.Close();

                if(idCategoria != -1)
                {
                    existe = true;
                }
                else
                {
                    conexion.Open();


                    SqlCommand comando2 = new SqlCommand("insert into Categoria (Nombre, Descripcion) values (@p1, @p2)", conexion);
                    comando2.Parameters.Add("@p1", SqlDbType.VarChar).Value = categoria;
                    comando2.Parameters.Add("@p2", SqlDbType.VarChar).Value = "Categoria insertada desde excel";
                    comando2.ExecuteNonQuery();
                    conexion.Close();
                    
                }

            }
            catch (Exception e1)
            {}
            return existe;
        }

        private int getCategoria(String categoria)
        {
            SqlConnection conexion = new SqlConnection(cadenita);
            conexion.Open();

            SqlCommand comando = new SqlCommand("select id, nombre from Categoria where nombre = @p1 ", conexion);
            comando.Parameters.Add("@p1", SqlDbType.VarChar).Value = categoria;
            SqlDataReader lector = comando.ExecuteReader();

            int idCategoria = -1;
            string nombreCategoria = "";

            while (lector.Read())
            {
                idCategoria = Convert.ToInt32(lector["Id"].ToString());
                categoria = lector["nombre"].ToString();
            }
            conexion.Close();

            return idCategoria;
        }

        private int getMarca(String marca)
        {
            SqlConnection conexion = new SqlConnection(cadenita);
            conexion.Open();

            SqlCommand comando = new SqlCommand("select id from Marca where marca = @p1 ", conexion);
            comando.Parameters.Add("@p1", SqlDbType.VarChar).Value = marca;
            SqlDataReader lector = comando.ExecuteReader();

            int idCategoria = -1;
            

            while (lector.Read())
            {
                idCategoria = Convert.ToInt32(lector["Id"].ToString());                
            }
            conexion.Close();

            return idCategoria;
        }

        private bool verificarMarca(String marca, int categoria)
        {
            //Verificar si la categoria de ese producto existe
            bool existe = false;

            SqlConnection conexion = new SqlConnection(cadenita);

            try
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand("select id, marca from Marca where Marca = @p1 ", conexion);
                comando.Parameters.Add("@p1", SqlDbType.VarChar).Value = marca;
                SqlDataReader lector = comando.ExecuteReader();

                int idCategoria = -1;
                string nombreCategoria = "";

                while (lector.Read())
                {
                    idCategoria = Convert.ToInt32(lector["Id"].ToString());
                    nombreCategoria = lector["Marca"].ToString();
                }
                conexion.Close();

                if (idCategoria != -1)
                {
                    
                    existe = true;
                }
                else
                {
                    
                    conexion.Open();

                    SqlCommand comando2 = new SqlCommand("insert into Marca (Marca, Categoria) values (@p1, @p2)", conexion);
                    comando2.Parameters.Add("@p1", SqlDbType.VarChar).Value = marca;
                    comando2.Parameters.Add("@p2", SqlDbType.VarChar).Value = categoria;
                    comando2.ExecuteNonQuery();
                    conexion.Close();

                }

            }
            catch (Exception e1)
            { }
            return existe;
        }

        private bool productoExistente(String nombre, int categoria, int marca)
        {
            bool existe = false;

            SqlConnection conexion = new SqlConnection(cadenita);

            try
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand("select id, nombreDelProducto from Productos" +
                    " where (nombreDelProducto = @p1) and (categoria = @p2) and (marca = @p3) ", conexion);
                comando.Parameters.Add("@p1", SqlDbType.VarChar).Value = nombre;
                comando.Parameters.Add("@p2", SqlDbType.Int).Value = categoria;
                comando.Parameters.Add("@p3", SqlDbType.Int).Value = marca;
                SqlDataReader lector = comando.ExecuteReader();

                int idCategoria = -1;
                string nombreee = "";
                while (lector.Read())
                {
                    idCategoria = Convert.ToInt32(lector["Id"].ToString());
                    nombreee = lector["nombreDelProducto"].ToString();
                }
                conexion.Close();
                
                if (idCategoria != -1)
                {
                    
                    //Hacemos un update al producto con la cantidad correspondiente
                    existe = true;
                }
                else
                {
                    


/*
                    conexion.Open();
                    SqlCommand comando2 = new SqlCommand("insert into Categoria (Nombre, Descripcion) values (@p1, @p2)", conexion);
                    comando2.Parameters.Add("@p1", SqlDbType.VarChar).Value = categoria;
                    comando2.Parameters.Add("@p2", SqlDbType.VarChar).Value = "Categoria insertada desde excel";
                    //comando2.ExecuteNonQuery();
                    conexion.Close();
*/
                }

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            return existe;
        }

        private int getIdProducto(String nombre, int categoria, int marca)
        {
            SqlConnection conexion = new SqlConnection(cadenita);

            conexion.Open();

            SqlCommand comando = new SqlCommand("select id, nombreDelProducto from Productos" +
                " where (nombreDelProducto = @p1) and (categoria = @p2) and (marca = @p3) ", conexion);
            comando.Parameters.Add("@p1", SqlDbType.VarChar).Value = nombre;
            comando.Parameters.Add("@p2", SqlDbType.Int).Value = categoria;
            comando.Parameters.Add("@p3", SqlDbType.Int).Value = marca;
            SqlDataReader lector = comando.ExecuteReader();

            int idCategoria = -1;
            
            while (lector.Read())
            {
                idCategoria = Convert.ToInt32(lector["Id"].ToString());

            }
            conexion.Close();
            
            return idCategoria;
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

                    /*
                     AQUI TENEMOS QUE CHECAR QUE ROLLO
                     */

                    
                    bool verifyCategoria = verificarCategoria(categoria);
                    bool verifyMarca = verificarMarca(marca, getCategoria(categoria));

                    int marcaNumber = getMarca(marca);
                    int categoriaNumber = getCategoria(categoria);

                    if(!verifyCategoria|| !verifyMarca )
                    {
                        /*if (!verifyCategoria)
                        {
                            MessageBox.Show("No existe la categoria, por lo tanto aqui se tiene que crear");
                        }
                        else
                        {
                            MessageBox.Show("No existe la marca, todo bien");
                        }*/
                        /*
                         * 
                         * 
                         * 
                         * 
                        try
                        {
                            conexion.Open();
                            SqlCommand comando = new SqlCommand("insert into productos(nombreDelProducto, categoria, marca, cantidad, precio, descripcion, diaDeRegistro) " +
                                "values (@p1, @p2, @p3, @p4, @p5, @p6, @p7)", conexion);
                            comando.Parameters.Add("@p1", SqlDbType.VarChar).Value = nombreDelProducto;
                            comando.Parameters.Add("@p2", SqlDbType.Int).Value = categoriaNumber;
                            comando.Parameters.Add("@p3", SqlDbType.Int).Value = marcaNumber;
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
                         *
                         *
                         *
                         */

                    }
                    else
                    {
                        int cantidadDelProductoLoL = -1;
                        if (!productoExistente(nombreDelProducto, categoriaNumber, marcaNumber))
                        {
                            Producto producto = new Producto(nombreDelProducto, categoria, marca, cantidad, precio, descripcion, diaActual);
                            try
                            {
                                conexion.Open();                                
                                SqlCommand comando = new SqlCommand("insert into productos(nombreDelProducto, categoria, marca, cantidad, precio, descripcion, diaDeRegistro) " +
                                    "values (@p1, @p2, @p3, @p4, @p5, @p6, @p7)", conexion);
                                comando.Parameters.Add("@p1", SqlDbType.VarChar).Value = nombreDelProducto;
                                comando.Parameters.Add("@p2", SqlDbType.Int).Value = categoriaNumber;
                                comando.Parameters.Add("@p3", SqlDbType.Int).Value = marcaNumber;
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
                        }
                        else
                        {
                            
                            int idProducto = getIdProducto(nombreDelProducto, categoriaNumber, marcaNumber);
                            try
                            {
                                conexion.Open();
                                SqlCommand comando = new SqlCommand("select cantidad from productos where id = @p1" , conexion);
                                
                                comando.Parameters.Add("@p1", SqlDbType.Int).Value = idProducto;

                                SqlDataReader lector = comando.ExecuteReader();                                
                                while (lector.Read())
                                {
                                    cantidadDelProductoLoL = Convert.ToInt32(lector["cantidad"].ToString());

                                }
                                
                                conexion.Close();


                            }
                            catch (Exception e1)
                            {
                                
                            }
                            

                            int x = cantidad + cantidadDelProductoLoL;


                            actualizarCantidad(x, idProducto);

                        }
                        /*
                         *
                         * Checar si existe el producto, si existe lo actualizamos
                         *
                         */
                    }
                    
                    /*
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
                    */
                    iRow++;
                }
                
            }
        }

        private void actualizarCantidad(int x, int y)
        {
            SqlConnection conexion = new SqlConnection(cadenita);
            conexion.Open();
            SqlCommand nuevoComando = new SqlCommand("update productos set cantidad = @p1 where id = @p2", conexion);
            nuevoComando.Parameters.Add("@p1", SqlDbType.Int).Value = x;
            nuevoComando.Parameters.Add("@p2", SqlDbType.Int).Value = y;

            //MessageBox.Show(x + " " + y);
            nuevoComando.ExecuteNonQuery();
            conexion.Close();
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

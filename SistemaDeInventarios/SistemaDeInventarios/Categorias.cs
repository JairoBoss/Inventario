using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeInventarios
{
    public partial class Categorias : Form
    {
        public Categorias()
        {
            InitializeComponent();
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
            this.categoriaTableAdapter.EliminarCategoria(idSeleccionado);
            this.categoriaTableAdapter.Fill(this.dSInventario.Categoria); //Actualiza
            cleanCategoria();
        }

        private void pictureBox3_Click(object sender, EventArgs e) //Actualizar
        {
            String nombreCategoria = txtCategoria.Text;
            String descripcionCategoria = txtDescripcion.Text;
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

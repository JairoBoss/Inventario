using Microsoft.Reporting.WinForms;
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
    public partial class ExportarInventario : Form
    {
        private string cadenita = Properties.Settings.Default.BD_InvetarioConnectionString;

        public ExportarInventario()
        {
            InitializeComponent();
        }

        private void ExportarInventario_Load(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(cadenita);
            conexion.Open();
            SqlCommand comando = new SqlCommand("SELECT Productos.Id, Productos.nombreDelProducto, Productos.cantidad, " +
                "Productos.precio, Categoria.Nombre AS Categoria, Marca.Marca, Productos.descripcion, Productos.rutaDeLaImagen, " +
                "Productos.diaDeRegistro FROM Productos INNER JOIN Categoria ON Productos.categoria = Categoria.Id INNER " +
                "JOIN Marca ON Productos.marca = Marca.Id AND Categoria.Id = Marca.Categoria", conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataSet dataset = new DataSet();
            adaptador.Fill(dataset);
            conexion.Close();
            ReportDataSource rds = new ReportDataSource("DataSet1", dataset.Tables[0]);

            reportViewer1.Reset();
            reportViewer1.LocalReport.Dispose();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.ReportPath = "C:/Users/jairo/Desktop/Inventario/SistemaDeInventarios/SistemaDeInventarios/ReporteInventario.rdlc";          
            reportViewer1.LocalReport.Refresh();
            reportViewer1.RefreshReport();

        }
    }
}

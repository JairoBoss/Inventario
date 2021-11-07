using Microsoft.Reporting.WinForms;
using SistemaDeInventarios.BD_InvetarioDataSet5TableAdapters;
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
    public partial class ExportarUsuarioIndividual : Form
    {
        private int idUsuario = -1;
        private string cadenita = Properties.Settings.Default.BD_InvetarioConnectionString; 
        public ExportarUsuarioIndividual(int idUser)
        {
            InitializeComponent();
            this.idUsuario = idUser;
        }

        private void ExportarUsuarioIndividual_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'BD_InvetarioDataSet8.getUnReporte' table. You can move, or remove it, as needed.
            this.getUnReporteTableAdapter.Fill(this.BD_InvetarioDataSet8.getUnReporte, idUsuario);

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}

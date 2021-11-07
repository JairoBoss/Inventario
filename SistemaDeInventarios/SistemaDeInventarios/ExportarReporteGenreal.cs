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
    public partial class ExportarReporteGenreal : Form
    {
        public ExportarReporteGenreal()
        {
            InitializeComponent();
        }

        private void ExportarReporteGenreal_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'BD_InvetarioDataSet3.ReporteGeneral' table. You can move, or remove it, as needed.
            this.ReporteGeneralTableAdapter.Fill(this.BD_InvetarioDataSet3.ReporteGeneral);

            this.reportViewer1.RefreshReport();
        }
    }
}

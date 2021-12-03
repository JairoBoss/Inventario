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
    public partial class ExportarInventario : Form
    {
        public ExportarInventario()
        {
            InitializeComponent();
        }

        private void ExportarInventario_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'BD_InvetarioDataSet1.getInventario' table. You can move, or remove it, as needed.
            this.getInventarioTableAdapter.Fill(this.BD_InvetarioDataSet1.getInventario);

            this.reportViewer1.RefreshReport();
        }
    }
}

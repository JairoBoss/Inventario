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
    public partial class ExportarCategoria : Form
    {
        public ExportarCategoria()
        {
            InitializeComponent();
        }

        private void ExportarCategoria_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'BD_InvetarioDataSet2.categoriaMarca' table. You can move, or remove it, as needed.
            this.categoriaMarcaTableAdapter.Fill(this.BD_InvetarioDataSet2.categoriaMarca);

            this.reportViewer1.RefreshReport();
        }
    }
}

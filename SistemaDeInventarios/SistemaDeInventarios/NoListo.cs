using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeInventarios
{
    public partial class NoListo : Form
    {
        public NoListo()
        {
            InitializeComponent();
        }

        private void NoListo_Load(object sender, EventArgs e)
        {
            SystemSounds.Hand.Play();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = Color.Gray;

            TransparencyKey = Color.Gray;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

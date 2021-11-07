
namespace SistemaDeInventarios
{
    partial class ExportarInventario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportarInventario));
            this.getInventarioBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BD_InvetarioDataSet1 = new SistemaDeInventarios.BD_InvetarioDataSet1();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.getInventarioTableAdapter = new SistemaDeInventarios.BD_InvetarioDataSet1TableAdapters.getInventarioTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.getInventarioBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BD_InvetarioDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // getInventarioBindingSource
            // 
            this.getInventarioBindingSource.DataMember = "getInventario";
            this.getInventarioBindingSource.DataSource = this.BD_InvetarioDataSet1;
            // 
            // BD_InvetarioDataSet1
            // 
            this.BD_InvetarioDataSet1.DataSetName = "BD_InvetarioDataSet1";
            this.BD_InvetarioDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.getInventarioBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SistemaDeInventarios.ReporteInventario.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(922, 611);
            this.reportViewer1.TabIndex = 0;
            // 
            // getInventarioTableAdapter
            // 
            this.getInventarioTableAdapter.ClearBeforeFill = true;
            // 
            // ExportarInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 611);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "ExportarInventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportar Inventario";
            this.Load += new System.EventHandler(this.ExportarInventario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.getInventarioBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BD_InvetarioDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource getInventarioBindingSource;
        private BD_InvetarioDataSet1 BD_InvetarioDataSet1;
        private BD_InvetarioDataSet1TableAdapters.getInventarioTableAdapter getInventarioTableAdapter;
    }
}
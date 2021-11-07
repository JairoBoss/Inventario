
namespace SistemaDeInventarios
{
    partial class ExportarUsuarioIndividual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportarUsuarioIndividual));
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.BD_InvetarioDataSet8 = new SistemaDeInventarios.BD_InvetarioDataSet8();
            this.getUnReporteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.getUnReporteTableAdapter = new SistemaDeInventarios.BD_InvetarioDataSet8TableAdapters.getUnReporteTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.BD_InvetarioDataSet8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getUnReporteBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.getUnReporteBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SistemaDeInventarios.ReporteIndividual.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(922, 611);
            this.reportViewer1.TabIndex = 0;
            // 
            // BD_InvetarioDataSet8
            // 
            this.BD_InvetarioDataSet8.DataSetName = "BD_InvetarioDataSet8";
            this.BD_InvetarioDataSet8.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // getUnReporteBindingSource
            // 
            this.getUnReporteBindingSource.DataMember = "getUnReporte";
            this.getUnReporteBindingSource.DataSource = this.BD_InvetarioDataSet8;
            // 
            // getUnReporteTableAdapter
            // 
            this.getUnReporteTableAdapter.ClearBeforeFill = true;
            // 
            // ExportarUsuarioIndividual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 611);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExportarUsuarioIndividual";
            this.Text = "Reporte Usuario Individual";
            this.Load += new System.EventHandler(this.ExportarUsuarioIndividual_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BD_InvetarioDataSet8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getUnReporteBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource getUnReporteBindingSource;
        private BD_InvetarioDataSet8 BD_InvetarioDataSet8;
        private BD_InvetarioDataSet8TableAdapters.getUnReporteTableAdapter getUnReporteTableAdapter;
    }
}
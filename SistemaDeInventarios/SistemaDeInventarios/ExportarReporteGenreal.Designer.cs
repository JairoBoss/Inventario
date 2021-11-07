
namespace SistemaDeInventarios
{
    partial class ExportarReporteGenreal
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.BD_InvetarioDataSet3 = new SistemaDeInventarios.BD_InvetarioDataSet3();
            this.ReporteGeneralBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ReporteGeneralTableAdapter = new SistemaDeInventarios.BD_InvetarioDataSet3TableAdapters.ReporteGeneralTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.BD_InvetarioDataSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReporteGeneralBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ReporteGeneralBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SistemaDeInventarios.ReporteUsuario.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // BD_InvetarioDataSet3
            // 
            this.BD_InvetarioDataSet3.DataSetName = "BD_InvetarioDataSet3";
            this.BD_InvetarioDataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReporteGeneralBindingSource
            // 
            this.ReporteGeneralBindingSource.DataMember = "ReporteGeneral";
            this.ReporteGeneralBindingSource.DataSource = this.BD_InvetarioDataSet3;
            // 
            // ReporteGeneralTableAdapter
            // 
            this.ReporteGeneralTableAdapter.ClearBeforeFill = true;
            // 
            // ExportarReporteGenreal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ExportarReporteGenreal";
            this.Text = "ExportarReporteGenreal";
            this.Load += new System.EventHandler(this.ExportarReporteGenreal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BD_InvetarioDataSet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReporteGeneralBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ReporteGeneralBindingSource;
        private BD_InvetarioDataSet3 BD_InvetarioDataSet3;
        private BD_InvetarioDataSet3TableAdapters.ReporteGeneralTableAdapter ReporteGeneralTableAdapter;
    }
}
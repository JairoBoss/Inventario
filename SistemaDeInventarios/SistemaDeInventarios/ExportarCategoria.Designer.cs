
namespace SistemaDeInventarios
{
    partial class ExportarCategoria
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
            this.BD_InvetarioDataSet2 = new SistemaDeInventarios.BD_InvetarioDataSet2();
            this.categoriaMarcaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.categoriaMarcaTableAdapter = new SistemaDeInventarios.BD_InvetarioDataSet2TableAdapters.categoriaMarcaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.BD_InvetarioDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoriaMarcaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DSCategorias";
            reportDataSource1.Value = this.categoriaMarcaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SistemaDeInventarios.ReporteCategorias.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(922, 611);
            this.reportViewer1.TabIndex = 0;
            // 
            // BD_InvetarioDataSet2
            // 
            this.BD_InvetarioDataSet2.DataSetName = "BD_InvetarioDataSet2";
            this.BD_InvetarioDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // categoriaMarcaBindingSource
            // 
            this.categoriaMarcaBindingSource.DataMember = "categoriaMarca";
            this.categoriaMarcaBindingSource.DataSource = this.BD_InvetarioDataSet2;
            // 
            // categoriaMarcaTableAdapter
            // 
            this.categoriaMarcaTableAdapter.ClearBeforeFill = true;
            // 
            // ExportarCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 611);
            this.Controls.Add(this.reportViewer1);
            this.MinimizeBox = false;
            this.Name = "ExportarCategoria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExportarCategoria";
            this.Load += new System.EventHandler(this.ExportarCategoria_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BD_InvetarioDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoriaMarcaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource categoriaMarcaBindingSource;
        private BD_InvetarioDataSet2 BD_InvetarioDataSet2;
        private BD_InvetarioDataSet2TableAdapters.categoriaMarcaTableAdapter categoriaMarcaTableAdapter;
    }
}
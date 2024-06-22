namespace Supermarket_2
{
    partial class Summary
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
            this.supermarketDataSet6 = new Supermarket_2.SupermarketDataSet6();
            this.supermarketDataSet6BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Billing_summaryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.supermarketDataSet6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.supermarketDataSet6BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Billing_summaryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.supermarketDataSet6BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Supermarket_2.SummaryRep.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(40, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(730, 426);
            this.reportViewer1.TabIndex = 1;
            // 
            // supermarketDataSet6
            // 
            this.supermarketDataSet6.DataSetName = "SupermarketDataSet6";
            this.supermarketDataSet6.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // supermarketDataSet6BindingSource
            // 
            this.supermarketDataSet6BindingSource.DataSource = this.supermarketDataSet6;
            this.supermarketDataSet6BindingSource.Position = 0;
            // 
            // Billing_summaryBindingSource
            // 
            this.Billing_summaryBindingSource.DataMember = "Billing_summary";
            this.Billing_summaryBindingSource.DataSource = this.supermarketDataSet6;
            // 
            // Summary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Summary";
            this.Text = "Summary";
            ((System.ComponentModel.ISupportInitialize)(this.supermarketDataSet6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.supermarketDataSet6BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Billing_summaryBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource supermarketDataSet6BindingSource;
        private SupermarketDataSet6 supermarketDataSet6;
        private System.Windows.Forms.BindingSource Billing_summaryBindingSource;
    }
}
// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools.PrintReportForm
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.PdfPrintCenter.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools
{
    internal class PrintReportForm : Form
    {
        private IContainer components;
        private WebBrowser webBrowser;
        private PictureBox pictureBoxAttention;
        private Label labelAttention;
        private Button buttonPrintReport;
        private Button buttonCloseReport;

        public PrintReportForm() => this.InitializeComponent();

        public PrintReportForm(string reportFilePath)
        {
            this.InitializeComponent();
            if (!File.Exists(reportFilePath))
                return;
            this.webBrowser.Navigate(reportFilePath);
        }

        private void ButtonPrintReport_Click(object sender, EventArgs e)
        {
            this.webBrowser.ShowPrintDialog();
        }

        private void ButtonCloseReport_Click(object sender, EventArgs e) => this.Close();

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.webBrowser = new WebBrowser();
            this.pictureBoxAttention = new PictureBox();
            this.labelAttention = new Label();
            this.buttonPrintReport = new Button();
            this.buttonCloseReport = new Button();
            ((ISupportInitialize)this.pictureBoxAttention).BeginInit();
            this.SuspendLayout();
            this.webBrowser.AllowWebBrowserDrop = false;
            this.webBrowser.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.Location = new Point(0, 0);
            this.webBrowser.MinimumSize = new Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new Size(747, 460);
            this.webBrowser.TabIndex = 0;
            this.pictureBoxAttention.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.pictureBoxAttention.Image = (Image)Resources.PNG_Attention;
            this.pictureBoxAttention.Location = new Point(17, 478);
            this.pictureBoxAttention.Name = "pictureBoxAttention";
            this.pictureBoxAttention.Size = new Size(16 /*0x10*/, 16 /*0x10*/);
            this.pictureBoxAttention.SizeMode = PictureBoxSizeMode.AutoSize;
            this.pictureBoxAttention.TabIndex = 1;
            this.pictureBoxAttention.TabStop = false;
            this.pictureBoxAttention.Visible = false;
            this.labelAttention.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.labelAttention.AutoSize = true;
            this.labelAttention.Location = new Point(39, 481);
            this.labelAttention.Name = "labelAttention";
            this.labelAttention.Size = new Size(228, 13);
            this.labelAttention.TabIndex = 3;
            this.labelAttention.Text = "Документ печатается на разных принтерах";
            this.labelAttention.Visible = false;
            this.buttonPrintReport.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.buttonPrintReport.Location = new Point(494, 475);
            this.buttonPrintReport.Name = "buttonPrintReport";
            this.buttonPrintReport.Size = new Size(112 /*0x70*/, 23);
            this.buttonPrintReport.TabIndex = 4;
            this.buttonPrintReport.Text = "Печатать отчёт";
            this.buttonPrintReport.UseVisualStyleBackColor = true;
            this.buttonPrintReport.Click += new EventHandler(this.ButtonPrintReport_Click);
            this.buttonCloseReport.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.buttonCloseReport.DialogResult = DialogResult.Cancel;
            this.buttonCloseReport.Location = new Point(617, 475);
            this.buttonCloseReport.Name = "buttonCloseReport";
            this.buttonCloseReport.Size = new Size(112 /*0x70*/, 23);
            this.buttonCloseReport.TabIndex = 5;
            this.buttonCloseReport.Text = "Закрыть отчёт";
            this.buttonCloseReport.UseVisualStyleBackColor = true;
            this.buttonCloseReport.Click += new EventHandler(this.ButtonCloseReport_Click);
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Window;
            this.CancelButton = (IButtonControl)this.buttonCloseReport;
            this.ClientSize = new Size(747, 513);
            this.Controls.Add((Control)this.buttonCloseReport);
            this.Controls.Add((Control)this.buttonPrintReport);
            this.Controls.Add((Control)this.labelAttention);
            this.Controls.Add((Control)this.pictureBoxAttention);
            this.Controls.Add((Control)this.webBrowser);
            this.MinimumSize = new Size(763, 552);
            this.Name = nameof(PrintReportForm);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Отчёт по печати";
            ((ISupportInitialize)this.pictureBoxAttention).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

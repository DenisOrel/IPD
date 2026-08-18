// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings.NewSheetDialog
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings
{
    internal class NewSheetDialog : Form
    {
        private IContainer components;
        private GroupBox groupBox3;
        private NumericUpDown pageTopUpDown;
        private Label label3;
        private Label label4;
        private NumericUpDown pageLeftUpDown;
        private GroupBox groupBox1;
        private ComboBox fmtComboBox;
        private Button cancelBtn;
        private Button okBtn;
        private Label labelFormat;

        public NewSheetDialog()
        {
            this.InitializeComponent();
            KnownPaperFormats.LoadToComboBox(this.fmtComboBox);
            this.fmtComboBox.SelectedIndex = 4;
        }

        public FormatLocation SelectedFormat
        {
            get
            {
                KnownPaperFormat selectedItem = this.fmtComboBox.SelectedItem as KnownPaperFormat;
                return new FormatLocation()
                {
                    Left = Convert.ToInt32(this.pageLeftUpDown.Value),
                    Top = Convert.ToInt32(this.pageTopUpDown.Value),
                    Format = selectedItem
                };
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.groupBox3 = new GroupBox();
            this.pageTopUpDown = new NumericUpDown();
            this.label3 = new Label();
            this.label4 = new Label();
            this.pageLeftUpDown = new NumericUpDown();
            this.groupBox1 = new GroupBox();
            this.labelFormat = new Label();
            this.fmtComboBox = new ComboBox();
            this.cancelBtn = new Button();
            this.okBtn = new Button();
            this.groupBox3.SuspendLayout();
            this.pageTopUpDown.BeginInit();
            this.pageLeftUpDown.BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            this.groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.groupBox3.Controls.Add((Control)this.pageTopUpDown);
            this.groupBox3.Controls.Add((Control)this.label3);
            this.groupBox3.Controls.Add((Control)this.label4);
            this.groupBox3.Controls.Add((Control)this.pageLeftUpDown);
            this.groupBox3.Location = new Point(0, 105);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(251, 110);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Расположение листа";
            this.pageTopUpDown.Location = new Point(89, 45);
            this.pageTopUpDown.Maximum = new Decimal(new int[4]
            {
          10000,
          0,
          0,
          0
            });
            this.pageTopUpDown.Name = "pageTopUpDown";
            this.pageTopUpDown.Size = new Size(49, 20);
            this.pageTopUpDown.TabIndex = 10;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(65, 47);
            this.label3.Name = "label3";
            this.label3.Size = new Size(17, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Y:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(64 /*0x40*/, 24);
            this.label4.Name = "label4";
            this.label4.Size = new Size(17, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "X:";
            this.pageLeftUpDown.Location = new Point(89, 22);
            this.pageLeftUpDown.Maximum = new Decimal(new int[4]
            {
          10000,
          0,
          0,
          0
            });
            this.pageLeftUpDown.Name = "pageLeftUpDown";
            this.pageLeftUpDown.Size = new Size(49, 20);
            this.pageLeftUpDown.TabIndex = 7;
            this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.groupBox1.Controls.Add((Control)this.labelFormat);
            this.groupBox1.Controls.Add((Control)this.fmtComboBox);
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(251, 99);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры макета";
            this.labelFormat.AutoSize = true;
            this.labelFormat.Location = new Point(12, 26);
            this.labelFormat.Name = "labelFormat";
            this.labelFormat.Size = new Size(52, 13);
            this.labelFormat.TabIndex = 2;
            this.labelFormat.Text = "Формат:";
            this.fmtComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.fmtComboBox.FormattingEnabled = true;
            this.fmtComboBox.Items.AddRange(new object[6]
            {
          (object) "A0",
          (object) "A1",
          (object) "A2",
          (object) "A3",
          (object) "A4",
          (object) "A5"
            });
            this.fmtComboBox.Location = new Point(40, 52);
            this.fmtComboBox.Name = "fmtComboBox";
            this.fmtComboBox.Size = new Size(173, 21);
            this.fmtComboBox.TabIndex = 1;
            this.cancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.cancelBtn.DialogResult = DialogResult.Cancel;
            this.cancelBtn.Location = new Point(144 /*0x90*/, 221);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new Size(95, 25);
            this.cancelBtn.TabIndex = 4;
            this.cancelBtn.Text = "Отмена";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.okBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.okBtn.DialogResult = DialogResult.OK;
            this.okBtn.Location = new Point(40, 221);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new Size(95, 25);
            this.okBtn.TabIndex = 5;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.AcceptButton = (IButtonControl)this.okBtn;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = (IButtonControl)this.cancelBtn;
            this.ClientSize = new Size(251, 254);
            this.Controls.Add((Control)this.okBtn);
            this.Controls.Add((Control)this.cancelBtn);
            this.Controls.Add((Control)this.groupBox1);
            this.Controls.Add((Control)this.groupBox3);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new Size(267, 293);
            this.Name = "NewSheet";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Добавить лист";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.pageTopUpDown.EndInit();
            this.pageLeftUpDown.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}

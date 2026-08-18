// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings.SelectFormatDialog
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings
{
    internal class SelectFormatDialog : Form
    {
        private IContainer components;
        private Label labelAvailableFormats;
        private ComboBox comboBoxAvailableFormats;
        private Button buttonCancel;
        private Button buttonOk;

        public SelectFormatDialog(List<KnownPaperFormat> smallerFormats)
        {
            this.InitializeComponent();
            this.InitializeComboBox(smallerFormats);
            this.CheckOkButton();
        }

        public KnownPaperFormat SelectedFormat { get; private set; }

        private void InitializeComboBox(List<KnownPaperFormat> smallerFormats)
        {
            this.comboBoxAvailableFormats.Items.AddRange((object[])smallerFormats.ToArray());
            this.comboBoxAvailableFormats.SelectedIndex = 0;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
                this.SelectedFormat = this.comboBoxAvailableFormats.SelectedItem as KnownPaperFormat;
            base.OnClosing(e);
        }

        private void ComboBoxAvailableFormats_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CheckOkButton();
        }

        private void CheckOkButton()
        {
            this.buttonOk.Enabled = this.comboBoxAvailableFormats.SelectedIndex >= 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelAvailableFormats = new Label();
            this.comboBoxAvailableFormats = new ComboBox();
            this.buttonCancel = new Button();
            this.buttonOk = new Button();
            this.SuspendLayout();
            this.labelAvailableFormats.AutoSize = true;
            this.labelAvailableFormats.Location = new Point(16 /*0x10*/, 23);
            this.labelAvailableFormats.Margin = new Padding(4, 0, 4, 0);
            this.labelAvailableFormats.Name = "labelAvailableFormats";
            this.labelAvailableFormats.Size = new Size(151, 17);
            this.labelAvailableFormats.TabIndex = 0;
            this.labelAvailableFormats.Text = "Доступные форматы:";
            this.comboBoxAvailableFormats.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxAvailableFormats.FormattingEnabled = true;
            this.comboBoxAvailableFormats.Location = new Point(20, 43);
            this.comboBoxAvailableFormats.Margin = new Padding(4);
            this.comboBoxAvailableFormats.Name = "comboBoxAvailableFormats";
            this.comboBoxAvailableFormats.Size = new Size(223, 24);
            this.comboBoxAvailableFormats.TabIndex = 1;
            this.comboBoxAvailableFormats.SelectedIndexChanged += new EventHandler(this.ComboBoxAvailableFormats_SelectedIndexChanged);
            this.buttonCancel.DialogResult = DialogResult.Cancel;
            this.buttonCancel.Location = new Point(171, 78);
            this.buttonCancel.Margin = new Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new Size((int)sbyte.MaxValue, 31 /*0x1F*/);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonOk.DialogResult = DialogResult.OK;
            this.buttonOk.Location = new Point(36, 78);
            this.buttonOk.Margin = new Padding(4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new Size((int)sbyte.MaxValue, 31 /*0x1F*/);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.AcceptButton = (IButtonControl)this.buttonOk;
            this.AutoScaleDimensions = new SizeF(8f, 16f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = (IButtonControl)this.buttonCancel;
            this.ClientSize = new Size(311, 122);
            this.Controls.Add((Control)this.buttonOk);
            this.Controls.Add((Control)this.buttonCancel);
            this.Controls.Add((Control)this.comboBoxAvailableFormats);
            this.Controls.Add((Control)this.labelAvailableFormats);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Margin = new Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new Size(329, 169);
            this.Name = nameof(SelectFormatDialog);
            this.RightToLeft = RightToLeft.No;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Выбор форматов";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

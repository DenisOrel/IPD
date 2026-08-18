
// Type: Intermech.PdfPrintCenter.Utils.DeleteFilesDialog




using Intermech.PdfPrintCenter.Utils.UtilMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.Utils
{
    internal class DeleteFilesDialog : Form
    {
      private IContainer components;
      private Label labelInfo;
      private PictureBox iconWarning;
      private RadioButton radioButtonAll;
      private RadioButton radioButtonPartly;
      private RadioButton radioButtonNothing;
      private Button buttonOk;
      private Button buttonCancel;
      private Panel panelRadioButtons;

      public DeleteFilesDialog(List<string> filesInPrintQueue, bool allInPrintQueue)
      {
        this.InitializeComponent();
        this.InitializeWarningIcon();
        this.InitializeInfo(filesInPrintQueue);
        this.InitializeRadioPanel(allInPrintQueue);
        this.InitializeButtons();
        this.SetNewHeight();
      }

      public DeleteFilesDialog.Actions Action { get; private set; }

      private void InitializeButtons()
      {
        int num = 25;
        int bottomYcoordinate = this.panelRadioButtons.GetBottomYCoordinate();
        this.buttonCancel.SetYCoordinate(bottomYcoordinate + num);
        this.buttonOk.SetYCoordinate(bottomYcoordinate + num);
        this.buttonOk.Enabled = false;
      }

      private void InitializeInfo(List<string> filesInPrintQueue)
      {
        this.labelInfo.Text = $"Наборы страниц следующих выделенных файлов находятся в очереди печати: {string.Join(", ", (IEnumerable<string>) filesInPrintQueue)}.\nКакое действие предпринять?";
      }

      private void InitializeRadioPanel(bool allInPrintQueue)
      {
        this.panelRadioButtons.SetYCoordinate(Math.Max(this.iconWarning.GetBottomYCoordinate(), this.labelInfo.GetBottomYCoordinate()) + 17);
        if (!allInPrintQueue)
          return;
        this.radioButtonPartly.Enabled = false;
      }

      private void InitializeWarningIcon()
      {
        this.iconWarning.Image = (Image) SystemIcons.Warning.ToBitmap();
      }

      private void SetNewHeight() => this.Height = this.buttonCancel.GetBottomYCoordinate() + 50;

      protected override void OnClosing(CancelEventArgs e)
      {
        if (this.DialogResult == DialogResult.OK)
          this.Action = !this.radioButtonAll.Checked ? (!this.radioButtonPartly.Checked ? DeleteFilesDialog.Actions.DeleteNothing : DeleteFilesDialog.Actions.DeletePartly) : DeleteFilesDialog.Actions.DeleteAll;
        else if (this.DialogResult == DialogResult.Cancel)
          this.Action = DeleteFilesDialog.Actions.DeleteNothing;
        base.OnClosing(e);
      }

      private void RadioButton_CheckedChanged(object sender, EventArgs e)
      {
        this.buttonOk.Enabled = this.radioButtonAll.Checked || this.radioButtonPartly.Checked || this.radioButtonNothing.Checked;
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        this.labelInfo = new Label();
        this.iconWarning = new PictureBox();
        this.radioButtonAll = new RadioButton();
        this.radioButtonPartly = new RadioButton();
        this.radioButtonNothing = new RadioButton();
        this.buttonOk = new Button();
        this.buttonCancel = new Button();
        this.panelRadioButtons = new Panel();
        ((ISupportInitialize) this.iconWarning).BeginInit();
        this.panelRadioButtons.SuspendLayout();
        this.SuspendLayout();
        this.labelInfo.AutoSize = true;
        this.labelInfo.Location = new Point(69, 13);
        this.labelInfo.MaximumSize = new Size(270, 500);
        this.labelInfo.Name = "labelInfo";
        this.labelInfo.Size = new Size(28, 13);
        this.labelInfo.TabIndex = 0;
        this.labelInfo.Text = "Text";
        this.iconWarning.Location = new Point(23, 12);
        this.iconWarning.Name = "iconWarning";
        this.iconWarning.Size = new Size(37, 37);
        this.iconWarning.TabIndex = 1;
        this.iconWarning.TabStop = false;
        this.radioButtonAll.AutoSize = true;
        this.radioButtonAll.Location = new Point(3, 3);
        this.radioButtonAll.Name = "radioButtonAll";
        this.radioButtonAll.Size = new Size(126, 17);
        this.radioButtonAll.TabIndex = 2;
        this.radioButtonAll.TabStop = true;
        this.radioButtonAll.Text = "Удалить все файлы";
        this.radioButtonAll.UseVisualStyleBackColor = true;
        this.radioButtonAll.CheckedChanged += new EventHandler(this.RadioButton_CheckedChanged);
        this.radioButtonPartly.AutoSize = true;
        this.radioButtonPartly.Location = new Point(3, 26);
        this.radioButtonPartly.Name = "radioButtonPartly";
        this.radioButtonPartly.Size = new Size(314, 17);
        this.radioButtonPartly.TabIndex = 3;
        this.radioButtonPartly.TabStop = true;
        this.radioButtonPartly.Text = "Удалить только файлы, находящиеся в рабочей области";
        this.radioButtonPartly.UseVisualStyleBackColor = true;
        this.radioButtonPartly.CheckedChanged += new EventHandler(this.RadioButton_CheckedChanged);
        this.radioButtonNothing.AutoSize = true;
        this.radioButtonNothing.Location = new Point(3, 49);
        this.radioButtonNothing.Name = "radioButtonNothing";
        this.radioButtonNothing.Size = new Size(119, 17);
        this.radioButtonNothing.TabIndex = 4;
        this.radioButtonNothing.TabStop = true;
        this.radioButtonNothing.Text = "Ничего не удалять";
        this.radioButtonNothing.UseVisualStyleBackColor = true;
        this.radioButtonNothing.CheckedChanged += new EventHandler(this.RadioButton_CheckedChanged);
        this.buttonOk.DialogResult = DialogResult.OK;
        this.buttonOk.Location = new Point(164, 182);
        this.buttonOk.Name = "buttonOk";
        this.buttonOk.Size = new Size(95, 25);
        this.buttonOk.TabIndex = 5;
        this.buttonOk.Text = "OK";
        this.buttonOk.UseVisualStyleBackColor = true;
        this.buttonCancel.DialogResult = DialogResult.Cancel;
        this.buttonCancel.Location = new Point(265, 182);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new Size(95, 25);
        this.buttonCancel.TabIndex = 6;
        this.buttonCancel.Text = "Отмена";
        this.buttonCancel.UseVisualStyleBackColor = true;
        this.panelRadioButtons.Controls.Add((Control) this.radioButtonAll);
        this.panelRadioButtons.Controls.Add((Control) this.radioButtonPartly);
        this.panelRadioButtons.Controls.Add((Control) this.radioButtonNothing);
        this.panelRadioButtons.Location = new Point(13, 84);
        this.panelRadioButtons.Name = "panelRadioButtons";
        this.panelRadioButtons.Size = new Size(323, 68);
        this.panelRadioButtons.TabIndex = 7;
        this.AcceptButton = (IButtonControl) this.buttonOk;
        this.AutoScaleDimensions = new SizeF(6f, 13f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.CancelButton = (IButtonControl) this.buttonCancel;
        this.ClientSize = new Size(372, 219);
        this.Controls.Add((Control) this.panelRadioButtons);
        this.Controls.Add((Control) this.buttonCancel);
        this.Controls.Add((Control) this.buttonOk);
        this.Controls.Add((Control) this.iconWarning);
        this.Controls.Add((Control) this.labelInfo);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Name = nameof (DeleteFilesDialog);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Удаление из рабочей области";
        ((ISupportInitialize) this.iconWarning).EndInit();
        this.panelRadioButtons.ResumeLayout(false);
        this.panelRadioButtons.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
      }

      public enum Actions
      {
        DeleteAll,
        DeletePartly,
        DeleteNothing,
      }
    }
}

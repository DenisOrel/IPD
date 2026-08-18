
// Type: Intermech.PdfPrintCenter.Utils.AddNodesToLayoutDialog




using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes;
using Intermech.PdfPrintCenter.Utils.UtilMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.Utils
{
    internal class AddNodesToLayoutDialog : Form
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

      public AddNodesToLayoutDialog(
        Dictionary<string, List<NodesToPrintQueue>> nodesToFilename,
        bool allNotOnMinLayout)
      {
        this.InitializeComponent();
        this.InitializeWarningIcon();
        this.InitializeInfo(nodesToFilename);
        this.InitializeRadioPanel(allNotOnMinLayout);
        this.InitializeButtons();
        this.SetNewHeight();
      }

      public AddNodesToLayoutDialog.Actions Action { get; private set; }

      private void InitializeButtons()
      {
        int num = 25;
        int bottomYcoordinate = this.panelRadioButtons.GetBottomYCoordinate();
        this.buttonCancel.SetYCoordinate(bottomYcoordinate + num);
        this.buttonOk.SetYCoordinate(bottomYcoordinate + num);
        this.buttonOk.Enabled = false;
      }

      private void InitializeInfo(
        Dictionary<string, List<NodesToPrintQueue>> nodesToFilename)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Для следующих наборов страниц не удалось найти макет с минимальной основой:");
        foreach (string key in nodesToFilename.Keys)
        {
          stringBuilder.Append($"- файл {key}.pdf: ");
          foreach (NodesToPrintQueue nodesToPrintQueue in nodesToFilename[key])
            stringBuilder.Append($"макет \"{nodesToPrintQueue.PrintParameters.Layout.Caption}\", страницы {string.Join(", ", nodesToPrintQueue.Nodes.Select<WorkspacePagesTreeNode, string>((Func<WorkspacePagesTreeNode, string>) (node => node.Pages)))}; ");
          stringBuilder.AppendLine();
        }
        stringBuilder.AppendLine("Какое действие предпринять?");
        this.labelInfo.Text = stringBuilder.ToString();
      }

      private void InitializeRadioPanel(bool allNotOnMinLayout)
      {
        this.panelRadioButtons.SetYCoordinate(Math.Max(this.iconWarning.GetBottomYCoordinate(), this.labelInfo.GetBottomYCoordinate()) + 17);
        if (!allNotOnMinLayout)
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
          this.Action = !this.radioButtonAll.Checked ? (!this.radioButtonPartly.Checked ? AddNodesToLayoutDialog.Actions.AddNothing : AddNodesToLayoutDialog.Actions.AddPartly) : AddNodesToLayoutDialog.Actions.AddAll;
        else if (this.DialogResult == DialogResult.Cancel)
          this.Action = AddNodesToLayoutDialog.Actions.AddNothing;
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
        this.radioButtonAll.Size = new Size(181, 17);
        this.radioButtonAll.TabIndex = 2;
        this.radioButtonAll.TabStop = true;
        this.radioButtonAll.Text = "Добавить все наборы страниц";
        this.radioButtonAll.UseVisualStyleBackColor = true;
        this.radioButtonAll.CheckedChanged += new EventHandler(this.RadioButton_CheckedChanged);
        this.radioButtonPartly.AutoSize = true;
        this.radioButtonPartly.Location = new Point(3, 26);
        this.radioButtonPartly.Name = "radioButtonPartly";
        this.radioButtonPartly.Size = new Size(332, 17);
        this.radioButtonPartly.TabIndex = 3;
        this.radioButtonPartly.TabStop = true;
        this.radioButtonPartly.Text = "Добавить только наборы страниц с минимальным макетом";
        this.radioButtonPartly.UseVisualStyleBackColor = true;
        this.radioButtonPartly.CheckedChanged += new EventHandler(this.RadioButton_CheckedChanged);
        this.radioButtonNothing.AutoSize = true;
        this.radioButtonNothing.Location = new Point(3, 49);
        this.radioButtonNothing.Name = "radioButtonNothing";
        this.radioButtonNothing.Size = new Size(132, 17);
        this.radioButtonNothing.TabIndex = 4;
        this.radioButtonNothing.TabStop = true;
        this.radioButtonNothing.Text = "Ничего не добавлять";
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
        this.panelRadioButtons.Size = new Size(346, 68);
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
        this.Name = nameof (AddNodesToLayoutDialog);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Добавление в очередь печати";
        ((ISupportInitialize) this.iconWarning).EndInit();
        this.panelRadioButtons.ResumeLayout(false);
        this.panelRadioButtons.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
      }

      public enum Actions
      {
        AddAll,
        AddPartly,
        AddNothing,
      }
    }
}


// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.DocumentPrintSettings.EditDocumentPrintSettingsForm




using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes;
using Intermech.PdfPrintCenter.Utils;
using Intermech.PdfPrintCenter.Utils.UtilMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.DocumentPrintSettings
{
    internal class EditDocumentPrintSettingsForm : Form
    {
      private List<LayoutDescriptor> _layouts;
      private IList<string> _printersOrderList;
      private List<PagePrintSettings> _initialSettings = new List<PagePrintSettings>();
      private IContainer components;
      private Label labelObjectName;
      private TextBox textBoxObjectName;
      private CheckBox checkBoxIgnoreCopiesCheck;
      private Button buttonCancel;
      private TableLayoutPanel tableLayoutPanelSettings;
      private Label labelPrinter;
      private Label labelLayout;
      private Label Pages;
      private Label labelCopies;
      private TableLayoutPanel tableLayoutPanelLabels;
      private Label labelRowNumber;
      private Button buttonOk;
      private Label labelFitToPage;

      public EditDocumentPrintSettingsForm() => this.InitializeComponent();

      public EditDocumentPrintSettingsForm(
        List<PrintQueuePagesNode> nodes,
        List<LayoutDescriptor> layouts,
        IList<string> printersOrderList)
      {
        this.InitializeComponent();
        this.InitializeLayoutsList(layouts);
        this.InitializePrintersOrderList(printersOrderList);
        this.InitializeTextBoxObjectName(nodes.First<PrintQueuePagesNode>().FileName);
        this.InitializeTableLayoutPanelLabels(nodes.Count);
        this.InitializeTableLayoutPanelSettings(nodes);
        this.InitializeIgnoreCheckBox(nodes.First<PrintQueuePagesNode>().IgnoreDifferentCopies);
        this.InitializeUpDownCopies(this.IsDifferentCopies(nodes));
        this.InitializePrintSettings(nodes);
      }

      public List<PagePrintSettings> PagesPrintSettings { get; private set; }

      private void InitializeIgnoreCheckBox(bool differentCopiesIgnored)
      {
        this.checkBoxIgnoreCopiesCheck.Checked = differentCopiesIgnored;
      }

      private void InitializeLayoutsList(List<LayoutDescriptor> layouts) => this._layouts = layouts;

      private void InitializePrintersOrderList(IList<string> printersOrderList)
      {
        this._printersOrderList = printersOrderList;
      }

      private void InitializePrintSettings(List<PrintQueuePagesNode> nodes)
      {
        this.PagesPrintSettings = new List<PagePrintSettings>();
        for (int index = 0; index < this.tableLayoutPanelSettings.RowCount; ++index)
        {
          string currentPrinterName = this.GetCurrentPrinterName(index);
          IPdfPageProducer currentLayout = this.GetCurrentLayout(index);
          this.PagesPrintSettings.Add(new PagePrintSettings(currentPrinterName, currentLayout, nodes[index]));
          this._initialSettings.Add(new PagePrintSettings(currentPrinterName, currentLayout, new PrintQueuePagesNode(nodes[index])));
        }
      }

      private void InitializeTableLayoutPanelLabels(int nodesCount)
      {
        if (nodesCount <= this.tableLayoutPanelSettings.RowCount)
          return;
        this.tableLayoutPanelLabels.Width -= SystemInformation.VerticalScrollBarWidth;
      }

      private void InitializeTableLayoutPanelSettings(List<PrintQueuePagesNode> nodes)
      {
        int rowCount = this.tableLayoutPanelSettings.RowCount;
        if (nodes.Count > rowCount)
        {
          for (int index = 0; index < nodes.Count - rowCount; ++index)
          {
            ++this.tableLayoutPanelSettings.RowCount;
            this.tableLayoutPanelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
          }
        }
        else if (nodes.Count < rowCount)
        {
          for (int index = 0; index < rowCount - nodes.Count; ++index)
            --this.tableLayoutPanelSettings.RowCount;
        }
        int num = 1;
        foreach (PrintQueuePagesNode node in nodes)
        {
          Label label = new Label();
          label.Text = num++.ToString() + ".";
          label.TextAlign = ContentAlignment.MiddleCenter;
          this.AddToTableAndSetWidth((Control) label);
          this.AddToTableAndSetWidth((Control) this.CreateComboBoxPrinters((node.Parent.Parent as PrinterNode).PrinterName));
          this.AddToTableAndSetWidth((Control) this.CreateComboBoxLayouts((node.Parent as LayoutNode).LayoutName));
          this.AddToTableAndSetWidth((Control) this.CreateTextBoxPages(node.Pages));
          this.tableLayoutPanelSettings.Controls.Add((Control) this.CreateUpDownCopies(short.Parse(node.Copies)));
          this.tableLayoutPanelSettings.Controls.Add((Control) this.CreateCheckBoxFit(node.FitToPage));
        }
      }

      private void InitializeTextBoxObjectName(string objectName)
      {
        this.textBoxObjectName.Text = objectName;
      }

      private void InitializeUpDownCopies(bool isDifferentCopies)
      {
        if (!isDifferentCopies || this.checkBoxIgnoreCopiesCheck.Checked)
          return;
        foreach (Control control in this.tableLayoutPanelSettings.Controls.OfType<NumericUpDown>())
          control.BackColor = Color.LightCoral;
      }

      protected override void OnFormClosing(FormClosingEventArgs e)
      {
        if (this.SettingsChanged())
        {
          if (this.DialogResult == DialogResult.OK)
          {
            Dictionary<string, List<int>> wrongLayouts = this.GetWrongLayouts();
            if (wrongLayouts.Any<KeyValuePair<string, List<int>>>())
            {
              int num = (int) MessageBox.Show(this.CreateErrorMessage(wrongLayouts), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
              e.Cancel = true;
              return;
            }
          }
          else if (this.DialogResult == DialogResult.Cancel)
          {
            switch (MessageBox.Show("Сохранить изменения перед выходом?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk))
            {
              case DialogResult.Cancel:
                e.Cancel = true;
                return;
              case DialogResult.Yes:
                Dictionary<string, List<int>> wrongLayouts1 = this.GetWrongLayouts();
                if (wrongLayouts1.Any<KeyValuePair<string, List<int>>>())
                {
                  int num = (int) MessageBox.Show(this.CreateErrorMessage(wrongLayouts1), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                  e.Cancel = true;
                  return;
                }
                this.DialogResult = DialogResult.OK;
                break;
            }
          }
        }
        base.OnFormClosing(e);
      }

      private void CheckBoxIgnoreCopiesCheck_CheckedChanged(object sender, EventArgs e)
      {
        this.CheckUpDownControlsColor();
      }

      private void UpDownCopies_ValueChanged(object sender, EventArgs e)
      {
        this.CheckUpDownControlsColor();
      }

      private void AddToTableAndSetWidth(Control control)
      {
        this.tableLayoutPanelSettings.Controls.Add(control);
        control.Width = control.Parent.Width;
      }

      private void CheckUpDownControlsColor()
      {
        List<NumericUpDown> list = this.tableLayoutPanelSettings.Controls.OfType<NumericUpDown>().ToList<NumericUpDown>();
        foreach (Control control in list)
          control.BackColor = !this.IsDifferentCopies(list) || this.checkBoxIgnoreCopiesCheck.Checked ? Color.White : Color.LightCoral;
      }

      private ReadOnlyComboBox CreateComboBoxLayouts(string selectedLayoutName)
      {
        ReadOnlyComboBox comboBoxLayouts = new ReadOnlyComboBox();
        comboBoxLayouts.DropDownStyle = ComboBoxStyle.DropDown;
        ControlUtils.LoadLayouts((ComboBox) comboBoxLayouts, this._layouts);
        comboBoxLayouts.SelectedIndex = comboBoxLayouts.IndexByName(selectedLayoutName);
        return comboBoxLayouts;
      }

      private ReadOnlyComboBox CreateComboBoxPrinters(string selectedPrinterName)
      {
        ReadOnlyComboBox comboBoxPrinters = new ReadOnlyComboBox();
        comboBoxPrinters.DropDownStyle = ComboBoxStyle.DropDown;
        ControlUtils.LoadPrinters((ComboBox) comboBoxPrinters, this._printersOrderList);
        comboBoxPrinters.SelectedIndex = comboBoxPrinters.Items.IndexOf((object) selectedPrinterName);
        return comboBoxPrinters;
      }

      private string CreateErrorMessage(Dictionary<string, List<int>> wrongLayouts)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Изменения не могут быть применены, так как некоторые наборы страниц не могут быть расположены на выбранных макетах:");
        foreach (string key in wrongLayouts.Keys)
        {
          string str1 = string.Join<int>(", ", (IEnumerable<int>) wrongLayouts[key]);
          string str2 = wrongLayouts[key].Count == 1 ? "набор страниц №" : "наборы страниц №";
          stringBuilder.AppendLine($"- макет \"{key}\": {str2} {str1};");
        }
        return stringBuilder.ToString();
      }

      private TextBox CreateTextBoxPages(string pages)
      {
        TextBox textBoxPages = new TextBox();
        textBoxPages.Text = pages;
        textBoxPages.ReadOnly = true;
        return textBoxPages;
      }

      private NumericUpDown CreateUpDownCopies(short copies)
      {
        NumericUpDown upDownCopies = new NumericUpDown();
        upDownCopies.Minimum = 1M;
        upDownCopies.Value = (Decimal) copies;
        upDownCopies.ValueChanged += new EventHandler(this.UpDownCopies_ValueChanged);
        return upDownCopies;
      }

      private CheckBox CreateCheckBoxFit(bool fitToPage)
      {
        return new CheckBox()
        {
          Checked = fitToPage,
          CheckAlign = ContentAlignment.MiddleCenter
        };
      }

      private string GetCurrentPrinterName(int row)
      {
        return this.tableLayoutPanelSettings.GetControlFromPosition(1, row) is ReadOnlyComboBox controlFromPosition ? (string) controlFromPosition.SelectedItem : (string) (object) null;
      }

      private IPdfPageProducer GetCurrentLayout(int row)
      {
        return (this.tableLayoutPanelSettings.GetControlFromPosition(2, row) is ReadOnlyComboBox controlFromPosition ? controlFromPosition.SelectedItem : (object) null) as IPdfPageProducer;
      }

      private string GetCurrentCopies(int row)
      {
        return !(this.tableLayoutPanelSettings.GetControlFromPosition(4, row) is NumericUpDown controlFromPosition) ? (string) null : controlFromPosition.Value.ToString();
      }

      private bool GetCurrentFit(int row)
      {
        return (this.tableLayoutPanelSettings.GetControlFromPosition(5, row) as CheckBox).Checked;
      }

      private Dictionary<string, List<int>> GetWrongLayouts()
      {
        Dictionary<string, List<int>> wrongLayouts = new Dictionary<string, List<int>>();
        for (int index = 0; index < this.PagesPrintSettings.Count; ++index)
        {
          IPdfPageProducer layout = this.PagesPrintSettings[index].Layout;
          PrintQueuePagesNode node = this.PagesPrintSettings[index].Node;
          if (layout is LayoutDescriptor layoutDescriptor && !layoutDescriptor.CanDistributePage(new SizeF((float) node.PageSize.MmWidth, (float) node.PageSize.MmHeight)))
          {
            if (!wrongLayouts.ContainsKey(layoutDescriptor.Caption))
              wrongLayouts.Add(layoutDescriptor.Caption, new List<int>());
            wrongLayouts[layoutDescriptor.Caption].Add(index + 1);
          }
        }
        return wrongLayouts;
      }

      private bool IsDifferentCopies(List<PrintQueuePagesNode> nodes)
      {
        return nodes.Any<PrintQueuePagesNode>((Func<PrintQueuePagesNode, bool>) (node => node.Copies != nodes.First<PrintQueuePagesNode>().Copies));
      }

      private bool IsDifferentCopies(List<NumericUpDown> upDownControls)
      {
        return upDownControls.Any<NumericUpDown>((Func<NumericUpDown, bool>) (item => item.Value != upDownControls.First<NumericUpDown>().Value));
      }

      private bool SettingsChanged()
      {
        this.UpdateSettings();
        return !this._initialSettings.SequenceEqual<PagePrintSettings>((IEnumerable<PagePrintSettings>) this.PagesPrintSettings);
      }

      private void UpdateSettings()
      {
        for (int index = 0; index < this.tableLayoutPanelSettings.RowCount; ++index)
        {
          this.PagesPrintSettings[index].PrinterName = this.GetCurrentPrinterName(index);
          this.PagesPrintSettings[index].Layout = this.GetCurrentLayout(index);
          this.PagesPrintSettings[index].Node.Copies = this.GetCurrentCopies(index);
          this.PagesPrintSettings[index].Node.IgnoreDifferentCopies = this.checkBoxIgnoreCopiesCheck.Checked;
          this.PagesPrintSettings[index].Node.FitToPage = this.GetCurrentFit(index);
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
        this.labelObjectName = new Label();
        this.textBoxObjectName = new TextBox();
        this.checkBoxIgnoreCopiesCheck = new CheckBox();
        this.buttonCancel = new Button();
        this.tableLayoutPanelSettings = new TableLayoutPanel();
        this.labelPrinter = new Label();
        this.labelLayout = new Label();
        this.Pages = new Label();
        this.labelCopies = new Label();
        this.tableLayoutPanelLabels = new TableLayoutPanel();
        this.labelFitToPage = new Label();
        this.labelRowNumber = new Label();
        this.buttonOk = new Button();
        this.tableLayoutPanelLabels.SuspendLayout();
        this.SuspendLayout();
        this.labelObjectName.AutoSize = true;
        this.labelObjectName.Location = new Point(17, 11);
        this.labelObjectName.Name = "labelObjectName";
        this.labelObjectName.Size = new Size(58, 13);
        this.labelObjectName.TabIndex = 0;
        this.labelObjectName.Text = "Документ";
        this.textBoxObjectName.BackColor = SystemColors.ControlLightLight;
        this.textBoxObjectName.Location = new Point(16 /*0x10*/, 30);
        this.textBoxObjectName.Multiline = true;
        this.textBoxObjectName.Name = "textBoxObjectName";
        this.textBoxObjectName.ReadOnly = true;
        this.textBoxObjectName.ScrollBars = ScrollBars.Vertical;
        this.textBoxObjectName.Size = new Size(582, 49);
        this.textBoxObjectName.TabIndex = 1;
        this.checkBoxIgnoreCopiesCheck.AutoSize = true;
        this.checkBoxIgnoreCopiesCheck.Location = new Point(16 /*0x10*/, 380);
        this.checkBoxIgnoreCopiesCheck.Name = "checkBoxIgnoreCopiesCheck";
        this.checkBoxIgnoreCopiesCheck.Size = new Size(290, 17);
        this.checkBoxIgnoreCopiesCheck.TabIndex = 4;
        this.checkBoxIgnoreCopiesCheck.Text = "Игнорировать проверку копий для этого документа";
        this.checkBoxIgnoreCopiesCheck.UseVisualStyleBackColor = true;
        this.checkBoxIgnoreCopiesCheck.CheckedChanged += new EventHandler(this.CheckBoxIgnoreCopiesCheck_CheckedChanged);
        this.buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        this.buttonCancel.DialogResult = DialogResult.Cancel;
        this.buttonCancel.Location = new Point(503, 405);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new Size(95, 25);
        this.buttonCancel.TabIndex = 6;
        this.buttonCancel.Text = "Отмена";
        this.buttonCancel.UseVisualStyleBackColor = true;
        this.tableLayoutPanelSettings.AutoScroll = true;
        this.tableLayoutPanelSettings.ColumnCount = 6;
        this.tableLayoutPanelSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5f));
        this.tableLayoutPanelSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27f));
        this.tableLayoutPanelSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27f));
        this.tableLayoutPanelSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23f));
        this.tableLayoutPanelSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8f));
        this.tableLayoutPanelSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
        this.tableLayoutPanelSettings.Location = new Point(16 /*0x10*/, 116);
        this.tableLayoutPanelSettings.Name = "tableLayoutPanelSettings";
        this.tableLayoutPanelSettings.RowCount = 10;
        this.tableLayoutPanelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
        this.tableLayoutPanelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
        this.tableLayoutPanelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
        this.tableLayoutPanelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
        this.tableLayoutPanelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
        this.tableLayoutPanelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
        this.tableLayoutPanelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
        this.tableLayoutPanelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
        this.tableLayoutPanelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
        this.tableLayoutPanelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
        this.tableLayoutPanelSettings.Size = new Size(582, 251);
        this.tableLayoutPanelSettings.TabIndex = 3;
        this.labelPrinter.AutoSize = true;
        this.labelPrinter.Location = new Point(32 /*0x20*/, 0);
        this.labelPrinter.Name = "labelPrinter";
        this.labelPrinter.Size = new Size(50, 13);
        this.labelPrinter.TabIndex = 1;
        this.labelPrinter.Text = "Принтер";
        this.labelPrinter.TextAlign = ContentAlignment.MiddleCenter;
        this.labelLayout.AutoSize = true;
        this.labelLayout.Location = new Point(189, 0);
        this.labelLayout.Name = "labelLayout";
        this.labelLayout.Size = new Size(39, 13);
        this.labelLayout.TabIndex = 2;
        this.labelLayout.Text = "Макет";
        this.labelLayout.TextAlign = ContentAlignment.MiddleCenter;
        this.Pages.AutoSize = true;
        this.Pages.Location = new Point(346, 0);
        this.Pages.Name = "Pages";
        this.Pages.Size = new Size(57, 13);
        this.Pages.TabIndex = 3;
        this.Pages.Text = "Страницы";
        this.Pages.TextAlign = ContentAlignment.MiddleCenter;
        this.labelCopies.AutoSize = true;
        this.labelCopies.Location = new Point(479, 0);
        this.labelCopies.Name = "labelCopies";
        this.labelCopies.Size = new Size(38, 13);
        this.labelCopies.TabIndex = 4;
        this.labelCopies.Text = "Копии";
        this.labelCopies.TextAlign = ContentAlignment.MiddleCenter;
        this.tableLayoutPanelLabels.ColumnCount = 6;
        this.tableLayoutPanelLabels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5f));
        this.tableLayoutPanelLabels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27f));
        this.tableLayoutPanelLabels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27f));
        this.tableLayoutPanelLabels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23f));
        this.tableLayoutPanelLabels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8f));
        this.tableLayoutPanelLabels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
        this.tableLayoutPanelLabels.Controls.Add((Control) this.labelFitToPage, 5, 0);
        this.tableLayoutPanelLabels.Controls.Add((Control) this.labelPrinter, 1, 0);
        this.tableLayoutPanelLabels.Controls.Add((Control) this.labelCopies, 4, 0);
        this.tableLayoutPanelLabels.Controls.Add((Control) this.labelLayout, 2, 0);
        this.tableLayoutPanelLabels.Controls.Add((Control) this.Pages, 3, 0);
        this.tableLayoutPanelLabels.Controls.Add((Control) this.labelRowNumber, 0, 0);
        this.tableLayoutPanelLabels.Location = new Point(16 /*0x10*/, 100);
        this.tableLayoutPanelLabels.Name = "tableLayoutPanelLabels";
        this.tableLayoutPanelLabels.RowCount = 1;
        this.tableLayoutPanelLabels.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
        this.tableLayoutPanelLabels.Size = new Size(582, 14);
        this.tableLayoutPanelLabels.TabIndex = 2;
        this.labelFitToPage.AutoSize = true;
        this.labelFitToPage.Location = new Point(525, 0);
        this.labelFitToPage.Name = "labelFitToPage";
        this.labelFitToPage.Size = new Size(49, 13);
        this.labelFitToPage.TabIndex = 5;
        this.labelFitToPage.Text = "Вписать";
        this.labelRowNumber.AutoSize = true;
        this.labelRowNumber.Location = new Point(3, 0);
        this.labelRowNumber.Name = "labelRowNumber";
        this.labelRowNumber.Size = new Size(18, 13);
        this.labelRowNumber.TabIndex = 0;
        this.labelRowNumber.Text = "№";
        this.labelRowNumber.TextAlign = ContentAlignment.MiddleCenter;
        this.buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        this.buttonOk.DialogResult = DialogResult.OK;
        this.buttonOk.Location = new Point(402, 405);
        this.buttonOk.Name = "buttonOk";
        this.buttonOk.Size = new Size(95, 25);
        this.buttonOk.TabIndex = 5;
        this.buttonOk.Text = "OK";
        this.buttonOk.UseVisualStyleBackColor = true;
        this.AcceptButton = (IButtonControl) this.buttonOk;
        this.AutoScaleDimensions = new SizeF(6f, 13f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.CancelButton = (IButtonControl) this.buttonCancel;
        this.ClientSize = new Size(610, 442);
        this.Controls.Add((Control) this.buttonOk);
        this.Controls.Add((Control) this.tableLayoutPanelLabels);
        this.Controls.Add((Control) this.tableLayoutPanelSettings);
        this.Controls.Add((Control) this.buttonCancel);
        this.Controls.Add((Control) this.checkBoxIgnoreCopiesCheck);
        this.Controls.Add((Control) this.textBoxObjectName);
        this.Controls.Add((Control) this.labelObjectName);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Name = nameof (EditDocumentPrintSettingsForm);
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Настройка печати документа";
        this.tableLayoutPanelLabels.ResumeLayout(false);
        this.tableLayoutPanelLabels.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
      }

      private enum PrintSettingsColumns
      {
        RowNumberColumn,
        PrinterColumn,
        LayoutColumn,
        PagesColumn,
        CopiesColumn,
        FitToPageColumn,
      }
    }
}


// Type: Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettingsForm




using Intermech.PdfPrintCenter.Interfaces;
using Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings
{
    internal class PrintersSettingsForm : Form
    {
      private IPrintersSettingsService printersSettingsService;
      private Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings _initialPrintersSettings;
      private IDictionary<string, List<string>> _formatsToPrinters = (IDictionary<string, List<string>>) new Dictionary<string, List<string>>();
      private string _selectedPrinter;
      private IContainer components;
      private ListBox listBoxPrinters;
      private CheckedListBox checkedListBoxFormats;
      private Label labelPrinters;
      private Label labelFormats;
      private Button buttonCancel;
      private Button buttonOk;
      private Button buttonPrinterUp;
      private Button buttonPrinterDown;
      private Button buttonResetPrintersOrder;
      private Button buttonResetFormatsSettings;
      private Button buttonRefreshPrintersList;

      public PrintersSettingsForm()
      {
        this.InitializeComponent();
        this.InitializeFormSettings();
        this.CheckUpDownButtons();
      }

      [Inject]
      public PrintersSettingsForm(IPrintersSettingsService printersSettingsService)
      {
        this.InitializeComponent();
        this.InitializeServices(printersSettingsService);
        this.InitializeFormSettings();
        this.CheckUpDownButtons();
      }

      private void InitializeFormSettings()
      {
        this.InitializePrintersSettings();
        this.InitializeFormatsToPrinters();
        this.InitializeFormatsList();
        this.InitializePrintersList();
      }

      private void InitializeFormatsList()
      {
        foreach (KnownPaperFormat knownPaperFormat in KnownPaperFormats.Formats.Where<KnownPaperFormat>((Func<KnownPaperFormat, bool>) (format => format.IsPortait)))
          this.checkedListBoxFormats.Items.Add((object) knownPaperFormat.BaseName);
        this.checkedListBoxFormats.Enabled = false;
      }

      private void InitializeFormatsToPrinters()
      {
        this._formatsToPrinters = (IDictionary<string, List<string>>) new Dictionary<string, List<string>>(this._initialPrintersSettings.FormatsToPrinters);
      }

      private void InitializePrintersList()
      {
        foreach (object obj in (IEnumerable<string>) this._initialPrintersSettings.PrintersOrder)
          this.listBoxPrinters.Items.Add(obj);
      }

      private void InitializePrintersSettings()
      {
        this._initialPrintersSettings = this.printersSettingsService.GetPrintersSettings().Clone() as Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings;
      }

      private void InitializeServices(IPrintersSettingsService printersSettingsService)
      {
        this.printersSettingsService = printersSettingsService;
      }

      protected override void OnFormClosing(FormClosingEventArgs e)
      {
        if (this.DialogResult == DialogResult.OK)
          this.SaveSettings();
        else if (this.DialogResult == DialogResult.Cancel)
        {
          this.AddNewSettings();
          if (this.IsSettingsChanged())
          {
            switch (MessageBox.Show("Сохранить изменения перед выходом?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk))
            {
              case DialogResult.Cancel:
                e.Cancel = true;
                return;
              case DialogResult.Yes:
                this.SaveSettings();
                this.DialogResult = DialogResult.OK;
                break;
            }
          }
        }
        base.OnFormClosing(e);
      }

      private void ButtonPrinterUp_Click(object sender, EventArgs e)
      {
        int selectedIndex = this.listBoxPrinters.SelectedIndex;
        if (selectedIndex <= 0 || selectedIndex >= this.listBoxPrinters.Items.Count)
          return;
        this.SwapElements(selectedIndex, selectedIndex - 1);
        this.listBoxPrinters.SelectedIndex = selectedIndex - 1;
      }

      private void ButtonPrinterDown_Click(object sender, EventArgs e)
      {
        int selectedIndex = this.listBoxPrinters.SelectedIndex;
        if (selectedIndex < 0 && selectedIndex >= this.listBoxPrinters.Items.Count - 1)
          return;
        this.SwapElements(selectedIndex, selectedIndex + 1);
        this.listBoxPrinters.SelectedIndex = selectedIndex + 1;
      }

      private void ButtonRefreshPrintersList_Click(object sender, EventArgs e)
      {
        Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings printersSettings = this.printersSettingsService.GetDefaultPrintersSettings();
        IDictionary<string, List<string>> formatsToPrinters = printersSettings.FormatsToPrinters;
        foreach (string key in printersSettings.PrintersOrder.Except<string>((IEnumerable<string>) this.listBoxPrinters.Items.OfType<string>().ToList<string>()))
        {
          this.listBoxPrinters.Items.Add((object) key);
          this._formatsToPrinters.Add(key, formatsToPrinters[key]);
        }
      }

      private void ButtonResetFormatsSettings_Click(object sender, EventArgs e)
      {
        this._formatsToPrinters = (this.printersSettingsService.GetDefaultPrintersSettings().Clone() as Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings).FormatsToPrinters;
        this.ReloadCheckedListBox();
      }

      private void ButtonResetPrintersOrder_Click(object sender, EventArgs e)
      {
        IList<string> printersOrder = this.printersSettingsService.GetDefaultPrintersSettings().PrintersOrder;
        this.listBoxPrinters.Items.Clear();
        foreach (object obj in (IEnumerable<string>) printersOrder)
          this.listBoxPrinters.Items.Add(obj);
        this.CheckUpDownButtons();
      }

      private void ListBoxPrinters_SelectedIndexChanged(object sender, EventArgs e)
      {
        this.AddNewSettings();
        this.ReloadCheckedListBox();
        this.CheckUpDownButtons();
      }

      private void AddNewSettings()
      {
        if (this._selectedPrinter == null)
          return;
        this._formatsToPrinters.Remove(this._selectedPrinter);
        this._formatsToPrinters.Add(this._selectedPrinter, this.checkedListBoxFormats.CheckedItems.OfType<string>().ToList<string>());
      }

      private void CheckUpDownButtons()
      {
        this.buttonPrinterUp.Enabled = this.listBoxPrinters.SelectedIndex > 0 && this.listBoxPrinters.SelectedIndex < this.listBoxPrinters.Items.Count;
        this.buttonPrinterDown.Enabled = this.listBoxPrinters.SelectedIndex >= 0 && this.listBoxPrinters.SelectedIndex < this.listBoxPrinters.Items.Count - 1;
      }

      private string CreateSettingsElement(string printerName, IEnumerable<string> formats)
      {
        return $"{printerName}:{string.Join(",", (IEnumerable<string>) formats.OrderBy<string, string>((Func<string, string>) (format => format)))}";
      }

      private List<string> FindFormatsToPrinter(string printerName)
      {
        return this._formatsToPrinters.ContainsKey(printerName) ? this._formatsToPrinters[printerName] : (List<string>) null;
      }

      private Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings GetCurrentPrintersSettings()
      {
        return new Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings()
        {
          FormatsToPrinters = this._formatsToPrinters,
          PrintersOrder = (IList<string>) this.listBoxPrinters.Items.OfType<string>().ToList<string>()
        };
      }

      private bool IsSettingsChanged()
      {
        return !this._initialPrintersSettings.Equals((object) this.GetCurrentPrintersSettings());
      }

      private void LoadPrinterSettings(string printerName)
      {
        List<string> formatsToPrinter = this.FindFormatsToPrinter(printerName);
        if (formatsToPrinter == null)
          return;
        foreach (string s in formatsToPrinter)
          this.checkedListBoxFormats.SetItemChecked(this.checkedListBoxFormats.FindString(s), true);
      }

      private void ReloadCheckedListBox()
      {
        this.checkedListBoxFormats.ClearSelected();
        for (int index = 0; index < this.checkedListBoxFormats.Items.Count; ++index)
          this.checkedListBoxFormats.SetItemChecked(index, false);
        if (this.listBoxPrinters.SelectedIndex == -1)
        {
          this._selectedPrinter = (string) null;
          this.checkedListBoxFormats.Enabled = false;
        }
        else
        {
          this.checkedListBoxFormats.Enabled = true;
          string selectedItem = this.listBoxPrinters.SelectedItem as string;
          this.LoadPrinterSettings(selectedItem);
          this._selectedPrinter = selectedItem;
        }
      }

      private void SaveSettings()
      {
        this.AddNewSettings();
        Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings printersSettings = this.GetCurrentPrintersSettings();
        printersSettings.Freeze();
        this.printersSettingsService.PutPrintersSettings(printersSettings);
      }

      private void SwapElements(int firstIndex, int secondIndex)
      {
        if (firstIndex == secondIndex)
          return;
        object obj = this.listBoxPrinters.Items[firstIndex];
        this.listBoxPrinters.Items[firstIndex] = this.listBoxPrinters.Items[secondIndex];
        this.listBoxPrinters.Items[secondIndex] = obj;
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        this.listBoxPrinters = new ListBox();
        this.checkedListBoxFormats = new CheckedListBox();
        this.labelPrinters = new Label();
        this.labelFormats = new Label();
        this.buttonCancel = new Button();
        this.buttonOk = new Button();
        this.buttonPrinterUp = new Button();
        this.buttonPrinterDown = new Button();
        this.buttonResetPrintersOrder = new Button();
        this.buttonResetFormatsSettings = new Button();
        this.buttonRefreshPrintersList = new Button();
        this.SuspendLayout();
        this.listBoxPrinters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        this.listBoxPrinters.FormattingEnabled = true;
        this.listBoxPrinters.Location = new Point(12, 25);
        this.listBoxPrinters.Name = "listBoxPrinters";
        this.listBoxPrinters.Size = new Size(233, 303);
        this.listBoxPrinters.TabIndex = 1;
        this.listBoxPrinters.SelectedIndexChanged += new EventHandler(this.ListBoxPrinters_SelectedIndexChanged);
        this.checkedListBoxFormats.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.checkedListBoxFormats.CheckOnClick = true;
        this.checkedListBoxFormats.FormattingEnabled = true;
        this.checkedListBoxFormats.Location = new Point(251, 25);
        this.checkedListBoxFormats.Name = "checkedListBoxFormats";
        this.checkedListBoxFormats.Size = new Size(233, 304);
        this.checkedListBoxFormats.TabIndex = 3;
        this.labelPrinters.AutoSize = true;
        this.labelPrinters.Location = new Point(9, 9);
        this.labelPrinters.Name = "labelPrinters";
        this.labelPrinters.Size = new Size(61, 13);
        this.labelPrinters.TabIndex = 0;
        this.labelPrinters.Text = "Принтеры:";
        this.labelFormats.AutoSize = true;
        this.labelFormats.Location = new Point(248, 9);
        this.labelFormats.Name = "labelFormats";
        this.labelFormats.Size = new Size(60, 13);
        this.labelFormats.TabIndex = 2;
        this.labelFormats.Text = "Форматы:";
        this.buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        this.buttonCancel.DialogResult = DialogResult.Cancel;
        this.buttonCancel.Location = new Point(522, 348);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new Size(95, 25);
        this.buttonCancel.TabIndex = 10;
        this.buttonCancel.Text = "Отмена";
        this.buttonCancel.UseVisualStyleBackColor = true;
        this.buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        this.buttonOk.DialogResult = DialogResult.OK;
        this.buttonOk.Location = new Point(421, 348);
        this.buttonOk.Name = "buttonOk";
        this.buttonOk.Size = new Size(95, 25);
        this.buttonOk.TabIndex = 9;
        this.buttonOk.Text = "ОК";
        this.buttonOk.UseVisualStyleBackColor = true;
        this.buttonPrinterUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.buttonPrinterUp.Location = new Point(490, 25);
        this.buttonPrinterUp.Name = "buttonPrinterUp";
        this.buttonPrinterUp.Size = new Size((int) sbyte.MaxValue, 40);
        this.buttonPrinterUp.TabIndex = 4;
        this.buttonPrinterUp.Text = "↑";
        this.buttonPrinterUp.UseVisualStyleBackColor = true;
        this.buttonPrinterUp.Click += new EventHandler(this.ButtonPrinterUp_Click);
        this.buttonPrinterDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.buttonPrinterDown.Location = new Point(490, 71);
        this.buttonPrinterDown.Name = "buttonPrinterDown";
        this.buttonPrinterDown.Size = new Size((int) sbyte.MaxValue, 40);
        this.buttonPrinterDown.TabIndex = 5;
        this.buttonPrinterDown.Text = "↓";
        this.buttonPrinterDown.UseVisualStyleBackColor = true;
        this.buttonPrinterDown.Click += new EventHandler(this.ButtonPrinterDown_Click);
        this.buttonResetPrintersOrder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.buttonResetPrintersOrder.Location = new Point(490, 172);
        this.buttonResetPrintersOrder.Name = "buttonResetPrintersOrder";
        this.buttonResetPrintersOrder.Size = new Size((int) sbyte.MaxValue, 49);
        this.buttonResetPrintersOrder.TabIndex = 7;
        this.buttonResetPrintersOrder.Text = "Сбросить порядок принтеров";
        this.buttonResetPrintersOrder.UseVisualStyleBackColor = true;
        this.buttonResetPrintersOrder.Click += new EventHandler(this.ButtonResetPrintersOrder_Click);
        this.buttonResetFormatsSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.buttonResetFormatsSettings.Location = new Point(490, 227);
        this.buttonResetFormatsSettings.Name = "buttonResetFormatsSettings";
        this.buttonResetFormatsSettings.Size = new Size((int) sbyte.MaxValue, 49);
        this.buttonResetFormatsSettings.TabIndex = 8;
        this.buttonResetFormatsSettings.Text = "Сбросить настройки форматов";
        this.buttonResetFormatsSettings.UseVisualStyleBackColor = true;
        this.buttonResetFormatsSettings.Click += new EventHandler(this.ButtonResetFormatsSettings_Click);
        this.buttonRefreshPrintersList.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.buttonRefreshPrintersList.Location = new Point(490, 117);
        this.buttonRefreshPrintersList.Name = "buttonRefreshPrintersList";
        this.buttonRefreshPrintersList.Size = new Size((int) sbyte.MaxValue, 49);
        this.buttonRefreshPrintersList.TabIndex = 6;
        this.buttonRefreshPrintersList.Text = "Обновить список принтеров";
        this.buttonRefreshPrintersList.UseVisualStyleBackColor = true;
        this.buttonRefreshPrintersList.Click += new EventHandler(this.ButtonRefreshPrintersList_Click);
        this.AcceptButton = (IButtonControl) this.buttonOk;
        this.AutoScaleDimensions = new SizeF(6f, 13f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.CancelButton = (IButtonControl) this.buttonCancel;
        this.ClientSize = new Size(626, 385);
        this.Controls.Add((Control) this.buttonRefreshPrintersList);
        this.Controls.Add((Control) this.buttonResetFormatsSettings);
        this.Controls.Add((Control) this.buttonResetPrintersOrder);
        this.Controls.Add((Control) this.buttonPrinterDown);
        this.Controls.Add((Control) this.buttonPrinterUp);
        this.Controls.Add((Control) this.buttonOk);
        this.Controls.Add((Control) this.buttonCancel);
        this.Controls.Add((Control) this.labelFormats);
        this.Controls.Add((Control) this.labelPrinters);
        this.Controls.Add((Control) this.checkedListBoxFormats);
        this.Controls.Add((Control) this.listBoxPrinters);
        this.MinimumSize = new Size(642, 424);
        this.Name = nameof (PrintersSettingsForm);
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Настройка принтеров";
        this.ResumeLayout(false);
        this.PerformLayout();
      }
    }
}

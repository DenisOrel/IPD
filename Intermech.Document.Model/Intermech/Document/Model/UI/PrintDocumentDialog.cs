// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.PrintDocumentDialog
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Printing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Runtime.ExceptionServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#nullable disable
namespace Intermech.Document.Model.UI;

public class PrintDocumentDialog : Form
{
  private VisualStyleState oldVisualStyleState;
  private ImDocumentData document;
  private PrinterSettings settings;
  private PrintDocument printDocument;
  private bool ControlsUpdating;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label lStatus;
  private Label label10;
  private Label lDriverName;
  private Label lComment;
  private Label label9;
  private Label lPortName;
  private Label label11;
  private Label label8;
  private System.Windows.Forms.Button bCancel;
  private System.Windows.Forms.Button bPrint;
  private System.Windows.Forms.Button bOptions;
  private Label label7;
  private System.Windows.Forms.ComboBox cbPrinter;
  private Label label6;
  private NumericUpDown nCopies;
  private Label label4;
  private System.Windows.Forms.GroupBox groupBox1;
  private System.Windows.Forms.RadioButton rbSome;
  private System.Windows.Forms.RadioButton rbAll;
  private System.Windows.Forms.CheckBox cbCollate;
  private PictureBox pictureBox1;
  private System.Windows.Forms.ToolTip toolTip1;
  private System.Windows.Forms.TextBox tbPages;
  private System.Windows.Forms.CheckBox cbFitToPage;
  private System.Windows.Forms.Button bShiftPage;
  private System.Windows.Forms.RadioButton rbCurrent;

  public PrintDocumentDialog()
  {
    this.oldVisualStyleState = Application.VisualStyleState;
    this.InitializeComponent();
  }

  public PrintDocumentDialog(PrintDocument printDocument, ImDocumentData imDoc)
  {
    this.oldVisualStyleState = Application.VisualStyleState;
    this.InitializeComponent();
    imDoc.ImPrintSettings.FitToPagePrint = new bool?();
    this.Document = imDoc;
    this.PrintDocument = printDocument;
    ImDocumentEditorConfig.Instance.LoadDocumentPrintersSettings(true);
  }

  public ImDocumentData Document
  {
    get => this.document;
    set => this.document = value;
  }

  private PrinterSettings Settings
  {
    get => this.settings;
    set
    {
      try
      {
        this.settings = value;
        this.UpdateControls();
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
      }
    }
  }

  /// <summary>Инициализировать настройки принтера</summary>
  /// <remarks>Инициализация нужна чтобы обойти ошибку некоторых драйверов принтера, которые выбрасывают AccessViolationException</remarks>
  private void InitPrinterSettings(PrinterSettings settings)
  {
    settings.Copies = (short) 1;
    settings.Collate = true;
    settings.Duplex = Duplex.Simplex;
  }

  public PrintDocument PrintDocument
  {
    get => this.printDocument;
    set
    {
      try
      {
        this.printDocument = value;
        if (this.printDocument != null)
          this.Settings = this.printDocument.PrinterSettings;
        else
          this.Settings = new PrinterSettings();
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
      }
    }
  }

  [HandleProcessCorruptedStateExceptions]
  private void UpdateControls()
  {
    if (this.settings == null)
      return;
    this.ControlsUpdating = true;
    try
    {
      if (this.Document != null)
        this.cbFitToPage.Checked = !this.Document.ImPrintSettings.FitToPagePrint.HasValue ? this.Document.FitToPage : this.Document.ImPrintSettings.FitToPagePrint.Value;
      switch (this.settings.PrintRange)
      {
        case PrintRange.AllPages:
          this.rbAll.Checked = true;
          break;
        case PrintRange.SomePages:
          this.rbSome.Checked = true;
          break;
        case PrintRange.CurrentPage:
          this.rbCurrent.Checked = true;
          break;
      }
      this.tbPages.Enabled = !this.rbAll.Checked;
      if (this.settings.ToPage < this.settings.FromPage)
        this.settings.ToPage = this.settings.FromPage;
      this.nCopies.Maximum = (Decimal) this.settings.MaximumCopies;
      this.nCopies.Minimum = this.settings.MaximumCopies <= 0 ? 0M : 1M;
      if ((int) this.settings.Copies > this.settings.MaximumCopies)
        this.settings.Copies = (short) this.settings.MaximumCopies;
      if (this.settings.Copies == (short) 0)
        this.settings.Copies = (short) 1;
      this.nCopies.Value = (Decimal) this.settings.Copies;
      this.cbCollate.Checked = this.settings.Collate;
      if (this.cbPrinter.Items.Count == 0)
      {
        PrinterSettings.StringCollection installedPrinters = PrinterSettings.InstalledPrinters;
        this.cbPrinter.Items.Clear();
        foreach (string str in installedPrinters)
          this.cbPrinter.Items.Add((object) str);
      }
      this.cbPrinter.SelectedItem = (object) this.settings.PrinterName;
      this.bShiftPage.Visible = ImDocumentEditorConfig.Instance.IsClientPluginConfig;
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      this.ControlsUpdating = false;
    }
  }

  private void bPrint_Click(object sender, EventArgs e)
  {
    try
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void nCopies_ValueChanged(object sender, EventArgs e)
  {
    if (this.ControlsUpdating || !(this.nCopies.Value > 0M))
      return;
    this.settings.Copies = (short) this.nCopies.Value;
  }

  private void rbSome_CheckedChanged(object sender, EventArgs e)
  {
    try
    {
      if (this.ControlsUpdating)
        return;
      if (this.rbAll.Checked)
        this.settings.PrintRange = PrintRange.AllPages;
      if (this.rbSome.Checked)
        this.settings.PrintRange = PrintRange.SomePages;
      if (this.rbCurrent.Checked)
        this.settings.PrintRange = PrintRange.CurrentPage;
      this.UpdateControls();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void bOptions_Click(object sender, EventArgs e)
  {
    try
    {
      PrintComplectDialog.OpenPrinterPropertiesDialog(this.settings, this.Handle);
      this.UpdateControls();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  [HandleProcessCorruptedStateExceptions]
  private void cbPrinter_SelectedIndexChanged(object sender, EventArgs e)
  {
    try
    {
      this.settings.PrinterName = this.cbPrinter.Text;
      SafePrinterHandle safePrinterHandle = new SafePrinterHandle(this.settings.PrinterName);
      try
      {
        if (safePrinterHandle.PrinterInfo2 != null)
        {
          if (safePrinterHandle.PrinterInfo2.Status != null)
            this.lStatus.Text = safePrinterHandle.PrinterInfo2.Status.ToString();
          this.lPortName.Text = safePrinterHandle.PrinterInfo2.PortName;
          this.lDriverName.Text = safePrinterHandle.PrinterInfo2.DriverName;
          this.lComment.Text = safePrinterHandle.PrinterInfo2.Comment;
        }
        else
        {
          this.lStatus.Text = "Error";
          this.lPortName.Text = "Error";
          this.lDriverName.Text = "Error";
          this.lComment.Text = "Error";
        }
      }
      finally
      {
        safePrinterHandle.Close();
      }
      this.UpdateControls();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void nFirst_ValueChanged(object sender, EventArgs e)
  {
    try
    {
      if (this.ControlsUpdating || this.settings.PrintRange != PrintRange.SomePages)
        return;
      this.UpdateControls();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void cbCollate_CheckedChanged(object sender, EventArgs e)
  {
    if (this.ControlsUpdating)
      return;
    this.settings.Collate = this.cbCollate.Checked;
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    DocumentMenuHelper.SilentRecoverVisualStyle(this.oldVisualStyleState);
    if (this.Document != null && this.DialogResult != DialogResult.Cancel)
    {
      this.Document.ImPrintSettings.SelectedPrintPages.Clear();
      if (this.rbSome.Checked)
      {
        List<int> pagesForPrint = PrintComplectDialog.GetPagesForPrint(PageNumberingHelper.GetPageNumbersForPrinting(this.Document, this.tbPages.Text));
        if (pagesForPrint.Count == 0)
        {
          int num = (int) MessageBox.Show("Неправильно задан список страниц для печати", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          e.Cancel = true;
        }
        else
          this.Document.ImPrintSettings.SelectedPrintPages.AddRange((IEnumerable<int>) pagesForPrint);
      }
    }
    base.OnClosing(e);
    if (e.Cancel)
      return;
    PrintComplectDialog.RefreshProperties(this.settings, this.Handle);
    this.document = (ImDocumentData) null;
    this.printDocument = (PrintDocument) null;
    this.settings = (PrinterSettings) null;
  }

  private void cbFitToPage_CheckedChanged(object sender, EventArgs e)
  {
    if (this.ControlsUpdating || this.Document == null)
      return;
    this.Document.ImPrintSettings.FitToPagePrint = new bool?(this.cbFitToPage.Checked);
  }

  private void bShiftPage_Click(object sender, EventArgs e)
  {
    int num = (int) ShiftPageForPrinter.Execute(this.settings.PrinterName);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    this.Document = (ImDocumentData) null;
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PrintDocumentDialog));
    this.lStatus = new Label();
    this.label10 = new Label();
    this.lDriverName = new Label();
    this.lComment = new Label();
    this.label9 = new Label();
    this.lPortName = new Label();
    this.label11 = new Label();
    this.label8 = new Label();
    this.bCancel = new System.Windows.Forms.Button();
    this.bPrint = new System.Windows.Forms.Button();
    this.bOptions = new System.Windows.Forms.Button();
    this.label7 = new Label();
    this.cbPrinter = new System.Windows.Forms.ComboBox();
    this.label6 = new Label();
    this.nCopies = new NumericUpDown();
    this.label4 = new Label();
    this.groupBox1 = new System.Windows.Forms.GroupBox();
    this.pictureBox1 = new PictureBox();
    this.tbPages = new System.Windows.Forms.TextBox();
    this.rbSome = new System.Windows.Forms.RadioButton();
    this.rbAll = new System.Windows.Forms.RadioButton();
    this.cbCollate = new System.Windows.Forms.CheckBox();
    this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
    this.cbFitToPage = new System.Windows.Forms.CheckBox();
    this.bShiftPage = new System.Windows.Forms.Button();
    this.rbCurrent = new System.Windows.Forms.RadioButton();
    this.nCopies.BeginInit();
    this.groupBox1.SuspendLayout();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.lStatus, "lStatus");
    this.lStatus.Name = "lStatus";
    componentResourceManager.ApplyResources((object) this.label10, "label10");
    this.label10.Name = "label10";
    componentResourceManager.ApplyResources((object) this.lDriverName, "lDriverName");
    this.lDriverName.Name = "lDriverName";
    componentResourceManager.ApplyResources((object) this.lComment, "lComment");
    this.lComment.Name = "lComment";
    componentResourceManager.ApplyResources((object) this.label9, "label9");
    this.label9.Name = "label9";
    componentResourceManager.ApplyResources((object) this.lPortName, "lPortName");
    this.lPortName.Name = "lPortName";
    componentResourceManager.ApplyResources((object) this.label11, "label11");
    this.label11.Name = "label11";
    componentResourceManager.ApplyResources((object) this.label8, "label8");
    this.label8.Name = "label8";
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Name = "bCancel";
    this.bCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.bPrint, "bPrint");
    this.bPrint.DialogResult = DialogResult.OK;
    this.bPrint.Name = "bPrint";
    this.bPrint.UseVisualStyleBackColor = true;
    this.bPrint.Click += new EventHandler(this.bPrint_Click);
    componentResourceManager.ApplyResources((object) this.bOptions, "bOptions");
    this.bOptions.Name = "bOptions";
    this.bOptions.UseVisualStyleBackColor = true;
    this.bOptions.Click += new EventHandler(this.bOptions_Click);
    componentResourceManager.ApplyResources((object) this.label7, "label7");
    this.label7.Name = "label7";
    this.cbPrinter.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.cbPrinter, "cbPrinter");
    this.cbPrinter.Name = "cbPrinter";
    this.cbPrinter.SelectedIndexChanged += new EventHandler(this.cbPrinter_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    componentResourceManager.ApplyResources((object) this.nCopies, "nCopies");
    this.nCopies.Maximum = new Decimal(new int[4]
    {
      1410065408,
      2,
      0,
      0
    });
    this.nCopies.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.nCopies.Name = "nCopies";
    this.nCopies.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.nCopies.ValueChanged += new EventHandler(this.nCopies_ValueChanged);
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Controls.Add((Control) this.rbCurrent);
    this.groupBox1.Controls.Add((Control) this.pictureBox1);
    this.groupBox1.Controls.Add((Control) this.tbPages);
    this.groupBox1.Controls.Add((Control) this.rbSome);
    this.groupBox1.Controls.Add((Control) this.rbAll);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    this.toolTip1.SetToolTip((Control) this.pictureBox1, componentResourceManager.GetString("pictureBox1.ToolTip"));
    componentResourceManager.ApplyResources((object) this.tbPages, "tbPages");
    this.tbPages.Name = "tbPages";
    componentResourceManager.ApplyResources((object) this.rbSome, "rbSome");
    this.rbSome.Name = "rbSome";
    this.rbSome.TabStop = true;
    this.rbSome.UseVisualStyleBackColor = true;
    this.rbSome.CheckedChanged += new EventHandler(this.rbSome_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbAll, "rbAll");
    this.rbAll.Name = "rbAll";
    this.rbAll.TabStop = true;
    this.rbAll.UseVisualStyleBackColor = true;
    this.rbAll.CheckedChanged += new EventHandler(this.rbSome_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbCollate, "cbCollate");
    this.cbCollate.Name = "cbCollate";
    this.cbCollate.UseVisualStyleBackColor = true;
    this.cbCollate.CheckedChanged += new EventHandler(this.cbCollate_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbFitToPage, "cbFitToPage");
    this.cbFitToPage.Name = "cbFitToPage";
    this.cbFitToPage.UseVisualStyleBackColor = true;
    this.cbFitToPage.CheckedChanged += new EventHandler(this.cbFitToPage_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.bShiftPage, "bShiftPage");
    this.bShiftPage.Name = "bShiftPage";
    this.bShiftPage.UseVisualStyleBackColor = true;
    this.bShiftPage.Click += new EventHandler(this.bShiftPage_Click);
    componentResourceManager.ApplyResources((object) this.rbCurrent, "rbCurrent");
    this.rbCurrent.Name = "rbCurrent";
    this.rbCurrent.TabStop = true;
    this.rbCurrent.UseVisualStyleBackColor = true;
    this.rbCurrent.CheckedChanged += new EventHandler(this.rbSome_CheckedChanged);
    this.AcceptButton = (IButtonControl) this.bPrint;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.cbFitToPage);
    this.Controls.Add((Control) this.cbCollate);
    this.Controls.Add((Control) this.lStatus);
    this.Controls.Add((Control) this.label10);
    this.Controls.Add((Control) this.lDriverName);
    this.Controls.Add((Control) this.lComment);
    this.Controls.Add((Control) this.label9);
    this.Controls.Add((Control) this.lPortName);
    this.Controls.Add((Control) this.label11);
    this.Controls.Add((Control) this.label8);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.bPrint);
    this.Controls.Add((Control) this.bShiftPage);
    this.Controls.Add((Control) this.bOptions);
    this.Controls.Add((Control) this.label7);
    this.Controls.Add((Control) this.cbPrinter);
    this.Controls.Add((Control) this.label6);
    this.Controls.Add((Control) this.nCopies);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.groupBox1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (PrintDocumentDialog);
    this.nCopies.EndInit();
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}


// Type: Intermech.Controls.PrintDlg
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Printing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Controls;

public class PrintDlg : Form
{
  private PrinterSettings settings;
  private PrintDocument doc;
  private bool ControlsUpdating;
  private const int DM_OUT_BUFFER = 14;
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
  private Button bCancel;
  private Button bPrint;
  private Button bOptions;
  private Label label7;
  private ComboBox cbPrinter;
  private Label label6;
  private NumericUpDown nCopies;
  private Label label4;
  private GroupBox groupBox1;
  private NumericUpDown nLast;
  private NumericUpDown nFirst;
  private Label label3;
  private Label label2;
  private RadioButton rbSome;
  private RadioButton rbAll;

  public PrintDlg() => this.InitializeComponent();

  public PrintDlg(PrintDocument printDocument)
  {
    this.InitializeComponent();
    this.PrintDocument = printDocument;
  }

  public PrinterSettings Settings
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

  public PrintDocument PrintDocument
  {
    get => this.doc;
    set
    {
      try
      {
        this.doc = value;
        if (this.doc != null)
          this.Settings = this.doc.PrinterSettings;
        else
          this.Settings = new PrinterSettings();
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
      }
    }
  }

  private void UpdateControls()
  {
    this.ControlsUpdating = true;
    try
    {
      if (this.PrintDocument == null)
      {
        this.nFirst.Enabled = false;
        this.nLast.Enabled = false;
        this.rbAll.Enabled = false;
        this.rbSome.Enabled = false;
        this.nCopies.Enabled = false;
        this.cbPrinter.Enabled = false;
      }
      else
      {
        this.nFirst.Minimum = (Decimal) this.settings.MinimumPage;
        this.nFirst.Maximum = (Decimal) this.settings.MaximumPage;
        this.nLast.Minimum = (Decimal) this.settings.FromPage;
        this.nLast.Maximum = (Decimal) this.settings.MaximumPage;
        switch (this.settings.PrintRange)
        {
          case PrintRange.AllPages:
            this.rbAll.Checked = true;
            break;
          case PrintRange.SomePages:
            this.rbSome.Checked = true;
            break;
        }
        if (this.rbAll.Checked)
        {
          this.nFirst.Enabled = false;
          this.nLast.Enabled = false;
        }
        else
        {
          this.nFirst.Enabled = true;
          this.nLast.Enabled = true;
        }
        this.nFirst.Value = (Decimal) this.settings.FromPage;
        if (this.settings.ToPage < this.settings.FromPage)
          this.settings.ToPage = this.settings.FromPage;
        this.nLast.Value = (Decimal) this.settings.ToPage;
        this.nCopies.Maximum = (Decimal) this.settings.MaximumCopies;
        this.nCopies.Minimum = this.settings.MaximumCopies <= 0 ? 0M : 1M;
        if ((int) this.settings.Copies > this.settings.MaximumCopies)
          this.settings.Copies = (short) this.settings.MaximumCopies;
        this.nCopies.Value = (Decimal) this.settings.Copies;
        if (this.cbPrinter.Items.Count == 0)
        {
          PrinterSettings.StringCollection installedPrinters = PrinterSettings.InstalledPrinters;
          this.cbPrinter.Items.Clear();
          foreach (string str in installedPrinters)
            this.cbPrinter.Items.Add((object) str);
        }
        this.cbPrinter.SelectedItem = (object) this.settings.PrinterName;
      }
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
      this.UpdateControls();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  public static void OpenPrinterPropertiesDialog(PrinterSettings printerSettings, IntPtr pHandle)
  {
    IntPtr hdevmode = printerSettings.GetHdevmode();
    IntPtr num1 = PrintDlg.GlobalLock(hdevmode);
    int fMode1 = 0;
    IntPtr num2 = Marshal.AllocHGlobal(PrintDlg.DocumentProperties(pHandle, IntPtr.Zero, printerSettings.PrinterName, num1, num1, fMode1));
    int fMode2 = 14;
    PrintDlg.DocumentProperties(pHandle, IntPtr.Zero, printerSettings.PrinterName, num2, num1, fMode2);
    PrintDlg.GlobalUnlock(hdevmode);
    printerSettings.SetHdevmode(num2);
    printerSettings.DefaultPageSettings.SetHdevmode(num2);
    PrintDlg.GlobalFree(hdevmode);
    Marshal.FreeHGlobal(num2);
  }

  [DllImport("kernel32.dll")]
  private static extern IntPtr GlobalLock(IntPtr hMem);

  [DllImport("kernel32.dll")]
  private static extern bool GlobalUnlock(IntPtr hMem);

  [DllImport("kernel32.dll")]
  private static extern bool GlobalFree(IntPtr hMem);

  [DllImport("winspool.Drv", EntryPoint = "DocumentPropertiesW", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  private static extern int DocumentProperties(
    IntPtr hwnd,
    IntPtr hPrinter,
    [MarshalAs(UnmanagedType.LPWStr)] string pDeviceName,
    IntPtr pDevModeOutput,
    IntPtr pDevModeInput,
    int fMode);

  private void bOptions_Click(object sender, EventArgs e)
  {
    try
    {
      PrintDlg.OpenPrinterPropertiesDialog(this.settings, this.Handle);
      this.UpdateControls();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

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
      if (sender == this.nLast)
        this.settings.ToPage = (int) this.nLast.Value;
      if (sender == this.nFirst)
        this.settings.FromPage = (int) this.nFirst.Value;
      this.UpdateControls();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.lStatus = new Label();
    this.label10 = new Label();
    this.lDriverName = new Label();
    this.lComment = new Label();
    this.label9 = new Label();
    this.lPortName = new Label();
    this.label11 = new Label();
    this.label8 = new Label();
    this.bCancel = new Button();
    this.bPrint = new Button();
    this.bOptions = new Button();
    this.label7 = new Label();
    this.cbPrinter = new ComboBox();
    this.label6 = new Label();
    this.nCopies = new NumericUpDown();
    this.label4 = new Label();
    this.groupBox1 = new GroupBox();
    this.nLast = new NumericUpDown();
    this.nFirst = new NumericUpDown();
    this.label3 = new Label();
    this.label2 = new Label();
    this.rbSome = new RadioButton();
    this.rbAll = new RadioButton();
    this.nCopies.BeginInit();
    this.groupBox1.SuspendLayout();
    this.nLast.BeginInit();
    this.nFirst.BeginInit();
    this.SuspendLayout();
    this.lStatus.AutoSize = true;
    this.lStatus.Location = new Point(98, 59);
    this.lStatus.Name = "lStatus";
    this.lStatus.Size = new Size(35, 13);
    this.lStatus.TabIndex = 27;
    this.lStatus.Text = "label8";
    this.label10.AutoSize = true;
    this.label10.Location = new Point(12, 59);
    this.label10.Name = "label10";
    this.label10.Size = new Size(61, 13);
    this.label10.TabIndex = 28;
    this.label10.Text = "Состояние";
    this.lDriverName.AutoSize = true;
    this.lDriverName.Location = new Point(98, 79);
    this.lDriverName.Name = "lDriverName";
    this.lDriverName.Size = new Size(35, 13);
    this.lDriverName.TabIndex = 25;
    this.lDriverName.Text = "label8";
    this.lComment.AutoSize = true;
    this.lComment.Location = new Point(98, 119);
    this.lComment.Name = "lComment";
    this.lComment.Size = new Size(35, 13);
    this.lComment.TabIndex = 26;
    this.lComment.Text = "label8";
    this.label9.AutoSize = true;
    this.label9.Location = new Point(12, 79);
    this.label9.Name = "label9";
    this.label9.Size = new Size(26, 13);
    this.label9.TabIndex = 31 /*0x1F*/;
    this.label9.Text = "Тип";
    this.lPortName.AutoSize = true;
    this.lPortName.Location = new Point(98, 99);
    this.lPortName.Name = "lPortName";
    this.lPortName.Size = new Size(35, 13);
    this.lPortName.TabIndex = 32 /*0x20*/;
    this.lPortName.Text = "label8";
    this.label11.AutoSize = true;
    this.label11.Location = new Point(12, 119);
    this.label11.Name = "label11";
    this.label11.Size = new Size(80 /*0x50*/, 13);
    this.label11.TabIndex = 29;
    this.label11.Text = "Комментарий:";
    this.label8.AutoSize = true;
    this.label8.Location = new Point(12, 99);
    this.label8.Name = "label8";
    this.label8.Size = new Size(39, 13);
    this.label8.TabIndex = 30;
    this.label8.Text = "Место";
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(308, 241);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(75, 23);
    this.bCancel.TabIndex = 24;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bPrint.DialogResult = DialogResult.OK;
    this.bPrint.Location = new Point(213, 241);
    this.bPrint.Name = "bPrint";
    this.bPrint.Size = new Size(75, 23);
    this.bPrint.TabIndex = 23;
    this.bPrint.Text = "Печать";
    this.bPrint.UseVisualStyleBackColor = true;
    this.bPrint.Click += new EventHandler(this.bPrint_Click);
    this.bOptions.Location = new Point(310, 25);
    this.bOptions.Name = "bOptions";
    this.bOptions.Size = new Size(75, 23);
    this.bOptions.TabIndex = 22;
    this.bOptions.Text = "Свойства...";
    this.bOptions.UseVisualStyleBackColor = true;
    this.bOptions.Click += new EventHandler(this.bOptions_Click);
    this.label7.AutoSize = true;
    this.label7.Location = new Point(12, 9);
    this.label7.Name = "label7";
    this.label7.Size = new Size(50, 13);
    this.label7.TabIndex = 21;
    this.label7.Text = "Принтер";
    this.cbPrinter.FormattingEnabled = true;
    this.cbPrinter.Location = new Point(15, 25);
    this.cbPrinter.Name = "cbPrinter";
    this.cbPrinter.Size = new Size(289, 21);
    this.cbPrinter.TabIndex = 20;
    this.cbPrinter.SelectedIndexChanged += new EventHandler(this.cbPrinter_SelectedIndexChanged);
    this.label6.AutoSize = true;
    this.label6.Location = new Point(12, 143);
    this.label6.Name = "label6";
    this.label6.Size = new Size(43, 13);
    this.label6.TabIndex = 19;
    this.label6.Text = "Печать";
    this.nCopies.Location = new Point(329, 163);
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
    this.nCopies.Size = new Size(54, 20);
    this.nCopies.TabIndex = 17;
    this.nCopies.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.nCopies.ValueChanged += new EventHandler(this.nCopies_ValueChanged);
    this.label4.AutoSize = true;
    this.label4.Location = new Point(251, 165);
    this.label4.Name = "label4";
    this.label4.Size = new Size(72, 13);
    this.label4.TabIndex = 16 /*0x10*/;
    this.label4.Text = "Число копий";
    this.groupBox1.Controls.Add((Control) this.nLast);
    this.groupBox1.Controls.Add((Control) this.nFirst);
    this.groupBox1.Controls.Add((Control) this.label3);
    this.groupBox1.Controls.Add((Control) this.label2);
    this.groupBox1.Controls.Add((Control) this.rbSome);
    this.groupBox1.Controls.Add((Control) this.rbAll);
    this.groupBox1.Location = new Point(15, 159);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(200, 74);
    this.groupBox1.TabIndex = 15;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Диапазон печати";
    this.nLast.Location = new Point(152, 42);
    this.nLast.Name = "nLast";
    this.nLast.Size = new Size(40, 20);
    this.nLast.TabIndex = 5;
    this.nLast.ValueChanged += new EventHandler(this.nFirst_ValueChanged);
    this.nFirst.Location = new Point(90, 42);
    this.nFirst.Name = "nFirst";
    this.nFirst.Size = new Size(40, 20);
    this.nFirst.TabIndex = 4;
    this.nFirst.ValueChanged += new EventHandler(this.nFirst_ValueChanged);
    this.label3.AutoSize = true;
    this.label3.Location = new Point(132, 44);
    this.label3.Name = "label3";
    this.label3.Size = new Size(19, 13);
    this.label3.TabIndex = 3;
    this.label3.Text = "по";
    this.label2.AutoSize = true;
    this.label2.Location = new Point(78, 44);
    this.label2.Name = "label2";
    this.label2.Size = new Size(13, 13);
    this.label2.TabIndex = 2;
    this.label2.Text = "с";
    this.rbSome.AutoSize = true;
    this.rbSome.Location = new Point(6, 42);
    this.rbSome.Name = "rbSome";
    this.rbSome.Size = new Size(75, 17);
    this.rbSome.TabIndex = 1;
    this.rbSome.TabStop = true;
    this.rbSome.Text = "Страницы";
    this.rbSome.UseVisualStyleBackColor = true;
    this.rbSome.CheckedChanged += new EventHandler(this.rbSome_CheckedChanged);
    this.rbAll.AutoSize = true;
    this.rbAll.Location = new Point(6, 19);
    this.rbAll.Name = "rbAll";
    this.rbAll.Size = new Size(44, 17);
    this.rbAll.TabIndex = 0;
    this.rbAll.TabStop = true;
    this.rbAll.Text = "Все";
    this.rbAll.UseVisualStyleBackColor = true;
    this.rbAll.CheckedChanged += new EventHandler(this.rbSome_CheckedChanged);
    this.AcceptButton = (IButtonControl) this.bPrint;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(394, 271);
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
    this.Name = "PrintDocumentDialog";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Печать";
    this.nCopies.EndInit();
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.nLast.EndInit();
    this.nFirst.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}

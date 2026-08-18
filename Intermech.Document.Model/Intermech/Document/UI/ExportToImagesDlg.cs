// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.ExportToImagesDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Summary description for ExportToImagesDlg.</summary>
public class ExportToImagesDlg : Form
{
  private Button btnOK;
  private Button btnCancel;
  private Label label1;
  private TextBox tbFilename;
  private Button btnBrowse;
  private GroupBox gbPages;
  private RadioButton rbAllPages;
  private RadioButton rbNumbers;
  private TextBox tbPageNumbers;
  private Label label2;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  /// <summary>Конструктор</summary>
  public ExportToImagesDlg() => this.InitializeComponent();

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExportToImagesDlg));
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.label1 = new Label();
    this.tbFilename = new TextBox();
    this.btnBrowse = new Button();
    this.gbPages = new GroupBox();
    this.label2 = new Label();
    this.tbPageNumbers = new TextBox();
    this.rbNumbers = new RadioButton();
    this.rbAllPages = new RadioButton();
    this.gbPages.SuspendLayout();
    this.SuspendLayout();
    this.btnOK.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.Name = "btnOK";
    this.btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.tbFilename, "tbFilename");
    this.tbFilename.Name = "tbFilename";
    componentResourceManager.ApplyResources((object) this.btnBrowse, "btnBrowse");
    this.btnBrowse.Name = "btnBrowse";
    this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);
    this.gbPages.Controls.Add((Control) this.label2);
    this.gbPages.Controls.Add((Control) this.tbPageNumbers);
    this.gbPages.Controls.Add((Control) this.rbNumbers);
    this.gbPages.Controls.Add((Control) this.rbAllPages);
    componentResourceManager.ApplyResources((object) this.gbPages, "gbPages");
    this.gbPages.Name = "gbPages";
    this.gbPages.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.tbPageNumbers, "tbPageNumbers");
    this.tbPageNumbers.Name = "tbPageNumbers";
    this.tbPageNumbers.TextChanged += new EventHandler(this.tbPageNumbers_TextChanged);
    componentResourceManager.ApplyResources((object) this.rbNumbers, "rbNumbers");
    this.rbNumbers.Name = "rbNumbers";
    this.rbAllPages.Checked = true;
    componentResourceManager.ApplyResources((object) this.rbAllPages, "rbAllPages");
    this.rbAllPages.Name = "rbAllPages";
    this.rbAllPages.TabStop = true;
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.gbPages);
    this.Controls.Add((Control) this.btnBrowse);
    this.Controls.Add((Control) this.tbFilename);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ExportToImagesDlg);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.gbPages.ResumeLayout(false);
    this.gbPages.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>
  /// Разобрать строку с номерами страниц и перечислить их в массиве
  /// </summary>
  /// <param name="pageCount">Общее количество страниц документа</param>
  /// <param name="pageNumbers">Перечисление страниц в строке</param>
  /// <returns>Массив номеров страниц</returns>
  public static int[] ExtractPageNambers(int pageCount, string pageNumbers)
  {
    pageNumbers = pageNumbers.Trim();
    string[] strArray = pageNumbers.Split(',');
    ArrayList arrayList = new ArrayList(pageCount);
    for (int index1 = 0; index1 < strArray.Length; ++index1)
    {
      int length = strArray[index1].IndexOf("-");
      int intValue1;
      int intValue2;
      switch (length)
      {
        case -1:
          int intValue3;
          if (!NumberParserAdvanced.TryParseInt32(strArray[index1], out intValue3))
            throw new Exception(LocalizationHolder.rm.GetString("Document.Model_51"));
          arrayList.Add((object) (intValue3 - 1));
          continue;
        case 0:
          intValue1 = 1;
          if (!NumberParserAdvanced.TryParseInt32(strArray[index1].Substring(1), out intValue2))
            throw new Exception(LocalizationHolder.rm.GetString("Document.Model_52"));
          break;
        default:
          if (length == strArray[index1].Length - 1)
          {
            intValue2 = pageCount;
            if (!NumberParserAdvanced.TryParseInt32(strArray[index1].Substring(0, strArray[index1].Length - 1), out intValue1))
              throw new Exception(LocalizationHolder.rm.GetString("Document.Model_53"));
            break;
          }
          if (!NumberParserAdvanced.TryParseInt32(strArray[index1].Substring(0, length), out intValue1))
            throw new Exception(LocalizationHolder.rm.GetString("Document.Model_54"));
          if (!NumberParserAdvanced.TryParseInt32(strArray[index1].Substring(length + 1), out intValue2))
            throw new Exception(LocalizationHolder.rm.GetString("Document.Model_55"));
          break;
      }
      for (int index2 = intValue1; index2 <= intValue2; ++index2)
        arrayList.Add((object) (index2 - 1));
    }
    return (int[]) arrayList.ToArray(typeof (int));
  }

  /// <summary>Выполнить диалог</summary>
  /// <param name="pageCount">Количество страниц в документе</param>
  /// <param name="pages">Результат выбора страниц</param>
  /// <param name="fileName">База имени файлов в которые нужно экспортировать изображения страниц</param>
  /// <returns>Результат выполнения диалога</returns>
  public DialogResult ExecuteDlg(int pageCount, out int[] pages, ref string fileName)
  {
    this.tbFilename.Text = fileName;
    int num = (int) this.ShowDialog();
    pages = (int[]) null;
    if (num != 1)
      return (DialogResult) num;
    if (this.rbNumbers.Checked)
      pages = ExportToImagesDlg.ExtractPageNambers(pageCount, this.tbPageNumbers.Text);
    fileName = this.tbFilename.Text;
    return (DialogResult) num;
  }

  private void btnBrowse_Click(object sender, EventArgs e)
  {
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    saveFileDialog.FileName = this.tbFilename.Text;
    saveFileDialog.Filter = "Windows Metafile (*.wmf)|*.wmf";
    saveFileDialog.InitialDirectory = "\".\"";
    saveFileDialog.RestoreDirectory = true;
    if (saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    this.tbFilename.Text = saveFileDialog.FileName;
  }

  /// <summary>Создать и выполнить диалог</summary>
  /// <param name="pageCount">Количество страниц в документе</param>
  /// <param name="pages">Результат выбора страниц</param>
  /// <param name="fileName">База имени файлов в которые нужно экспортировать изображения страниц</param>
  /// <returns>Результат выполнения диалога</returns>
  public static DialogResult Execute(int pageCount, out int[] pages, ref string fileName)
  {
    return new ExportToImagesDlg().ExecuteDlg(pageCount, out pages, ref fileName);
  }

  private void tbPageNumbers_TextChanged(object sender, EventArgs e)
  {
    this.rbNumbers.Checked = this.tbPageNumbers.Text != "";
    this.rbAllPages.Checked = !this.rbNumbers.Checked;
  }
}

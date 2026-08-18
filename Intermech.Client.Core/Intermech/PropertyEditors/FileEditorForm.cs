
// Type: Intermech.PropertyEditors.FileEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for FileEditorForm.</summary>
public class FileEditorForm : Form
{
  private RadioButton saveRB;
  private RadioButton viewRB;
  private RadioButton loadRB;
  private RadioButton clearRB;
  private Button buttonDo;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private OpenFileDialog loadFileDialog;
  private SaveFileDialog saveFileDialog;
  private FilePropertyClass filePropertyClass;

  public FileEditorForm()
  {
    this.InitializeComponent();
    this.TopLevel = false;
    this.Dock = DockStyle.Fill;
    this.FormBorderStyle = FormBorderStyle.None;
  }

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FileEditorForm));
    this.buttonDo = new Button();
    this.saveRB = new RadioButton();
    this.viewRB = new RadioButton();
    this.loadRB = new RadioButton();
    this.clearRB = new RadioButton();
    this.loadFileDialog = new OpenFileDialog();
    this.saveFileDialog = new SaveFileDialog();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.buttonDo, "buttonDo");
    this.buttonDo.Name = "buttonDo";
    this.buttonDo.Click += new EventHandler(this.buttonDo_Click);
    componentResourceManager.ApplyResources((object) this.saveRB, "saveRB");
    this.saveRB.Name = "saveRB";
    this.viewRB.Checked = true;
    componentResourceManager.ApplyResources((object) this.viewRB, "viewRB");
    this.viewRB.Name = "viewRB";
    this.viewRB.TabStop = true;
    componentResourceManager.ApplyResources((object) this.loadRB, "loadRB");
    this.loadRB.Name = "loadRB";
    componentResourceManager.ApplyResources((object) this.clearRB, "clearRB");
    this.clearRB.Name = "clearRB";
    componentResourceManager.ApplyResources((object) this.loadFileDialog, "loadFileDialog");
    this.loadFileDialog.RestoreDirectory = true;
    componentResourceManager.ApplyResources((object) this.saveFileDialog, "saveFileDialog");
    this.saveFileDialog.RestoreDirectory = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.clearRB);
    this.Controls.Add((Control) this.loadRB);
    this.Controls.Add((Control) this.saveRB);
    this.Controls.Add((Control) this.viewRB);
    this.Controls.Add((Control) this.buttonDo);
    this.Name = nameof (FileEditorForm);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.FileEditorForm_Load);
    this.ResumeLayout(false);
  }

  public void SetFormData(FilePropertyClass fpc) => this.filePropertyClass = fpc;

  public FilePropertyClass GetFormData() => this.filePropertyClass;

  private void buttonDo_Click(object sender, EventArgs e)
  {
    if (this.viewRB.Checked)
    {
      int num1 = this.viewRB.Enabled ? 1 : 0;
    }
    if (this.saveRB.Checked && this.saveRB.Enabled && this.saveFileDialog.ShowDialog() == DialogResult.OK)
    {
      using (Stream aDestStream = (Stream) new FileStream(this.saveFileDialog.FileName, FileMode.CreateNew, FileAccess.Write))
      {
        BlobProcReader blobProcReader = new BlobProcReader(this.filePropertyClass.ElementID, this.filePropertyClass.AttributableElement, this.filePropertyClass.AttributeID, this.filePropertyClass.Index, Consts.DefaultBlobBlockSize, aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
        try
        {
          blobProcReader.ReadData();
        }
        catch
        {
          string[] strArray = new string[6]
          {
            LocalizationHolder.rm.GetString("Client.Core_958"),
            this.filePropertyClass.ElementID.ToString(),
            LocalizationHolder.rm.GetString("Client.Core_959"),
            null,
            null,
            null
          };
          int num2 = this.filePropertyClass.AttributeID;
          strArray[3] = num2.ToString();
          strArray[4] = LocalizationHolder.rm.GetString("Client.Core_960");
          num2 = this.filePropertyClass.Index;
          strArray[5] = num2.ToString();
          int num3 = (int) MessageBox.Show(string.Concat(strArray));
        }
      }
    }
    if (this.loadRB.Checked && this.loadRB.Enabled && this.loadFileDialog.ShowDialog() == DialogResult.OK)
      this.filePropertyClass = new FilePropertyClass(this.loadFileDialog.FileName, this.filePropertyClass.ElementID, this.filePropertyClass.AttributableElement, this.filePropertyClass.AttributeID, this.filePropertyClass.Index);
    if (!this.clearRB.Checked || !this.clearRB.Enabled)
      return;
    this.filePropertyClass = new FilePropertyClass(string.Empty, this.filePropertyClass.ElementID, this.filePropertyClass.AttributableElement, this.filePropertyClass.AttributeID, this.filePropertyClass.Index);
  }

  private void FileEditorForm_Load(object sender, EventArgs e)
  {
    bool flag = this.filePropertyClass != null && this.filePropertyClass.FileName != string.Empty;
    this.viewRB.Enabled = flag;
    this.saveRB.Enabled = flag;
    this.clearRB.Enabled = flag;
  }
}

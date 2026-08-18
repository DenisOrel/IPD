
// Type: Intermech.Client.Core.OverwritePromptForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.ContextCommands;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// окно спрашивает нужно ли заменять файл, который существует
/// </summary>
public class OverwritePromptForm : Form
{
  /// <summary>имя файла для замены</summary>
  private string filename;
  /// <summary>размер файла</summary>
  private long oldFileSize;
  /// <summary>показывает нужно ли перезаписывать все файлы</summary>
  private bool replaceAll;
  /// <summary>показывает нужно ли пропускать все файлы</summary>
  private bool discardAll;
  private RenameMode rm;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox groupBoxOld;
  private Label labelOldSize;
  private Label labelOld;
  private GroupBox groupBoxNew;
  private Label labelNewSize;
  private Label labelNew;
  private Button buttonReplace;
  private Button buttonReplaceAll;
  private Button buttonCancel;
  private Button buttonDiscardAll;
  private Button buttonDiscard;
  private Button buttonRename;

  /// <summary>показывает нужно ли перезаписывать все файлы</summary>
  public bool ReplaceAll => this.replaceAll;

  /// <summary>показывает нужно ли пропускать все файлы</summary>
  public bool DiscardAll => this.discardAll;

  /// <summary>имя файла</summary>
  public string FileName
  {
    set
    {
      this.filename = value;
      this.labelOld.Text = value;
      this.labelOldSize.Text = this.oldFileSize.ToString() + LocalizationHolder.rm.GetString("Client.Core_1352");
    }
    get => this.filename;
  }

  /// <summary>только имя файла без пути</summary>
  public string OnlyName
  {
    set => this.labelNew.Text = value;
  }

  /// <summary>размер нового файла</summary>
  public long FileSize
  {
    set
    {
      this.labelNewSize.Text = value.ToString() + LocalizationHolder.rm.GetString("Client.Core_1352");
    }
  }

  public OverwritePromptForm()
  {
  }

  public OverwritePromptForm(
    string filename,
    string onlyName,
    long oldFileSize,
    long fileSize,
    RenameMode em)
  {
    this.InitializeComponent();
    this.rm = em;
    this.oldFileSize = oldFileSize;
    this.FileName = filename;
    this.OnlyName = onlyName;
    this.FileSize = fileSize;
    this.UpdateControls();
  }

  private void UpdateControls()
  {
    if (this.rm != RenameMode.WmfMode)
      return;
    this.Text = LocalizationHolder.rm.GetString("Client.Core_1353");
    this.groupBoxOld.Text = LocalizationHolder.rm.GetString("Client.Core_1354");
    this.groupBoxNew.Text = LocalizationHolder.rm.GetString("Client.Core_1355");
  }

  private void buttonReplaceAll_Click(object sender, EventArgs e) => this.replaceAll = true;

  private void buttonDiscardAll_Click(object sender, EventArgs e) => this.discardAll = true;

  /// <summary>
  /// переименовывать нужно в зависимости от типа
  /// (просто файл и набор метафайлов для старниц указанного файла)
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonRename_Click(object sender, EventArgs e)
  {
    RenameFileForm renameFileForm = new RenameFileForm(this.labelNew.Text);
    if (renameFileForm.ShowDialog() != DialogResult.OK)
      return;
    string newValue = renameFileForm.FileName;
    if (this.rm == RenameMode.WmfMode)
      newValue = renameFileForm.FileName + "#0.wmf";
    if (File.Exists(this.filename.Replace(this.labelNew.Text, newValue)))
      renameFileForm.FileName = this.labelNew.Text;
    if (renameFileForm.FileName.CompareTo(this.labelNew.Text) == 0)
      return;
    this.filename = this.filename.Replace(this.labelNew.Text, renameFileForm.FileName);
    this.DialogResult = DialogResult.Yes;
    this.Close();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OverwritePromptForm));
    this.groupBoxOld = new GroupBox();
    this.labelOldSize = new Label();
    this.labelOld = new Label();
    this.groupBoxNew = new GroupBox();
    this.labelNewSize = new Label();
    this.labelNew = new Label();
    this.buttonReplace = new Button();
    this.buttonReplaceAll = new Button();
    this.buttonCancel = new Button();
    this.buttonDiscardAll = new Button();
    this.buttonDiscard = new Button();
    this.buttonRename = new Button();
    this.groupBoxOld.SuspendLayout();
    this.groupBoxNew.SuspendLayout();
    this.SuspendLayout();
    this.groupBoxOld.Controls.Add((Control) this.labelOldSize);
    this.groupBoxOld.Controls.Add((Control) this.labelOld);
    componentResourceManager.ApplyResources((object) this.groupBoxOld, "groupBoxOld");
    this.groupBoxOld.Name = "groupBoxOld";
    this.groupBoxOld.TabStop = false;
    componentResourceManager.ApplyResources((object) this.labelOldSize, "labelOldSize");
    this.labelOldSize.Name = "labelOldSize";
    componentResourceManager.ApplyResources((object) this.labelOld, "labelOld");
    this.labelOld.Name = "labelOld";
    this.groupBoxNew.Controls.Add((Control) this.labelNewSize);
    this.groupBoxNew.Controls.Add((Control) this.labelNew);
    componentResourceManager.ApplyResources((object) this.groupBoxNew, "groupBoxNew");
    this.groupBoxNew.Name = "groupBoxNew";
    this.groupBoxNew.TabStop = false;
    componentResourceManager.ApplyResources((object) this.labelNewSize, "labelNewSize");
    this.labelNewSize.Name = "labelNewSize";
    componentResourceManager.ApplyResources((object) this.labelNew, "labelNew");
    this.labelNew.Name = "labelNew";
    this.buttonReplace.DialogResult = DialogResult.Yes;
    componentResourceManager.ApplyResources((object) this.buttonReplace, "buttonReplace");
    this.buttonReplace.Name = "buttonReplace";
    this.buttonReplace.UseVisualStyleBackColor = true;
    this.buttonReplaceAll.DialogResult = DialogResult.Yes;
    componentResourceManager.ApplyResources((object) this.buttonReplaceAll, "buttonReplaceAll");
    this.buttonReplaceAll.Name = "buttonReplaceAll";
    this.buttonReplaceAll.UseVisualStyleBackColor = true;
    this.buttonReplaceAll.Click += new EventHandler(this.buttonReplaceAll_Click);
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.buttonDiscardAll.DialogResult = DialogResult.No;
    componentResourceManager.ApplyResources((object) this.buttonDiscardAll, "buttonDiscardAll");
    this.buttonDiscardAll.Name = "buttonDiscardAll";
    this.buttonDiscardAll.UseVisualStyleBackColor = true;
    this.buttonDiscardAll.Click += new EventHandler(this.buttonDiscardAll_Click);
    this.buttonDiscard.DialogResult = DialogResult.No;
    componentResourceManager.ApplyResources((object) this.buttonDiscard, "buttonDiscard");
    this.buttonDiscard.Name = "buttonDiscard";
    this.buttonDiscard.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.buttonRename, "buttonRename");
    this.buttonRename.Name = "buttonRename";
    this.buttonRename.UseVisualStyleBackColor = true;
    this.buttonRename.Click += new EventHandler(this.buttonRename_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.Controls.Add((Control) this.buttonRename);
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.buttonDiscardAll);
    this.Controls.Add((Control) this.buttonDiscard);
    this.Controls.Add((Control) this.buttonReplaceAll);
    this.Controls.Add((Control) this.buttonReplace);
    this.Controls.Add((Control) this.groupBoxNew);
    this.Controls.Add((Control) this.groupBoxOld);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (OverwritePromptForm);
    this.ShowInTaskbar = false;
    this.groupBoxOld.ResumeLayout(false);
    this.groupBoxOld.PerformLayout();
    this.groupBoxNew.ResumeLayout(false);
    this.groupBoxNew.PerformLayout();
    this.ResumeLayout(false);
  }
}

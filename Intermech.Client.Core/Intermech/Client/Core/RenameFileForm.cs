
// Type: Intermech.Client.Core.RenameFileForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class RenameFileForm : Form
{
  /// <summary>имя файла для переименования</summary>
  private string name;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button buttonCancel;
  private Button buttonOK;
  private TextBox textBoxName;
  private Label labelNewName;

  /// <summary>имя файла для переименования</summary>
  public string FileName
  {
    get => this.name;
    set => this.name = value;
  }

  /// <summary>окно "Переименовать файл"</summary>
  /// <param name="oldName">старое имя файла</param>
  public RenameFileForm(string oldName)
  {
    this.InitializeComponent();
    this.textBoxName.Text = oldName;
    this.name = oldName;
  }

  private void buttonOK_Click(object sender, EventArgs e) => this.name = this.textBoxName.Text;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RenameFileForm));
    this.buttonCancel = new Button();
    this.buttonOK = new Button();
    this.textBoxName = new TextBox();
    this.labelNewName = new Label();
    this.SuspendLayout();
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.buttonOK.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.buttonOK, "buttonOK");
    this.buttonOK.Name = "buttonOK";
    this.buttonOK.UseVisualStyleBackColor = true;
    this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
    componentResourceManager.ApplyResources((object) this.textBoxName, "textBoxName");
    this.textBoxName.Name = "textBoxName";
    componentResourceManager.ApplyResources((object) this.labelNewName, "labelNewName");
    this.labelNewName.Name = "labelNewName";
    this.AcceptButton = (IButtonControl) this.buttonOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.Controls.Add((Control) this.labelNewName);
    this.Controls.Add((Control) this.textBoxName);
    this.Controls.Add((Control) this.buttonOK);
    this.Controls.Add((Control) this.buttonCancel);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (RenameFileForm);
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}

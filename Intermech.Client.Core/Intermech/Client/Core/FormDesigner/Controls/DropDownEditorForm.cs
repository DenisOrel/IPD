
// Type: Intermech.Client.Core.FormDesigner.Controls.DropDownEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
internal class DropDownEditorForm : Form, IWindowsFormsEditorService
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Конструктор.</summary>
  public DropDownEditorForm() => this.InitializeComponent();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  public void CloseDropDown() => this.Close();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="control"></param>
  public void DropDownControl(Control control)
  {
    control.Parent = (Control) this;
    control.Dock = DockStyle.Fill;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dialog"></param>
  /// <returns></returns>
  public DialogResult ShowDialog(Form dialog) => dialog.ShowDialog();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DropDownEditorForm));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = "DropDownEditor";
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
  }
}

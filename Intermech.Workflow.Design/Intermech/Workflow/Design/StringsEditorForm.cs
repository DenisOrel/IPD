// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.StringsEditorForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class StringsEditorForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel Panel2;
  private Button CancButton;
  private Button OkButton;
  public TextBox ValuesBox;

  public StringsEditorForm() => this.InitializeComponent();

  private void StringsEditorForm_Activated(object sender, EventArgs e)
  {
    this.ValuesBox.DeselectAll();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (StringsEditorForm));
    this.Panel2 = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.ValuesBox = new TextBox();
    this.Panel2.SuspendLayout();
    this.SuspendLayout();
    this.Panel2.Controls.Add((Control) this.CancButton);
    this.Panel2.Controls.Add((Control) this.OkButton);
    componentResourceManager.ApplyResources((object) this.Panel2, "Panel2");
    this.Panel2.Name = "Panel2";
    componentResourceManager.ApplyResources((object) this.CancButton, "CancButton");
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Name = "CancButton";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    this.ValuesBox.AcceptsReturn = true;
    componentResourceManager.ApplyResources((object) this.ValuesBox, "ValuesBox");
    this.ValuesBox.Name = "ValuesBox";
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.ValuesBox);
    this.Controls.Add((Control) this.Panel2);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (StringsEditorForm);
    this.ShowInTaskbar = false;
    this.Tag = (object) "  ";
    this.Activated += new EventHandler(this.StringsEditorForm_Activated);
    this.Panel2.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}

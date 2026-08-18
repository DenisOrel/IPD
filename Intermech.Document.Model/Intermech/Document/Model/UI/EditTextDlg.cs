// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.EditTextDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.UI;

public class EditTextDlg : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _BtnCancel;
  private Button _BtnOK;
  private TextBox textBox;
  private Label label;

  public string AttributeText
  {
    get => this.textBox.Text;
    set => this.textBox.Text = value;
  }

  protected override void OnShown(EventArgs e)
  {
    base.OnShown(e);
    this.textBox.Focus();
  }

  public EditTextDlg(string text, string caption)
  {
    this.InitializeComponent();
    this.label.Text = caption;
    this.AttributeText = text;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EditTextDlg));
    this._BtnCancel = new Button();
    this._BtnOK = new Button();
    this.textBox = new TextBox();
    this.label = new Label();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._BtnCancel, "_BtnCancel");
    this._BtnCancel.DialogResult = DialogResult.Cancel;
    this._BtnCancel.Name = "_BtnCancel";
    componentResourceManager.ApplyResources((object) this._BtnOK, "_BtnOK");
    this._BtnOK.DialogResult = DialogResult.OK;
    this._BtnOK.Name = "_BtnOK";
    componentResourceManager.ApplyResources((object) this.textBox, "textBox");
    this.textBox.Name = "textBox";
    componentResourceManager.ApplyResources((object) this.label, "label");
    this.label.Name = "label";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._BtnCancel;
    this.Controls.Add((Control) this.label);
    this.Controls.Add((Control) this.textBox);
    this.Controls.Add((Control) this._BtnCancel);
    this.Controls.Add((Control) this._BtnOK);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (EditTextDlg);
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}

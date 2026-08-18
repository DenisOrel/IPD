
// Type: Intermech.Controls.InputQueryForm
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Controls;

public class InputQueryForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button okBtn;
  private Button canBtn;
  public TextBox tb;
  public Label l;

  public InputQueryForm() => this.InitializeComponent();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string QueryLabel
  {
    get => this.l.Text;
    set => this.l.Text = value != null ? value : throw new ArgumentNullException(nameof (value));
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string QueryText
  {
    get => this.tb.Text;
    set => this.tb.Text = value != null ? value : throw new ArgumentNullException(nameof (value));
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (InputQueryForm));
    this.tb = new TextBox();
    this.okBtn = new Button();
    this.canBtn = new Button();
    this.l = new Label();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tb, "tb");
    this.tb.Name = "tb";
    this.okBtn.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.okBtn, "okBtn");
    this.okBtn.Name = "okBtn";
    this.okBtn.UseVisualStyleBackColor = true;
    this.canBtn.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.canBtn, "canBtn");
    this.canBtn.Name = "canBtn";
    this.canBtn.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.l, "l");
    this.l.Name = "l";
    this.AcceptButton = (IButtonControl) this.okBtn;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.canBtn;
    this.Controls.Add((Control) this.canBtn);
    this.Controls.Add((Control) this.okBtn);
    this.Controls.Add((Control) this.tb);
    this.Controls.Add((Control) this.l);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (InputQueryForm);
    this.ShowInTaskbar = false;
    this.Tag = (object) "  ";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}

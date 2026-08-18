// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.AskDocModify
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class AskDocModify : Form
{
  public int selRes;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private Panel panel1;
  private Panel panel2;
  private Label label2;
  private Button button1;
  private RadioButton rbCreateVersion;
  private RadioButton rbOverwrite;
  private RadioButton rbCreateNew;
  private Button button2;

  public AskDocModify() => this.InitializeComponent();

  public int Execute(bool versionable, bool unique)
  {
    this.rbCreateVersion.Enabled = versionable;
    this.rbCreateNew.Enabled = !unique;
    if (versionable)
      this.rbCreateVersion.Checked = true;
    else
      this.rbOverwrite.Checked = true;
    if (this.ShowDialog() == DialogResult.OK)
    {
      if (this.rbCreateVersion.Checked)
        this.selRes = 1;
      if (this.rbOverwrite.Checked)
        this.selRes = 2;
      if (this.rbCreateNew.Checked)
        this.selRes = 3;
    }
    else
      this.selRes = 0;
    return this.selRes;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AskDocModify));
    this.label1 = new Label();
    this.panel1 = new Panel();
    this.label2 = new Label();
    this.panel2 = new Panel();
    this.button2 = new Button();
    this.button1 = new Button();
    this.rbCreateVersion = new RadioButton();
    this.rbOverwrite = new RadioButton();
    this.rbCreateNew = new RadioButton();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.BorderStyle = BorderStyle.FixedSingle;
    this.panel1.Controls.Add((Control) this.label2);
    this.panel1.Controls.Add((Control) this.label1);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.BorderStyle = BorderStyle.Fixed3D;
    this.panel2.Controls.Add((Control) this.button2);
    this.panel2.Controls.Add((Control) this.button1);
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbCreateVersion, "rbCreateVersion");
    this.rbCreateVersion.Name = "rbCreateVersion";
    this.rbCreateVersion.TabStop = true;
    this.rbCreateVersion.Tag = (object) "1";
    this.rbCreateVersion.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbOverwrite, "rbOverwrite");
    this.rbOverwrite.Name = "rbOverwrite";
    this.rbOverwrite.TabStop = true;
    this.rbOverwrite.Tag = (object) "2";
    this.rbOverwrite.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbCreateNew, "rbCreateNew");
    this.rbCreateNew.Name = "rbCreateNew";
    this.rbCreateNew.TabStop = true;
    this.rbCreateNew.Tag = (object) "3";
    this.rbCreateNew.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.rbCreateNew);
    this.Controls.Add((Control) this.rbOverwrite);
    this.Controls.Add((Control) this.rbCreateVersion);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AskDocModify);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.panel2.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}

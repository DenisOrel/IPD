// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.TemporaryForm
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class TemporaryForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ImageList IL;
  private BackgroundWorker backgroundWorker1;
  private Button button1;

  public TemporaryForm() => this.InitializeComponent();

  private void simpleButton1_Click(object sender, EventArgs e)
  {
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
    this.components = (IContainer) new System.ComponentModel.Container();
    this.IL = new ImageList(this.components);
    this.backgroundWorker1 = new BackgroundWorker();
    this.button1 = new Button();
    this.SuspendLayout();
    this.IL.TransparentColor = Color.Magenta;
    this.IL.Images.SetKeyName(0, "");
    this.IL.Images.SetKeyName(1, "");
    this.IL.Images.SetKeyName(2, "");
    this.IL.Images.SetKeyName(3, "");
    this.IL.Images.SetKeyName(4, "");
    this.IL.Images.SetKeyName(5, "");
    this.IL.Images.SetKeyName(6, "");
    this.IL.Images.SetKeyName(7, "");
    this.IL.Images.SetKeyName(8, "");
    this.IL.Images.SetKeyName(9, "");
    this.IL.Images.SetKeyName(10, "");
    this.IL.Images.SetKeyName(11, "");
    this.IL.Images.SetKeyName(12, "");
    this.IL.Images.SetKeyName(13, "");
    this.IL.Images.SetKeyName(14, "");
    this.IL.Images.SetKeyName(15, "");
    this.IL.Images.SetKeyName(16 /*0x10*/, "");
    this.IL.Images.SetKeyName(17, "");
    this.IL.Images.SetKeyName(18, "");
    this.IL.Images.SetKeyName(19, "");
    this.IL.Images.SetKeyName(20, "");
    this.IL.Images.SetKeyName(21, "");
    this.IL.Images.SetKeyName(22, "");
    this.IL.Images.SetKeyName(23, "");
    this.IL.Images.SetKeyName(24, "");
    this.IL.Images.SetKeyName(25, "");
    this.IL.Images.SetKeyName(26, "");
    this.IL.Images.SetKeyName(27, "");
    this.IL.Images.SetKeyName(28, "");
    this.IL.Images.SetKeyName(29, "");
    this.IL.Images.SetKeyName(30, "");
    this.IL.Images.SetKeyName(31 /*0x1F*/, "");
    this.IL.Images.SetKeyName(32 /*0x20*/, "");
    this.IL.Images.SetKeyName(33, "");
    this.IL.Images.SetKeyName(34, "");
    this.IL.Images.SetKeyName(35, "");
    this.IL.Images.SetKeyName(36, "");
    this.IL.Images.SetKeyName(37, "VVV2.bmp");
    this.IL.Images.SetKeyName(38, "VVV1.bmp");
    this.button1.DialogResult = DialogResult.Cancel;
    this.button1.Location = new Point(552, 592);
    this.button1.Name = "button1";
    this.button1.Size = new Size(75, 23);
    this.button1.TabIndex = 0;
    this.button1.Text = "button1";
    this.button1.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(666, 637);
    this.Controls.Add((Control) this.button1);
    this.MinimumSize = new Size(200, 100);
    this.Name = nameof (TemporaryForm);
    this.Text = nameof (TemporaryForm);
    this.ResumeLayout(false);
  }
}

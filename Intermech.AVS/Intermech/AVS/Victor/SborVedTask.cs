// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.SborVedTask
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

public class SborVedTask : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public SborVedTask() => this.InitializeComponent();

  public SborVedTask(string text)
  {
    this.InitializeComponent();
    this.Text = text;
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
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(12f, 25f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(637, 32 /*0x20*/);
    this.ControlBox = false;
    this.Font = new Font("Microsoft Sans Serif", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.Margin = new Padding(6);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SborVedTask);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.ResumeLayout(false);
  }
}

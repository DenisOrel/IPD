// Decompiled with JetBrains decompiler
// Type: Intermech.AutoUpdater.AutoUpdaterErrorDialog
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoUpdater;

public class AutoUpdaterErrorDialog : AutoUpdaterMessageDialog
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public AutoUpdaterErrorDialog() => this.InitializeComponent();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoUpdaterErrorDialog));
    ((ISupportInitialize) this.pbUpdateIcon).BeginInit();
    this.SuspendLayout();
    this.pbUpdateIcon.Image = (Image) componentResourceManager.GetObject("pbUpdateIcon.Image");
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(504, 196);
    this.Name = nameof (AutoUpdaterErrorDialog);
    ((ISupportInitialize) this.pbUpdateIcon).EndInit();
    this.ResumeLayout(false);
  }
}

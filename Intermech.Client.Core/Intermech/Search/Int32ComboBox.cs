
// Type: Intermech.Search.Int32ComboBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class Int32ComboBox : BaseComboBox<int>
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public Int32ComboBox() => this.InitializeComponent();

  protected override bool SupportedClearing => true;

  protected override bool SupportedEditing => false;

  protected override bool TryParse(string text, out int result) => int.TryParse(text, out result);

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
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (Int32ComboBox);
    this.ResumeLayout(false);
  }
}

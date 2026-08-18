
// Type: Intermech.Search.BooleanAttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class BooleanAttributeEditor : SingleValueAttributeEditor
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private BooleanComboBox _booleanComboBox;

  public BooleanAttributeEditor() => this.InitializeComponent();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected override ISingleValueEditor ValueEditor => (ISingleValueEditor) this._booleanComboBox;

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
    this._booleanComboBox = new BooleanComboBox();
    this.SuspendLayout();
    this._booleanComboBox.Dock = DockStyle.Fill;
    this._booleanComboBox.Location = new Point(0, 0);
    this._booleanComboBox.Name = "_booleanComboBox";
    this._booleanComboBox.Size = new Size(200, 21);
    this._booleanComboBox.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._booleanComboBox);
    this.Name = nameof (BooleanAttributeEditor);
    this.Size = new Size(200, 21);
    this.ResumeLayout(false);
  }
}

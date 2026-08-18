
// Type: Intermech.Search.ObjectTypeAttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class ObjectTypeAttributeEditor : SingleValueAttributeEditor
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ObjectTypeBox _objectTypeBox;

  public ObjectTypeAttributeEditor() => this.InitializeComponent();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override bool AllowEmpty
  {
    get => false;
    set => throw new NotSupportedException();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected override ISingleValueEditor ValueEditor => (ISingleValueEditor) this._objectTypeBox;

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
    this._objectTypeBox = new ObjectTypeBox();
    this.SuspendLayout();
    this._objectTypeBox.Dock = DockStyle.Fill;
    this._objectTypeBox.Location = new Point(0, 0);
    this._objectTypeBox.Name = "_objectTypeBox";
    this._objectTypeBox.Size = new Size(200, 20);
    this._objectTypeBox.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._objectTypeBox);
    this.Name = nameof (ObjectTypeAttributeEditor);
    this.Size = new Size(200, 20);
    this.ResumeLayout(false);
  }
}

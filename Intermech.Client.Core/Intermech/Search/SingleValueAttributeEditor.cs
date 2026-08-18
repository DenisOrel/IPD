
// Type: Intermech.Search.SingleValueAttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public class SingleValueAttributeEditor : AttributeEditor
{
  private bool _isValueChangedSubscribed;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public SingleValueAttributeEditor() => this.InitializeComponent();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected virtual ISingleValueEditor ValueEditor => (ISingleValueEditor) null;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override bool IsEmpty => this.ValueEditor.IsEmpty;

  public override bool IsValid => this.ValueEditor.IsValid;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override object Value
  {
    get => this.ValueEditor.Value;
    set => this.ValueEditor.Value = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override object[] Values
  {
    get
    {
      if (this.Value == null)
        return (object[]) null;
      return new object[1]{ this.Value };
    }
    set => this.Value = value == null || value.Length == 0 ? (object) null : value[0];
  }

  protected override void DoInitializeEditor()
  {
    if (this.ValueEditor == null)
      return;
    if (!this._isValueChangedSubscribed)
    {
      this.ValueEditor.ValueChanged += new EventHandler(this.ValueEditor_ValueChanged);
      this._isValueChangedSubscribed = true;
    }
    this.ValueEditor.AllowEmpty = this.AllowEmpty;
  }

  public override void SetFocus() => this.ValueEditor.SetFocus();

  private void ValueEditor_ValueChanged(object sender, EventArgs e) => this.OnValueChanged();

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
    this.BeginInit();
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (SingleValueAttributeEditor);
    this.EndInit();
    this.ResumeLayout(false);
  }
}


// Type: Intermech.Search.MeasuredAttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class MeasuredAttributeEditor : SingleValueAttributeEditor
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private MeasuredValueBox _measuredValueBox;

  public MeasuredAttributeEditor() => this.InitializeComponent();

  protected override void DoInitializeEditor()
  {
    base.DoInitializeEditor();
    this._measuredValueBox.DefaultMeasureVersionID = this.GetDefaultMeasureVersionID();
    this._measuredValueBox.MeasureDescriptors = this.GetMeasureDescriptors();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected override ISingleValueEditor ValueEditor => (ISingleValueEditor) this._measuredValueBox;

  private long GetDefaultMeasureVersionID()
  {
    string validationRule = this.AttributeTypeForObject != null ? this.AttributeTypeForObject.ValidationRule : (this.AttributeTypeForRelation != null ? this.AttributeTypeForRelation.ValidationRule : (string) null);
    return string.IsNullOrEmpty(validationRule) ? 0L : MeasuredValueHelper.GetDefaultMeasureVerisonIDFromValidationRule(validationRule);
  }

  private MeasureDescriptor[] GetMeasureDescriptors()
  {
    return AttributeTypeHelper.IsUnknownAttributeTypeID(this.AttributeTypeID) ? new MeasureDescriptor[0] : MeasuredValueHelper.GetMeasureDescriptorsForAttributeType(this.AttributeTypeID);
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
    this._measuredValueBox = new MeasuredValueBox();
    this.SuspendLayout();
    this._measuredValueBox.Dock = DockStyle.Fill;
    this._measuredValueBox.Location = new Point(0, 0);
    this._measuredValueBox.Margin = new Padding(0);
    this._measuredValueBox.Name = "_measuredValueEditor";
    this._measuredValueBox.Size = new Size(200, 20);
    this._measuredValueBox.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._measuredValueBox);
    this.Name = nameof (MeasuredAttributeEditor);
    this.Size = new Size(200, 20);
    this.ResumeLayout(false);
  }
}


// Type: Intermech.Search.UniversalAttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class UniversalAttributeEditor : AttributeEditor
{
  private AttributeEditor _editor;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private BooleanAttributeEditor _booleanAttributeEditor;
  private DateTimeAttributeEditor _dateTimeAttributeEditor;
  private DoubleAttributeEditor _doubleAttributeEditor;
  private IntegerAttributeEditor _integerAttributeEditor;
  private MeasuredAttributeEditor _measuredAttributeEditor;
  private StringAttributeEditor _stringAttributeEditor;
  private SpecialAttributeEditor _specialAttributeEditor;
  private ObjectLinkAttributeEditor _objectLinkAttributeEditor;
  private SingleValueFromListAttributeEditor _singleValueFromListAttributeEditor;
  private MultiValuesFromListAttributeEditor _multiValuesFromListAttributeEditor;
  private GuidAttributeEditor _guidAttributeEditor;
  private ObjectTypeAttributeEditor _objectTypeAttributeEditor;

  public UniversalAttributeEditor() => this.InitializeComponent();

  public override bool IsValid => this._editor != null && this._editor.IsValid;

  protected override void DoInitializeEditor()
  {
    AttributeEditor attributeEditor = this.SelectEditor();
    if (this._editor != attributeEditor)
    {
      if (this._editor != null)
      {
        this._editor.ValueChanged -= new EventHandler(this.AttributeEditor_ValueChanged);
        this._editor.Visible = false;
      }
      this._editor = attributeEditor;
      if (this._editor != null)
      {
        this._editor.ValueChanged += new EventHandler(this.AttributeEditor_ValueChanged);
        this.MinimumSize = this._editor.MinimumSize;
        this._editor.Dock = DockStyle.Fill;
        this._editor.Visible = true;
      }
    }
    if (this._editor == null)
      return;
    this._editor.BeginInit();
    try
    {
      this._editor.AttributeTypeID = this.AttributeTypeID;
      this._editor.ObjectTypeID = this.ObjectTypeID;
      this._editor.RelationTypeID = this.RelationTypeID;
      this._editor.AttributePropertyDescriberService = this.AttributePropertyDescriberService;
      this._editor.ElementInfo = this.ElementInfo;
      this._editor.Values = this.Values;
    }
    finally
    {
      this._editor.EndInit();
    }
  }

  public override void SetFocus()
  {
    if (this._editor == null)
      return;
    this._editor.SetFocus();
  }

  private void AttributeEditor_ValueChanged(object sender, EventArgs e)
  {
    this.Values = this._editor.Values;
  }

  private AttributeEditor SelectEditor()
  {
    if (this.UITypeEditor != null)
      return (AttributeEditor) this._specialAttributeEditor;
    if (this.AttributeTypeID == -7)
      return (AttributeEditor) this._objectTypeAttributeEditor;
    if (this.AttributeTypeID == -77)
      throw new InvalidOperationException();
    if (this.AttributeType == null)
      return (AttributeEditor) null;
    switch (this.AttributeType.MultiValueMode)
    {
      case MultiValueModes.SingleValue:
        FieldTypes fieldTypes = this.AttributeType.FieldType;
        if (fieldTypes == FieldTypes.ftSystem)
          fieldTypes = AttributeTypeHelper.GetFieldTypeForObligatoryObjectAttribute((ObligatoryObjectAttributes) this.AttributeTypeID);
        switch (fieldTypes - 1)
        {
          case FieldTypes.ftUnknown:
            return (AttributeEditor) this._stringAttributeEditor;
          case FieldTypes.ftString:
            return (AttributeEditor) this._integerAttributeEditor;
          case FieldTypes.ftInteger:
            return (AttributeEditor) this._doubleAttributeEditor;
          case FieldTypes.ftDouble:
            return (AttributeEditor) this._dateTimeAttributeEditor;
          case FieldTypes.ftExternalLink:
            return (AttributeEditor) this._objectLinkAttributeEditor;
          case FieldTypes.ftPassword:
            return (AttributeEditor) this._stringAttributeEditor;
          case FieldTypes.ftBlob:
            return (AttributeEditor) this._booleanAttributeEditor;
          case FieldTypes.ftBoolean:
            return (AttributeEditor) this._measuredAttributeEditor;
          case FieldTypes.ftSystem:
            return (AttributeEditor) this._guidAttributeEditor;
          default:
            throw new InvalidOperationException();
        }
      case MultiValueModes.MultiValues:
        throw new InvalidOperationException();
      case MultiValueModes.SingleValueFromList:
        return (AttributeEditor) this._singleValueFromListAttributeEditor;
      case MultiValueModes.MultiValuesFromList:
        return (AttributeEditor) this._multiValuesFromListAttributeEditor;
      default:
        throw new InvalidOperationException();
    }
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
    this._booleanAttributeEditor = new BooleanAttributeEditor();
    this._dateTimeAttributeEditor = new DateTimeAttributeEditor();
    this._doubleAttributeEditor = new DoubleAttributeEditor();
    this._integerAttributeEditor = new IntegerAttributeEditor();
    this._measuredAttributeEditor = new MeasuredAttributeEditor();
    this._stringAttributeEditor = new StringAttributeEditor();
    this._specialAttributeEditor = new SpecialAttributeEditor();
    this._objectLinkAttributeEditor = new ObjectLinkAttributeEditor();
    this._singleValueFromListAttributeEditor = new SingleValueFromListAttributeEditor();
    this._multiValuesFromListAttributeEditor = new MultiValuesFromListAttributeEditor();
    this._guidAttributeEditor = new GuidAttributeEditor();
    this._objectTypeAttributeEditor = new ObjectTypeAttributeEditor();
    this._booleanAttributeEditor.BeginInit();
    this._dateTimeAttributeEditor.BeginInit();
    this._doubleAttributeEditor.BeginInit();
    this._integerAttributeEditor.BeginInit();
    this._measuredAttributeEditor.BeginInit();
    this._stringAttributeEditor.BeginInit();
    this._specialAttributeEditor.BeginInit();
    this._objectLinkAttributeEditor.BeginInit();
    this._singleValueFromListAttributeEditor.BeginInit();
    this._multiValuesFromListAttributeEditor.BeginInit();
    this._guidAttributeEditor.BeginInit();
    this._objectTypeAttributeEditor.BeginInit();
    this.BeginInit();
    this.SuspendLayout();
    this._booleanAttributeEditor.Location = new Point(3, 3);
    this._booleanAttributeEditor.Name = "_booleanAttributeEditor";
    this._booleanAttributeEditor.Size = new Size(262, 22);
    this._booleanAttributeEditor.TabIndex = 0;
    this._booleanAttributeEditor.Visible = false;
    this._dateTimeAttributeEditor.Location = new Point(4, 32 /*0x20*/);
    this._dateTimeAttributeEditor.Name = "_dateTimeAttributeEditor";
    this._dateTimeAttributeEditor.Size = new Size(261, 22);
    this._dateTimeAttributeEditor.TabIndex = 1;
    this._dateTimeAttributeEditor.Values = (object[]) null;
    this._dateTimeAttributeEditor.Visible = false;
    this._doubleAttributeEditor.Location = new Point(4, 60);
    this._doubleAttributeEditor.Name = "_doubleAttributeEditor";
    this._doubleAttributeEditor.Size = new Size(261, 23);
    this._doubleAttributeEditor.TabIndex = 2;
    this._doubleAttributeEditor.Visible = false;
    this._integerAttributeEditor.Location = new Point(4, 89);
    this._integerAttributeEditor.Name = "_integerAttributeEditor";
    this._integerAttributeEditor.Size = new Size(261, 25);
    this._integerAttributeEditor.TabIndex = 3;
    this._integerAttributeEditor.Visible = false;
    this._measuredAttributeEditor.Location = new Point(4, 120);
    this._measuredAttributeEditor.Name = "_measuredAttributeEditor";
    this._measuredAttributeEditor.Size = new Size(261, 21);
    this._measuredAttributeEditor.TabIndex = 4;
    this._measuredAttributeEditor.Visible = false;
    this._stringAttributeEditor.Location = new Point(4, 148);
    this._stringAttributeEditor.Name = "_stringAttributeEditor";
    this._stringAttributeEditor.Size = new Size(261, 21);
    this._stringAttributeEditor.TabIndex = 5;
    this._stringAttributeEditor.Visible = false;
    this._specialAttributeEditor.Location = new Point(4, 176 /*0xB0*/);
    this._specialAttributeEditor.Name = "_specialAttributeEditor";
    this._specialAttributeEditor.Size = new Size(261, 21);
    this._specialAttributeEditor.TabIndex = 6;
    this._specialAttributeEditor.Values = (object[]) null;
    this._specialAttributeEditor.Visible = false;
    this._objectLinkAttributeEditor.Location = new Point(10, 203);
    this._objectLinkAttributeEditor.Name = "_objectLinkAttributeEditor";
    this._objectLinkAttributeEditor.Size = new Size((int) byte.MaxValue, 20);
    this._objectLinkAttributeEditor.TabIndex = 7;
    this._objectLinkAttributeEditor.Visible = false;
    this._singleValueFromListAttributeEditor.Location = new Point(4, 229);
    this._singleValueFromListAttributeEditor.Name = "_singleValueFromListAttributeEditor";
    this._singleValueFromListAttributeEditor.Size = new Size(261, 23);
    this._singleValueFromListAttributeEditor.TabIndex = 8;
    this._singleValueFromListAttributeEditor.Values = (object[]) null;
    this._singleValueFromListAttributeEditor.Visible = false;
    this._multiValuesFromListAttributeEditor.Location = new Point(4, 259);
    this._multiValuesFromListAttributeEditor.MinimumSize = new Size(0, 100);
    this._multiValuesFromListAttributeEditor.Name = "_multiValuesFromListAttributeEditor";
    this._multiValuesFromListAttributeEditor.Size = new Size(261, 100);
    this._multiValuesFromListAttributeEditor.TabIndex = 9;
    this._multiValuesFromListAttributeEditor.Values = (object[]) null;
    this._multiValuesFromListAttributeEditor.Visible = false;
    this._guidAttributeEditor.Location = new Point(6, 365);
    this._guidAttributeEditor.Name = "_guidAttributeEditor";
    this._guidAttributeEditor.Size = new Size(262, 22);
    this._guidAttributeEditor.TabIndex = 10;
    this._guidAttributeEditor.Visible = false;
    this._objectTypeAttributeEditor.Location = new Point(0, 393);
    this._objectTypeAttributeEditor.Name = "_objectTypeAttributeEditor";
    this._objectTypeAttributeEditor.Size = new Size(265, 20);
    this._objectTypeAttributeEditor.TabIndex = 11;
    this._objectTypeAttributeEditor.Visible = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._objectTypeAttributeEditor);
    this.Controls.Add((Control) this._guidAttributeEditor);
    this.Controls.Add((Control) this._multiValuesFromListAttributeEditor);
    this.Controls.Add((Control) this._singleValueFromListAttributeEditor);
    this.Controls.Add((Control) this._objectLinkAttributeEditor);
    this.Controls.Add((Control) this._specialAttributeEditor);
    this.Controls.Add((Control) this._stringAttributeEditor);
    this.Controls.Add((Control) this._measuredAttributeEditor);
    this.Controls.Add((Control) this._integerAttributeEditor);
    this.Controls.Add((Control) this._doubleAttributeEditor);
    this.Controls.Add((Control) this._dateTimeAttributeEditor);
    this.Controls.Add((Control) this._booleanAttributeEditor);
    this.Name = nameof (UniversalAttributeEditor);
    this.Size = new Size(271, 434);
    this._booleanAttributeEditor.EndInit();
    this._dateTimeAttributeEditor.EndInit();
    this._doubleAttributeEditor.EndInit();
    this._integerAttributeEditor.EndInit();
    this._measuredAttributeEditor.EndInit();
    this._stringAttributeEditor.EndInit();
    this._specialAttributeEditor.EndInit();
    this._objectLinkAttributeEditor.EndInit();
    this._singleValueFromListAttributeEditor.EndInit();
    this._multiValuesFromListAttributeEditor.EndInit();
    this._guidAttributeEditor.EndInit();
    this._objectTypeAttributeEditor.EndInit();
    this.EndInit();
    this.ResumeLayout(false);
  }
}

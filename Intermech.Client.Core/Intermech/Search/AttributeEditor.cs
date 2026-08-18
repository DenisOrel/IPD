
// Type: Intermech.Search.AttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.PropertyEditors;
using Intermech.Search.Utilities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Search;

public class AttributeEditor : 
  UserControl,
  ISingleValueEditor,
  IMultiValueEditor,
  IKeyUpHandler,
  ISupportInitialize
{
  private int _attributeTypeID;
  private int _objectTypeID = -1;
  private int _relationTypeID = -1;
  private IAttributePropertyDescriberService _attributePropertyDescriberService;
  private IElementInfo _elementInfo;
  private object _value;
  private bool _allowEmpty;
  private object[] _values;
  private bool _isInitializing;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public AttributeEditor() => this.InitializeComponent();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int AttributeTypeID
  {
    get => this._attributeTypeID;
    set
    {
      if (AttributeTypeHelper.IsUnknownAttributeTypeID(value))
        throw new ArgumentException();
      if (this._attributeTypeID == value)
        return;
      this._attributeTypeID = value;
      this.InitializeEditor();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected IMSAttributeType AttributeType { get; private set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int ObjectTypeID
  {
    get => this._objectTypeID;
    set
    {
      if (this._objectTypeID == value)
        return;
      this._objectTypeID = value;
      this.InitializeEditor();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected IMSAttribute4ObjectType AttributeTypeForObject { get; private set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int RelationTypeID
  {
    get => this._relationTypeID;
    set
    {
      if (this._relationTypeID == value)
        return;
      this._relationTypeID = value;
      this.InitializeEditor();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected IMSAttribute4RelationType AttributeTypeForRelation { get; private set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IAttributePropertyDescriberService AttributePropertyDescriberService
  {
    get => this._attributePropertyDescriberService;
    set
    {
      if (this._attributePropertyDescriberService == value)
        return;
      this._attributePropertyDescriberService = value;
      this.InitializeEditor();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected IAttributePropertyDescriber AttributePropertyDescriber { get; set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected UITypeEditor UITypeEditor { get; private set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IElementInfo ElementInfo
  {
    get => this._elementInfo;
    set
    {
      if (this._elementInfo == value)
        return;
      this._elementInfo = value;
      this.InitializeEditor();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IKeyUpHandler KeyUpHandler { get; set; }

  protected virtual void DoInitializeEditor()
  {
  }

  protected void SetValue(object value, bool doSetValue)
  {
    if (!object.Equals(this._value, value))
    {
      this._value = value;
      object[] objArray;
      if (this._value == null)
        objArray = (object[]) null;
      else
        objArray = new object[1]{ this._value };
      this._values = objArray;
      if (!this._isInitializing & doSetValue)
        this.DoSetValue();
      this.OnValueChanged();
    }
    else
    {
      if (!(!this._isInitializing & doSetValue))
        return;
      this.DoSetValue();
    }
  }

  protected virtual void DoSetValue()
  {
  }

  protected void OnValueChanged()
  {
    EventHandler valueChanged = this.ValueChanged;
    if (valueChanged == null)
      return;
    valueChanged((object) this, EventArgs.Empty);
  }

  protected void SetValues(object[] value, bool doSetValue)
  {
    if (!object.Equals((object) this._values, (object) value))
    {
      this._values = value;
      this._value = this._values == null || this._values.Length == 0 ? (object) null : this._values[0];
      if (!this._isInitializing & doSetValue)
        this.DoSetValue();
      this.OnValueChanged();
    }
    else
    {
      if (!(!this._isInitializing & doSetValue))
        return;
      this.DoSetValue();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual bool AllowEmpty
  {
    get => this._allowEmpty;
    set => throw new NotSupportedException();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual bool IsEmpty => this.Value == null;

  public virtual bool IsValid
  {
    get
    {
      if (this.AllowEmpty)
        return true;
      return !this.AllowEmpty && !this.IsEmpty;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual object Value
  {
    get => this._value;
    set => this.SetValue(value, true);
  }

  public event EventHandler ValueChanged;

  public virtual void SetFocus()
  {
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual object[] Values
  {
    get => this._values;
    set => this.SetValues(value, true);
  }

  public virtual void HandleKeyUp(Keys keyCode)
  {
    if (this.Parent is IKeyUpHandler)
    {
      ((IKeyUpHandler) this.Parent).HandleKeyUp(keyCode);
    }
    else
    {
      if (this.KeyUpHandler == null)
        return;
      this.KeyUpHandler.HandleKeyUp(keyCode);
    }
  }

  public virtual void BeginInit() => this._isInitializing = true;

  public virtual void EndInit()
  {
    if (!this._isInitializing)
      return;
    this._isInitializing = false;
    this.InitializeEditor();
  }

  private void InitializeEditor()
  {
    if (this._isInitializing || AttributeTypeHelper.IsUnknownAttributeTypeID(this.AttributeTypeID))
      return;
    this.AttributeType = MetaDataHelper.GetAttributeType(this.AttributeTypeID);
    this._allowEmpty = AttributeTypeHelper.AllowEmpty(this.AttributeTypeID);
    this.AttributeTypeForObject = MetaDataHelper.GetAttribute4ObjectType(this.ObjectTypeID, this.AttributeTypeID);
    this.AttributeTypeForRelation = MetaDataHelper.GetAttribute4RelationType(this.RelationTypeID, this.AttributeTypeID);
    if (this.AttributePropertyDescriberService != null)
    {
      this.AttributePropertyDescriber = this.AttributePropertyDescriberService.GetDescriber(this.AttributeTypeID);
      if (this.AttributePropertyDescriber != null)
        this.UITypeEditor = this.AttributePropertyDescriber.GetPropDescriptorEditor(this.AttributeTypeID) as UITypeEditor;
    }
    this.DoInitializeEditor();
    this.DoSetValue();
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
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (AttributeEditor);
    this.Size = new Size(210, 141);
    this.ResumeLayout(false);
  }
}

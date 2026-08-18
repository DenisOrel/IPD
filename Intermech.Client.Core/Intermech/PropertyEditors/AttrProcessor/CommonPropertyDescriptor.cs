
// Type: Intermech.PropertyEditors.AttrProcessor.CommonPropertyDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>Общий PropertyDescriptor, описывающий атрибут.</summary>
public class CommonPropertyDescriptor : PropertyDescriptor
{
  protected int attributeId;
  protected AttributeProcessor attributeProcessor;
  protected string displayName;
  protected Type componentType;
  protected Type propertyType;
  protected TypeConverter converter;
  protected bool readOnly;
  protected bool canReset;
  protected object editor;

  public int AttributeId => this.attributeId;

  public AttributeProcessor AttributeProcessor => this.attributeProcessor;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="attributeProcessor"></param>
  /// <param name="displayName">при null подставляется наименование атрибута</param>
  /// <param name="attrs"></param>
  /// <param name="propertyType"></param>
  /// <param name="componentType"></param>
  /// <param name="converter"></param>
  /// <param name="readOnly"></param>
  /// <param name="canReset"></param>
  public CommonPropertyDescriptor(
    int attributeId,
    AttributeProcessor attributeProcessor,
    string displayName,
    Attribute[] attrs,
    Type propertyType,
    Type componentType,
    TypeConverter converter,
    bool readOnly,
    bool canReset)
    : base(attributeId.ToString(), attrs)
  {
    this.attributeId = attributeId;
    this.attributeProcessor = attributeProcessor;
    this.displayName = displayName;
    this.propertyType = propertyType;
    this.componentType = componentType;
    this.converter = converter;
    this.readOnly = readOnly;
    this.canReset = canReset;
  }

  public override string DisplayName
  {
    get
    {
      if (this.displayName == null)
      {
        IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(this.attributeId, false);
        this.displayName = attributeType != null ? attributeType.Name : this.attributeId.ToString();
      }
      return this.displayName;
    }
  }

  protected virtual void InitEditor() => this.editor = (object) null;

  public override Type ComponentType => this.componentType;

  public override TypeConverter Converter => this.converter;

  public override bool IsReadOnly => this.readOnly;

  public override Type PropertyType => this.propertyType;

  public override object GetEditor(Type editorBaseType)
  {
    this.InitEditor();
    return this.editor != null ? this.editor : base.GetEditor(editorBaseType);
  }

  public override object GetValue(object component)
  {
    return this.attributeProcessor.GetValue(this.attributeId, 0);
  }

  public override void SetValue(object component, object value)
  {
    this.attributeProcessor.SetValue(this.attributeId, 0, value);
  }

  public override bool CanResetValue(object component) => this.canReset;

  public override void ResetValue(object component)
  {
  }

  public override bool ShouldSerializeValue(object component) => false;
}

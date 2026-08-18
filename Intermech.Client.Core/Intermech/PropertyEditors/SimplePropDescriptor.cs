
// Type: Intermech.PropertyEditors.SimplePropDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for SimplePropDescriptor.</summary>
public class SimplePropDescriptor : PropDescriptor
{
  private ListPropDescriptor parentListPropDescriptor;
  private AttributeValuesPropertyClass attributeValuePropertyClass;

  public ListPropDescriptor ParentListPropDescriptor
  {
    get => this.parentListPropDescriptor;
    set => this.parentListPropDescriptor = value;
  }

  public AttributeValuesPropertyClass AttributeValuePropertyClass
  {
    get
    {
      return this.ParentListPropDescriptor != null ? this.ParentListPropDescriptor.AttributeValuesPropertyClass : this.attributeValuePropertyClass;
    }
  }

  public SimplePropDescriptor(
    int propID,
    object parent,
    string name,
    object value,
    Type type,
    TypeConverter typeConverter,
    object editor,
    string category,
    string description,
    bool onlyread,
    bool browsable,
    bool reset,
    string mask,
    AttributeValuesPropertyClass aAttributeValuePropertyClass)
    : this(propID, parent, name, value, type, typeConverter, editor, category, description, onlyread, browsable, reset, mask, false, aAttributeValuePropertyClass)
  {
  }

  public SimplePropDescriptor(
    int propID,
    object parent,
    string name,
    object value,
    Type type,
    TypeConverter typeConverter,
    object editor,
    string category,
    string description,
    bool onlyread,
    bool browsable,
    bool reset,
    string mask,
    bool disableManualEdit,
    AttributeValuesPropertyClass aAttributeValuePropertyClass)
    : base(propID, parent, name, value, type, typeConverter, editor, category, description, onlyread, browsable, reset, mask, disableManualEdit)
  {
    this.attributeValuePropertyClass = aAttributeValuePropertyClass;
  }
}

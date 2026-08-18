
// Type: Intermech.PropertyEditors.FieldTypePropDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for FieldTypePropDescriptor.</summary>
public class FieldTypePropDescriptor(
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
  bool reset) : PropDescriptor(propID, parent, name, value, type, typeConverter, editor, category, description, onlyread, browsable, reset)
{
  public override AttributeCollection Attributes
  {
    get
    {
      Attribute[] attributeArray = new Attribute[this.AttributeArray.Length + 1];
      this.AttributeArray.CopyTo((Array) attributeArray, 0);
      attributeArray[attributeArray.Length - 1] = (Attribute) new RefreshPropertiesAttribute(RefreshProperties.All);
      return new AttributeCollection(attributeArray);
    }
  }
}

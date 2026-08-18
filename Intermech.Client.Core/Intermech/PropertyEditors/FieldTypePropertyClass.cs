
// Type: Intermech.PropertyEditors.FieldTypePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.PropertyEditors;

public class FieldTypePropertyClass
{
  private FieldTypes fieldType = FieldTypes.ftString;

  public FieldTypes FieldType => this.fieldType;

  public FieldTypePropertyClass(FieldTypes aFieldType) => this.fieldType = aFieldType;

  public override string ToString() => EnumDescConverter.GetEnumDescription((Enum) this.fieldType);
}

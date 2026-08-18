
// Type: Intermech.PropertyEditors.AdvSelector
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>enum для указания, что выбирать</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum AdvSelector
{
  /// <summary>Выбираем тип атрибута объекта или связи</summary>
  [CustomDescription("Attribute.Client.Core_192")] AttributeType,
  /// <summary>Выбираем тип объекта/связи</summary>
  [CustomDescription("Attribute.Client.Core_193")] AttributableType,
  /// <summary>Выбираем тип объекта/связи и тип атрибута</summary>
  [CustomDescription("Attribute.Client.Core_194")] AttributableTypeWithAttributeType,
}

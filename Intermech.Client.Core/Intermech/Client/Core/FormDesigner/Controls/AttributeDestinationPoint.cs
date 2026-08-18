
// Type: Intermech.Client.Core.FormDesigner.Controls.AttributeDestinationPoint
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Для какого родителя назначается контрол.</summary>
[TypeConverter(typeof (AttrDestinationPointConverter))]
public enum AttributeDestinationPoint
{
  /// <summary>
  /// Контрол для атрибута главного объекта(связи), если в главном объекте(связи) нет такого атрибута, то контрол для дополнительной связи (если она есть)
  /// </summary>
  Default,
  /// <summary>Контрол для дополнительной связи</summary>
  Relation,
}


// Type: Intermech.Client.Core.History.HistoryTypeEnum
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;


namespace Intermech.Client.Core.History;

/// <summary>Для чего искать историю</summary>
internal enum HistoryTypeEnum
{
  /// <summary>Для данного объекта</summary>
  [CustomDescription("Attribute.Client.Core_227")] ForObject,
  /// <summary>Для всех объектов данного типа</summary>
  [CustomDescription("Attribute.Client.Core_228")] ForSameType,
  /// <summary>Для всех объектов</summary>
  [CustomDescription("Attribute.Client.Core_229")] ForAllType,
}


// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.IRelatedObjectQueryFilterMode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

/// <summary>Интерфейс настроек фильтрации для RelatedObjectQuery</summary>
public interface IRelatedObjectQueryFilterMode
{
  /// <summary>
  /// Фильтровать набор данных после открытия.
  /// Используется для сортировки на клиенте, например в workflow (История утверждения)
  /// </summary>
  bool FilterDataTable { get; }

  /// <summary>
  /// Фильтровать набор данных в соответствии с настройками отображения составов
  /// </summary>
  bool FilterDataByVersionRule { get; }
}

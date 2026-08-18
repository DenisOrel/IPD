
// Type: Intermech.Navigator.EditingContextsCompositionLevel
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator;

/// <summary>
/// Режим добавления объектов в состав контекста редактирования
/// </summary>
public enum EditingContextsCompositionLevel
{
  /// <summary>
  /// В контекст добавляются только указанные версии объектов
  /// </summary>
  OnlyObjects,
  /// <summary>
  /// В контекст добавляются указанные версии объектов, а также их составы на один уровень вложенности
  /// </summary>
  FirstLevel,
  /// <summary>
  /// В контекст добавляются указанные версии объектов, а также их развёрнутые составы
  /// </summary>
  AllLevels,
}


// Type: Intermech.Client.Core.ManualSortingEditorMode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Client.Core;

/// <summary>
/// В каком режиме работает форма "Настройка ручной сортировки"
/// </summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Client.Core_164")]
[Category("Misc")]
public enum ManualSortingEditorMode
{
  /// <summary>
  /// Режим администратора. Позволяет настраивать порядок связей в составе.
  /// </summary>
  [CustomDescription("Attribute.Client.Core_165")] mseAdminMode = 0,
  /// <summary>Только чтение.</summary>
  [CustomDescription("Attribute.Client.Core_166")] mseReadOnly = 2,
}

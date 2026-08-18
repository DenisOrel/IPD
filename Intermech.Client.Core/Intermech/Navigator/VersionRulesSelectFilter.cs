
// Type: Intermech.Navigator.VersionRulesSelectFilter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator;

/// <summary>
/// Флажки, по которым форма выбора правил подбора версий будет фильтровать список правил
/// </summary>
[Flags]
public enum VersionRulesSelectFilter
{
  /// <summary>Отображать в списке все правила подбора версий</summary>
  vrfNone = 0,
  /// <summary>
  /// Исключить из списка персональные правила подбора версий
  /// </summary>
  vrfExcludePersonalRules = 1,
  /// <summary>Исключить из списка общие правила подбора версий</summary>
  vrfExcludeCommonRules = 2,
  /// <summary>
  /// Исключить из списка системные правила ("Все версии", "Последние версии", ...)
  /// </summary>
  vrfExcludeSystemRules = 4,
  /// <summary>
  /// Исключить из списка правила, в которых нет переменных значений критериев подбора
  /// </summary>
  vrfExcludeStaticRules = 16, // 0x00000010
  /// <summary>
  /// Исключить из списка правила, в которых есть переменные значения критериев подбора
  /// </summary>
  vrfExcludeVariableRules = 32, // 0x00000020
  /// <summary>Исключить из списка правило "Все версии объектов"</summary>
  vrfExcludeAllVersionsRule = 64, // 0x00000040
}

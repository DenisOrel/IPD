// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CurrentRuleErrors
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Ошибки для текущего правила</summary>
public enum CurrentRuleErrors
{
  /// <summary>системное правило "Все версии объектов"</summary>
  AllVersions = -2, // 0xFFFFFFFE
  /// <summary>системное правило "Последние версии объектов"</summary>
  LatestVersions = -1, // 0xFFFFFFFF
  /// <summary>правило не выбрано</summary>
  NoSelected = 0,
  /// <summary>настройки недействительны - правило было изменено</summary>
  Changed = 1,
  /// <summary>нет ошибок, правило настроено</summary>
  Valid = 2,
  /// <summary>нет вариантов значений переменных для правила</summary>
  NoVariableValue = 3,
  /// <summary>фильтрация состава выключена (obsolete)</summary>
  FilteringIsOff = 4,
  /// <summary>не указан основной вариант значений переменных</summary>
  MainVariantIsNotSpecified = 5,
  /// <summary>правило является некорректным</summary>
  Incorrect = 6,
}

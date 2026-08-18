// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.OptionValueFlags
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Набор флажков, назначенных значению опции</summary>
[Flags]
[Serializable]
public enum OptionValueFlags : long
{
  /// <summary>Никаких флажков у значения опции нет</summary>
  None = 0,
  /// <summary>
  /// Значение опции устарело, его нельзя использовать в новых объектах
  /// </summary>
  Obsolete = 1,
  /// <summary>Значение опции было восстановлено</summary>
  Recovered = 2,
  /// <summary>
  /// [ЗАРЕЗЕРВИРОВАНО]
  /// Значение опции было заблокировано от изменения
  /// (снять блокировку может только тот же пользователь, либо администратор)
  /// </summary>
  Locked = 4,
}

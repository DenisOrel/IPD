// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.OptionValueState
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Статус значения опции конфигуратора составов
/// (для реализации 972548)
/// </summary>
[Serializable]
public enum OptionValueState
{
  /// <summary>Значение установленно вручную</summary>
  Custom = -1, // 0xFFFFFFFF
  /// <summary>Значение выбрано как связанное</summary>
  Linked = 0,
  /// <summary>Значение по умолчанию</summary>
  Default = 1,
}

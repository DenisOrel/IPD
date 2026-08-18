// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.NoteFieldOptions
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>
/// Перечислитель, позволяющий определить дополнительные настройки для
/// атрибутов, отображаемых в примечаниях спецификаций
/// </summary>
[Flags]
public enum NoteFieldOptions
{
  /// <summary>Никаких опций нет</summary>
  [Description("Никаких опций нет")] None = 0,
  /// <summary>
  /// Отображать единицы измерения для значений атрибута "Количество" в примечаниях
  /// </summary>
  [Description("Отображать единицы измерения для значений атрибута \"Количество\" в примечаниях")] ShowMeasureUnits = 1,
  /// <summary>Значения опций по умолчанию</summary>
  [Description("Значения опций по умолчанию")] Default = ShowMeasureUnits, // 0x00000001
}

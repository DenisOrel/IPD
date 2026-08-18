// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.RelFilter
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Флаги состояния задачи визуализатора</summary>
[TypeConverter(typeof (EnumDescConverter))]
[Category("Visualizer")]
[Flags]
[Serializable]
public enum RelFilter
{
  /// <summary>Показывать и структурные, и ассоциативные связи</summary>
  ShowAll = 0,
  /// <summary>Показывать только структурные связи</summary>
  OnlyStruct = 1,
  /// <summary>Показывать только ассоциативные связи</summary>
  OnlyAssoc = 2,
}

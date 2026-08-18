// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.CompositionAttributeState
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Состояния сравниваемых атрибутов</summary>
public enum CompositionAttributeState
{
  None = 0,
  /// <summary>Атрибут равен сравниваемому</summary>
  Equal = 1,
  /// <summary>Атрибут был добавлен</summary>
  Added = 2,
  /// <summary>Атрибут был удален</summary>
  Removed = 4,
  /// <summary>Атрибут был изменен</summary>
  Changed = 8,
  /// <summary>Пустышка</summary>
  Dummy = 16, // 0x00000010
}

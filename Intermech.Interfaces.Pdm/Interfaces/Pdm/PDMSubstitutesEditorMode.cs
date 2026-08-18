// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.PDMSubstitutesEditorMode
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Перечислитель, в котором указывается режим работы редактора допустимых замен
/// </summary>
[Flags]
public enum PDMSubstitutesEditorMode : long
{
  /// <summary>Стандартный режим работы</summary>
  Default = 0,
  /// <summary>
  /// Режим автоматического внесения изменений в исполнения с отображением диалогового окна
  /// (вызывается из редактора спецификаций формы "Б")
  /// </summary>
  DialogMultiInstances = 1,
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Project.EditingModeExtension
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using System;

#nullable disable
namespace Intermech.Project;

public static class EditingModeExtension
{
  /// <summary>Возвращает true, если разрешено какое-либо редактирование (свойства, или состав, или оба)</summary>
  public static bool Any(this EditingMode mode) => mode != 0;

  /// <summary>Возвращает true, если редактирование запрещено</summary>
  public static bool ReadOnly(this EditingMode mode) => mode == EditingMode.None;

  /// <summary>Есть ли флаг EditingMode.Properties в списке значений</summary>
  public static bool HasProperties(this EditingMode mode)
  {
    return mode.HasFlag((Enum) EditingMode.Properties);
  }

  /// <summary>Есть ли флаг EditingMode.Composition в списке значений</summary>
  public static bool HasComposition(this EditingMode mode)
  {
    return mode.HasFlag((Enum) EditingMode.Composition);
  }
}

// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBNullSaveMode
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Experimental.Kernel.Entities;

/// <summary>
/// Описывает варианты преобразования значения свойства доменного объекта в DBNull при записи в базу данных.
/// </summary>
internal enum DBNullSaveMode
{
  /// <summary>
  /// Неприменимо, так как тип свойства не допускает null и пустых значений
  /// </summary>
  NotApplicable,
  /// <summary>
  /// Не поддерживается, так как настройки атрибута IPS не допускают записи пустых значений
  /// </summary>
  NotSupported,
  /// <summary>null и пустое значение преобразуется в DBNull</summary>
  DBNull,
}

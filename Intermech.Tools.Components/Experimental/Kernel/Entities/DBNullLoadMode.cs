// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBNullLoadMode
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Experimental.Kernel.Entities;

/// <summary>
/// Описывает варианты преобразования DBNull в значение свойства доменного объекта при чтении из базы данных.
/// </summary>
internal enum DBNullLoadMode
{
  /// <summary>
  /// DBNull не может быть прочитан из базы данных, так как настройки атрибута IPS не допускают этого.
  /// </summary>
  NotApplicable,
  /// <summary>
  /// DBNull преобразуется в null. Этот режим используется для свойств ссылочных типов без пустого значения и свойств nullable типов.
  /// </summary>
  NullValue,
  /// <summary>
  /// DBNull преобразуется в пустое значение. Этот режим используется для свойств ссылочных типов, у которых есть пустое значение.
  /// </summary>
  EmptyValue,
  /// <summary>
  /// DBNull преобразуется в значение по умолчанию. Этот режим используется для свойств, чей тип не допускает null и пустых значений
  /// </summary>
  DefaultValue,
}

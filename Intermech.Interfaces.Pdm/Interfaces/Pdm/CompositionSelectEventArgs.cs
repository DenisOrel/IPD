// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.CompositionSelectEventArgs
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Kernel.Search;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Описывает объект для которого необходимо получить состав/применяемости
/// </summary>
public class CompositionSelectEventArgs
{
  /// <summary>
  /// ID схемы поиска или ссылка на виртуальную схему (RuntimeSearchScheme)
  /// </summary>
  private object SchemeID;
  /// <summary>Колонки</summary>
  private List<ColumnDescriptor> Columns;
  /// <summary>Событие обработано</summary>
  public bool Handled;

  /// <summary>Создать аргументы события</summary>
  /// <param name="scheme">ID схемы поиска или ссылка на виртуальную схему (RuntimeSearchScheme)</param>
  /// <param name="columns">Описания колонок</param>
  public CompositionSelectEventArgs(object scheme, List<ColumnDescriptor> columns)
  {
    this.Columns = columns;
    this.SchemeID = scheme;
    this.Handled = false;
  }
}

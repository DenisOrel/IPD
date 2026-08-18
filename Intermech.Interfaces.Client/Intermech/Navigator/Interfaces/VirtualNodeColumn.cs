// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.VirtualNodeColumn
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Потомок NodeColumn для отображения в нём "виртуальных значений", то есть значений, не являющихся отображением реально существующих атрибутов
/// (значение заполняется уже на клиенте)
/// 
/// Главное отличие от базового класса на данный момент - отключенная возможность сортировки по этому полю
/// Потому как для поэтапной загрузки запросов сортировка по ним не будет работать.
/// Как вариант - сортировать всё на клиенте и запрещать поэтапную загрузку результатов запроса, но это страшный грех.</summary>
[Serializable]
public class VirtualNodeColumn : 
  NodeColumn,
  IAssignable,
  ICloneable,
  IComparable,
  IComparable<NodeColumn>,
  IColumnAttributeInfo
{
  /// <summary>Создать колонку, заполнить её информацией из указанного объекта-источника</summary>
  /// <param name="source">Объект-источник</param>
  public VirtualNodeColumn(object source)
    : base(source)
  {
    this._disableSorting = true;
    this._sortOrder = NodeColumnSortOrder.None;
    this._sortIndex = -1;
  }

  /// <summary>Конструктор, позволяющий создать колонку без сортировки.</summary>
  /// <param name="schemeGuid">Идентификатор схемы колонки</param>
  /// <param name="id">Идентификатор колонки</param>
  /// <param name="dataType">Тип данных, содержащихся в колонке</param>
  /// <param name="attrType">Тип данных атрибута, связанного с колонкой</param>
  /// <param name="caption">Заголовок колонки</param>
  public VirtualNodeColumn(
    Guid schemeGuid,
    object id,
    Type dataType,
    FieldTypes attrType,
    string caption)
    : base(schemeGuid, id, dataType, attrType, caption)
  {
    this._disableSorting = true;
    this._sortOrder = NodeColumnSortOrder.None;
    this._sortIndex = -1;
  }

  /// <summary>Возвращает или устанавливает направление сортировки данных в колонке</summary>
  public override NodeColumnSortOrder SortOrder
  {
    [DebuggerStepThrough] get => NodeColumnSortOrder.None;
    set
    {
    }
  }

  /// <summary>Порядковый номер колонки в списке сортируемых колонок или -1, если колонка не участвует в сортировке.</summary>
  public override int SortIndex
  {
    [DebuggerStepThrough] get => -1;
    set
    {
    }
  }

  /// <summary>Запрет сортировки по данной колонке</summary>
  public override bool DisableSorting
  {
    [DebuggerStepThrough] get => true;
    set
    {
    }
  }

  /// <summary>Создать копию объекта, идентичную натуральному</summary>
  /// <returns>Копия объекта, идентичная натуральному</returns>
  public override object Clone() => (object) new VirtualNodeColumn((object) this);
}

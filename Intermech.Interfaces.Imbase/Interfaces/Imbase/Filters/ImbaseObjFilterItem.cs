// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Filters.ImbaseObjFilterItem
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Expert;
using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Imbase.Filters;

/// <summary>Элемент фильтра</summary>
[Serializable]
public class ImbaseObjFilterItem : ICloneable, IComparable, IComparable<ImbaseObjFilterItem>
{
  /// <summary>Условие ЭС</summary>
  private TempFormula _condition;
  /// <summary>Таблица с фильтрами Iмбайсе</summary>
  private DataTable _filterData;
  /// <summary>Порядковый номер объекта в списке</summary>
  private long _order;
  /// <summary>Расширенная информация об объекте</summary>
  /// <remarks>В частности для серверного кеша</remarks>
  [NonSerialized]
  private object _extInfo;

  /// <summary>Конструктор</summary>
  /// <param name="condition"></param>
  public ImbaseObjFilterItem(TempFormula condition)
    : this(condition, (DataTable) null)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="condition"></param>
  /// <param name="filterData"></param>
  public ImbaseObjFilterItem(TempFormula condition, DataTable filterData)
    : this(condition, filterData, 0L)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="condition"></param>
  /// <param name="filterData"></param>
  /// <param name="order"></param>
  public ImbaseObjFilterItem(TempFormula condition, DataTable filterData, long order)
  {
    this._condition = condition;
    this._filterData = filterData;
    this._order = order;
  }

  /// <summary>Создание клона объекта</summary>
  /// <returns></returns>
  public object Clone()
  {
    ImbaseObjFilterItem imbaseObjFilterItem = new ImbaseObjFilterItem((TempFormula) null)
    {
      Order = this.Order
    };
    if (this.Condition != null)
      imbaseObjFilterItem.Condition = this.Condition.Clone() as TempFormula;
    if (this.FilterData != null)
      imbaseObjFilterItem.FilterData = this.FilterData.Copy();
    return (object) imbaseObjFilterItem;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public int CompareTo(object obj) => this.CompareTo(obj as ImbaseObjFilterItem);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public int CompareTo(ImbaseObjFilterItem other)
  {
    return other == null ? -1 : this._order.CompareTo(other.Order);
  }

  /// <summary>Условие ЭС</summary>
  public TempFormula Condition
  {
    [DebuggerStepThrough] get => this._condition;
    [DebuggerStepThrough] set => this._condition = value;
  }

  /// <summary>Таблица с фильтрами Imbase</summary>
  public DataTable FilterData
  {
    [DebuggerStepThrough] get => this._filterData;
    [DebuggerStepThrough] set => this._filterData = value;
  }

  /// <summary>Порядковый номер объекта в списке</summary>
  public long Order
  {
    [DebuggerStepThrough] get => this._order;
    [DebuggerStepThrough] set => this._order = value;
  }

  /// <summary>Расширенная информация об объекте</summary>
  public object ExtInfo
  {
    [DebuggerStepThrough] get => this._extInfo;
    [DebuggerStepThrough] set => this._extInfo = value;
  }

  /// <summary>Создание таблицы для фильтра</summary>
  /// <param name="tableName">Имя таблицы</param>
  /// <returns></returns>
  public static DataTable CreateFilterTable(string tableName)
  {
    return new DataTable(tableName)
    {
      Columns = {
        {
          "F_GUID",
          typeof (string)
        },
        {
          "F_OWNER",
          typeof (string)
        },
        {
          "F_PATH",
          typeof (string)
        }
      },
      RemotingFormat = SerializationFormat.Binary
    };
  }
}

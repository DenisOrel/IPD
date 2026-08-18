// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.IExpertTable
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Expert.Table;
using Intermech.Interfaces;
using System.Collections;

#nullable disable
namespace Intermech.Expert;

/// <summary>Интерфейс для объектов-таблиц экспертной системы</summary>
public interface IExpertTable : 
  IExpertObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  /// <summary>Кол-во входов в таблице (0,1,2)</summary>
  int EntrysCount { get; set; }

  /// <summary>Сохраняет данные о таблицах в блобе</summary>
  /// <param name="tableCollection">Коллекция таблиц</param>
  void SaveTableData(eTableCollection tableCollection);

  /// <summary>Загружает коллекцию таблиц</summary>
  /// <returns></returns>
  eTableCollection LoadTableData();

  /// <summary>Сохраняет условие в базу</summary>
  void SaveCondition();

  /// <summary>Имя объекта экспертной системы</summary>
  string esName { get; set; }

  /// <summary>Количество слоев</summary>
  int LayersCount { get; set; }

  /// <summary>Клоичество столбцов</summary>
  int ColumnsCount { get; set; }

  /// <summary>Количество строк</summary>
  int RowsCount { get; set; }

  /// <summary>Список ролей атрибутов</summary>
  IList Roles { get; set; }

  /// <summary>Список атрибутов</summary>
  IList AttributesList { get; set; }

  /// <summary>Список типов объектов</summary>
  IList ObjectTypesList { get; set; }

  /// <summary>Список ссылок на объекты</summary>
  IList ObjectLinksList { get; set; }
}

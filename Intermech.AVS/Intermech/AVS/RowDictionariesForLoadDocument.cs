// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RowDictionariesForLoadDocument
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

internal class RowDictionariesForLoadDocument
{
  /// <summary>Словарь записей по индексу сортировки</summary>
  public Dictionary<long, AVSRow> specRowsBySortIndex;
  /// <summary>Словарь строк документа по guid связи</summary>
  public Dictionary<Guid, TableData> docRowsByGuid;
  /// <summary>Словарь строк документа по индексу сортировки</summary>
  public Dictionary<long, List<TableData>> docRowsBySortIndex;
  /// <summary>Словарь строк документа по глобальному идентификатору объектов</summary>
  public Dictionary<Guid, List<TableData>> docRowsByObjectGuid;
  /// <summary>Словарь строк документа без связей по глобальному идентификатору объектов</summary>
  public Dictionary<Guid, List<TableData>> docRowsWithoutRelationsByObjectGuid;
  /// <summary>Список записей документа по ObjectIdIzd</summary>
  public Dictionary<long, List<TableData>> docRowsByObjectID;
  /// <summary>Список объектов полученный из файла старого формата</summary>
  public List<long> objectsFromOldFormat;
  /// <summary>Список типов объектов полученный из файла старого формата</summary>
  public List<int> objectTypesFromOldFormat;

  public RowDictionariesForLoadDocument(Dictionary<long, AVSRow> specRowsBySortIndex = null)
  {
    this.specRowsBySortIndex = specRowsBySortIndex == null ? new Dictionary<long, AVSRow>() : specRowsBySortIndex;
    this.docRowsByGuid = new Dictionary<Guid, TableData>();
    this.docRowsBySortIndex = new Dictionary<long, List<TableData>>();
    this.docRowsByObjectGuid = new Dictionary<Guid, List<TableData>>();
    this.docRowsWithoutRelationsByObjectGuid = new Dictionary<Guid, List<TableData>>();
    this.docRowsByObjectID = new Dictionary<long, List<TableData>>();
    this.objectsFromOldFormat = new List<long>();
    this.objectTypesFromOldFormat = new List<int>();
  }
}

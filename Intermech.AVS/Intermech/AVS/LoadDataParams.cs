// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.LoadDataParams
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary>Параметры загрузки данных из БД</summary>
internal class LoadDataParams
{
  /// <summary>Параметры запроса</summary>
  public DBRecordSetParams SelectParamSet;
  /// <summary>Загружать данные связей и объектов. Если false, то загружать данные только объектов</summary>
  public bool LoadRelations;
  /// <summary>Индекс исполнения</summary>
  public int ProductIndex = -1;
  /// <summary>Родительское изделие</summary>
  public ProductInfo Product;
  /// <summary>Тип связей</summary>
  public int RelationType;
  /// <summary>Типы объектов</summary>
  public List<int> ObjectTypes;
  /// <summary>Пропускать документы без связи с разделом</summary>
  public bool SkipUnknownDoc;
  /// <summary>Сортировать все</summary>
  public bool SortAll;
  /// <summary>Создавать новые записи</summary>
  public bool CreateNewRecords;
  /// <summary>Контекст вызова метода</summary>
  public AVSDocumentContext Context;
  /// <summary>Сессия</summary>
  public IUserSession Session;
  /// <summary>Словарь загруженных связей. Если null, то не заполняется</summary>
  public Dictionary<long, AVSRow> LoadedRelations;
  /// <summary>Список загруженных записей. Если null, то не заполняется</summary>
  public List<AVSRow> LoadedSpecRows;
  /// <summary>Словарь строк документа по Guid. Если null, то не используется</summary>
  public Dictionary<Guid, TableData> DocRowsByGuid;
  /// <summary>Словарь строк документа по Guid для экспортной таблицы. Если null, то не используется</summary>
  public Dictionary<Guid, TableData> ExpDocRowsByGuid;
  /// <summary>Словарь записей по индексу сортировки. Если null, то не используется</summary>
  public Dictionary<long, AVSRow> SpecRowsBySortIndex;
  /// <summary>Словарь строк документа по индексу сортировки. Если null, то не используется</summary>
  public Dictionary<long, List<TableData>> DocRowsBySortIndex;
  /// <summary>Словарь строк документа по индексу сортировки для экспортной таблицы. Если null, то не используется</summary>
  public Dictionary<long, List<TableData>> ExpDocRowsBySortIndex;
  /// <summary>Словарь строк документа по идентификатору версии объекта. null, если не используется</summary>
  public Dictionary<long, List<TableData>> DocRowsByObjectID;
  /// <summary>Словарь строк документа по идентификатору версии объекта для экспортной таблицы. null, если не используется</summary>
  public Dictionary<long, List<TableData>> ExpDocRowsByObjectID;
  /// <summary>Словарь строк документа по глобальному идентификатору версии объекта. null, если не используется</summary>
  public Dictionary<Guid, List<TableData>> DocRowsByObjectGuid;
  /// <summary>Словарь строк документа по глобальному идентификатору версии объекта для экспортной таблицы. null, если не используется</summary>
  public Dictionary<Guid, List<TableData>> ExpDocRowsByObjectGuid;

  /// <summary>Конструктор</summary>
  /// <param name="paramSet">Параметры запроса</param>
  /// <param name="loadRelations">Загружать данные связей и объектов. Если false, то загружать данные только объектов</param>
  /// <param name="product">Индекс исполнения</param>
  /// <param name="relationType">Тип связей</param>
  /// <param name="objectTypes">Типы объектов</param>
  /// <param name="skipUnknownDoc">Пропускать документы без связи с разделом</param>
  /// <param name="sortAll">Сортировать все</param>
  /// <param name="createNewRecords">Создавать новые записи</param>
  /// <param name="context">Контекст вызова метода</param>
  /// <param name="session">Сессия</param>
  /// <param name="loadedRelations">Словарь загруженных связей. Если null, то не заполняется</param>
  /// <param name="loadedSpecRows">Список загруженных записей. Если null, то не заполняется</param>
  /// <param name="rowDicts">Словари строк документа</param>
  internal LoadDataParams(
    DBRecordSetParams selectParamSet,
    bool loadRelations,
    ProductInfo product,
    int relationType,
    List<int> objectTypes,
    bool skipUnknownDoc,
    bool sortAll,
    bool createNewRecords,
    AVSDocumentContext context,
    IUserSession session,
    Dictionary<long, AVSRow> loadedRelations,
    List<AVSRow> loadedSpecRows,
    RowDictionariesForLoadDocument rowDicts)
  {
    this.SelectParamSet = selectParamSet;
    this.LoadRelations = loadRelations;
    this.Product = product;
    this.ProductIndex = -1;
    this.RelationType = relationType;
    this.ObjectTypes = objectTypes;
    this.SkipUnknownDoc = skipUnknownDoc;
    this.SortAll = sortAll;
    this.CreateNewRecords = createNewRecords;
    this.Context = context;
    this.Session = session;
    this.LoadedRelations = loadedRelations;
    this.LoadedSpecRows = loadedSpecRows;
    this.DocRowsByGuid = rowDicts.docRowsByGuid;
    this.DocRowsBySortIndex = rowDicts.docRowsBySortIndex;
    this.DocRowsByObjectID = rowDicts.docRowsByObjectID;
    if (rowDicts.specRowsBySortIndex == null)
      this.SpecRowsBySortIndex = new Dictionary<long, AVSRow>();
    else
      this.SpecRowsBySortIndex = rowDicts.specRowsBySortIndex;
  }

  public bool IsDocRelation => this.RelationType == AvsIDCache.Relation_Document;
}

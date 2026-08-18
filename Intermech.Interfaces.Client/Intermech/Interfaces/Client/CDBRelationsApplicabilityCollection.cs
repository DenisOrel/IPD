// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CDBRelationsApplicabilityCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Коллекция допустимых связей объектов данного типа с объектами различных типов.
/// Прокси-класс для реализации IDBRelationsApplicabilityCollection на стороне клиента.
/// </summary>
internal class CDBRelationsApplicabilityCollection : 
  CacheObjectsCollection,
  IDBRelationsApplicabilityCollection,
  IIDBRelationsApplicabilityCollectionSSI
{
  /// <summary>Если true, то фильтровать по предметным областям</summary>
  protected bool _AreaSupport;
  /// <summary>Если true, то фильтровать по языковым вариантам</summary>
  protected bool _LanguageSupport;

  /// <summary>Создать экземпляр прокси-класса</summary>
  /// <param name="uSession">Клиентская сессия</param>
  public CDBRelationsApplicabilityCollection(ClientSession uSession)
    : base(uSession, false)
  {
    this._AreaSupport = false;
    this._LanguageSupport = false;
    this.InitOptions("IMS_TYPES_APPLICABILITY", "F_APPLICABILITY_ID");
  }

  /// <summary>
  /// Интерфейс IDBRelationsApplicability с серверной стороны
  /// </summary>
  public IDBRelationsApplicabilityCollection ServerSideIntf
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      IUserSession userSession = (IUserSession) this._clientSession;
      IClientSession clientSession = (IClientSession) this._clientSession;
      if (clientSession != null)
        userSession = clientSession.Session;
      return userSession.GetRelationsApplicabilityCollection();
    }
  }

  /// <summary>
  /// Возвращает таблицу со списком допустимых применяемостей связями типа relationType
  /// объектов типа objectType в объектах типа inObjectType.
  /// Если relationType меньше 0, то вернется список типов связей, которыми объекты типа objectType применяются в объектах типа inObjectType.
  /// Если objectType   меньше 0, то вернется спиcок всех применяемости всех типов в типе inObjectType.
  /// Если inObjectType меньше 0, то вернется список входимостей объектов типа objectType.
  /// 
  /// Если допустимых применяемостей нет, то возвращается DataTable с количеством записей 0
  /// </summary>
  /// <returns>Таблица со списком допустимых применяемостей связями типа relationType</returns>
  public DataTable GetApplicabilitiesList(int relationType, int objectType, int inObjectType)
  {
    this._clientSession.Guard.ValidateCall();
    if (objectType < 0 && inObjectType < 0)
      throw new ApplicationException(LocalizationHolder.rm.GetString("Interfaces.Client_7"));
    MyCompositeKey key = new MyCompositeKey(new object[3]
    {
      (object) relationType,
      (object) objectType,
      (object) inObjectType
    });
    DataTable fromTable = CDBRelationsApplicabilityCache.TryGet(key);
    if (fromTable != null)
      return DataSetProcessor.CopyTable(fromTable);
    DataTable table = this._clientSession.ClientCache.GetTable("IMS_TYPES_APPLICABILITY");
    DataTable applicabilitiesList = table.Clone();
    StringBuilder stringBuilder = new StringBuilder();
    if (relationType > -1)
      stringBuilder.AppendFormat("F_RELATION_TYPE = {0}", (object) relationType);
    ArrayList objsTreeList1 = new ArrayList();
    ArrayList objsTreeList2 = new ArrayList();
    ArrayList arrayList = (ArrayList) null;
    if (objectType > -1)
    {
      if (stringBuilder.Length > 0)
        stringBuilder.Append(" AND ");
      stringBuilder.Append("F_OBJECT_TYPE = {0}");
      СObjectType.CachedFillParentsArray((IClientSession) this._clientSession, objectType, objsTreeList1);
      arrayList = objsTreeList1;
    }
    if (inObjectType > -1)
    {
      if (stringBuilder.Length > 0)
        stringBuilder.Append(" AND ");
      if (objectType > -1)
        stringBuilder.Append("F_INOBJECT_TYPE = {1}");
      else
        stringBuilder.Append("F_INOBJECT_TYPE = {0}");
      СObjectType.CachedFillParentsArray((IClientSession) this._clientSession, inObjectType, objsTreeList2);
      arrayList = objsTreeList2;
    }
    if (objectType > -1 && inObjectType > -1)
    {
      for (int index1 = 0; index1 < objsTreeList1.Count; ++index1)
      {
        for (int index2 = 0; index2 < objsTreeList2.Count; ++index2)
        {
          DataRow[] dataRowArray = table.Select(string.Format(stringBuilder.ToString(), objsTreeList1[index1], objsTreeList2[index2]));
          if (dataRowArray.Length != 0)
          {
            foreach (DataRow fromRow in dataRowArray)
            {
              DataSetProcessor.AssignRow(applicabilitiesList, fromRow, applicabilitiesList.Rows.Count + 1, true);
              if (index1 > 0 || index2 > 0)
              {
                applicabilitiesList.Rows[applicabilitiesList.Rows.Count - 1]["F_PUBLIC"] = (object) Convert.ToInt32((object) InheritModes.Inherited);
                applicabilitiesList.AcceptChanges();
              }
              if ((Convert.ToInt32(fromRow["F_OPTIONS"]) & 2) == 2)
              {
                while (applicabilitiesList.Rows.Count > 1)
                  applicabilitiesList.Rows.RemoveAt(0);
                return applicabilitiesList;
              }
            }
          }
        }
      }
      while (applicabilitiesList.Rows.Count > 1)
        applicabilitiesList.Rows.RemoveAt(applicabilitiesList.Rows.Count - 1);
    }
    else
    {
      for (int index3 = 0; index3 < arrayList.Count; ++index3)
      {
        DataRow[] fromRows = table.Select(string.Format(stringBuilder.ToString(), arrayList[index3]));
        if (inObjectType > -1 && index3 > 0)
        {
          if (fromRows.Length != 0)
          {
            foreach (DataRow dataRow in fromRows)
            {
              bool flag = false;
              for (int index4 = 0; index4 < applicabilitiesList.Rows.Count; ++index4)
              {
                if (MetaDataHelper.IsObjectTypeChildOf(Convert.ToInt32(dataRow["F_OBJECT_TYPE"]), Convert.ToInt32(applicabilitiesList.Rows[index4]["F_OBJECT_TYPE"])))
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
              {
                DataRow row = applicabilitiesList.NewRow();
                for (int columnIndex = 0; columnIndex < applicabilitiesList.Columns.Count; ++columnIndex)
                  row[columnIndex] = dataRow[columnIndex];
                applicabilitiesList.Rows.Add(row);
              }
            }
          }
          else
            continue;
        }
        else if (objectType > -1 && index3 > 0)
        {
          int columnIndex1 = applicabilitiesList.Columns.IndexOf("F_INOBJECT_TYPE");
          foreach (DataRow dataRow in fromRows)
          {
            bool flag = false;
            for (int index5 = 0; index5 < applicabilitiesList.Rows.Count; ++index5)
            {
              if (Convert.ToInt32(dataRow[columnIndex1]) == Convert.ToInt32(applicabilitiesList.Rows[index5][columnIndex1]))
              {
                flag = true;
                break;
              }
            }
            if (!flag)
            {
              DataRow row = applicabilitiesList.NewRow();
              for (int columnIndex2 = 0; columnIndex2 < applicabilitiesList.Columns.Count; ++columnIndex2)
                row[columnIndex2] = dataRow[columnIndex2];
              applicabilitiesList.Rows.Add(row);
            }
          }
          applicabilitiesList.AcceptChanges();
        }
        else
          DataSetProcessor.AssignRows(applicabilitiesList, (IEnumerable<DataRow>) fromRows);
        foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
        {
          if (index3 == 0)
            row["F_PUBLIC"] = (object) Convert.ToInt32((object) InheritModes.Public);
          else if (Convert.ToInt32(row["F_PUBLIC"]) == Convert.ToInt32((object) InheritModes.Private))
            row["F_PUBLIC"] = (object) Convert.ToInt32((object) InheritModes.Inherited);
        }
      }
      foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
      {
        if (Convert.ToInt32(row["F_PUBLIC"]) == Convert.ToInt32((object) InheritModes.Public))
          row["F_PUBLIC"] = (object) Convert.ToInt32((object) InheritModes.Private);
      }
      applicabilitiesList.AcceptChanges();
    }
    CDBRelationsApplicabilityCache.Update(key, applicabilitiesList);
    return applicabilitiesList;
  }

  /// <summary>
  /// Возвращает объект-описатель допустимости связи типа relationType между объектами
  /// типов objectType и inObjectType. Если таковая применяемость не настроена, то
  /// метод возвращает null.
  /// </summary>
  /// <param name="relationType">Тип связи</param>
  /// <param name="objectType">Дочерний тип объекта</param>
  /// <param name="inObjectType">Родительский тип объекта</param>
  /// <returns>Описание входимости одного типа в другой</returns>
  public IDBRelationsApplicability GetApplicability(
    int relationType,
    int objectType,
    int inObjectType)
  {
    this._clientSession.Guard.ValidateCall();
    DataTable applicabilitiesList = this.GetApplicabilitiesList(relationType, objectType, inObjectType);
    return applicabilitiesList.Rows.Count == 0 ? (IDBRelationsApplicability) null : this.GetApplicability(Convert.ToInt32(applicabilitiesList.Rows[0]["F_APPLICABILITY_ID"]));
  }

  /// <summary>
  /// Возвращает объект-описатель допустимости связи номер applicabilityID
  /// </summary>
  /// <param name="applicabilityID">Идентификатор контекста связи</param>
  /// <returns>Описание входимости одного объекта в другой</returns>
  public IDBRelationsApplicability GetApplicability(int applicabilityID)
  {
    this._clientSession.Guard.ValidateCall();
    return (IDBRelationsApplicability) new CDBRelationsApplicability(this._clientSession, applicabilityID);
  }

  /// <summary>
  /// Создает новый контекст связи на основании структуры applicabilityProperties и
  /// возвращает его идентификатор.
  /// </summary>
  /// <param name="applicabilityProperties">Свойства входимости</param>
  /// <returns>Идентификатор нового контекста связи</returns>
  public int Create(
    RelationsApplicabilityProperties applicabilityProperties)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this.ServerSideIntf.Create(applicabilityProperties);
    this.ReloadCache(0);
    CDBRelationsApplicabilityCache.Reset();
    return num;
  }

  public DataRow GetApplicabilityRow(int relationType, int objectType, int inObjectType)
  {
    this._clientSession.Guard.ValidateCall();
    DataTable applicabilitiesList = this.GetApplicabilitiesList(relationType, objectType, inObjectType);
    return applicabilitiesList.Rows.Count == 0 ? (DataRow) null : applicabilitiesList.Rows[0];
  }
}

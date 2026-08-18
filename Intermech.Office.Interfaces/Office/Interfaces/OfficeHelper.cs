// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.OfficeHelper
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Remoting;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Набор вспомогательных статических методов</summary>
public class OfficeHelper
{
  [CanBeNull]
  public static IDBObject GetOfficeDoc([NotNull] IUserSession session, int objType, long objId, long id)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(OfficeConsts.ReltypeOfficeCompositionID);
    return OfficeHelper.GetOfficeDoc(session, relationCollection, objType, objId, id);
  }

  /// <summary>Получить идентификатор канцелярской связи, по которой поручение либо принадлежит документ (для корневых поручений), либо
  /// входит в другое поручение</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="resolutionObjectVersionID">Идентификатор версии поручения</param>
  /// <param name="resolutionObjectID">Идентификатор объекта (!!! НЕ ВЕРСИИ !!!) поручения, если он известен</param>
  /// <param name="relColl"></param>
  /// <returns>Идентификатор связи</returns>
  [CanBeNull]
  public static ResolutionContextInfo GetResolutionContextInfo(
    [NotNull] IUserSession session,
    long resolutionObjectVersionID,
    long resolutionObjectID = 0,
    [CanBeNull] IDBRelationCollection relColl = null)
  {
    if (resolutionObjectID == 0L)
      resolutionObjectID = session.GetObjectF_ID(resolutionObjectVersionID);
    relColl = relColl ?? session.GetRelationCollection(OfficeConsts.ReltypeOfficeCompositionID);
    RemotingCallContext.SetData("X-IPS-NoFilterQuery", "true");
    DataTable dataTable;
    try
    {
      dataTable = relColl.EntersIn(new DBRecordSetParams((ConditionStructure[]) null, new object[3]
      {
        (object) -20,
        (object) -7,
        (object) -2
      }), resolutionObjectID, false);
    }
    finally
    {
      RemotingCallContext.FreeNamedDataSlot("X-IPS-NoFilterQuery");
    }
    if (dataTable.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        int int32 = Convert.ToInt32(row[1]);
        if (int32 == OfficeConsts.ObjtypeDocumentsID || MetaDataHelper.IsObjectTypeChildOf(int32, OfficeConsts.ObjtypeDocumentsID))
          return new ResolutionContextInfo(resolutionObjectVersionID, Convert.ToInt64(row[0]), ResolutionParentType.Document, int32, Convert.ToInt64(row[2]));
        if (int32 == OfficeConsts.ObjtypeResolutionsID || MetaDataHelper.IsObjectTypeChildOf(int32, OfficeConsts.ObjtypeResolutionsID))
          return new ResolutionContextInfo(resolutionObjectVersionID, Convert.ToInt64(row[0]), ResolutionParentType.Resolution, int32, Convert.ToInt64(row[2]));
      }
    }
    return (ResolutionContextInfo) null;
  }

  /// <summary>Получить интерфейс документа, которому принадлежит поручение. Работает рекурсивно, то есть для вложенных поручений "докопается" до корневого, а потом и документа</summary>
  [CanBeNull]
  private static IDBObject GetOfficeDoc(
    [NotNull] IUserSession session,
    [NotNull] IDBRelationCollection relColl,
    int objType,
    long objId,
    long id)
  {
    RemotingCallContext.SetData("X-IPS-NoFilterQuery", "true");
    DataTable dataTable;
    try
    {
      dataTable = relColl.EntersIn(new DBRecordSetParams((ConditionStructure[]) null, new object[3]
      {
        (object) -7,
        (object) -2,
        (object) -3
      }), id, false);
    }
    finally
    {
      RemotingCallContext.FreeNamedDataSlot("X-IPS-NoFilterQuery");
    }
    if (dataTable.Rows.Count == 0)
      return objType != OfficeConsts.ObjtypeResolutionsID ? session.GetObject(objId) : (IDBObject) null;
    int int32 = Convert.ToInt32(dataTable.Rows[0][0]);
    return int32 != OfficeConsts.ObjtypeResolutionsID ? session.GetObject(Convert.ToInt64(dataTable.Rows[0][1])) : OfficeHelper.GetOfficeDoc(session, relColl, int32, Convert.ToInt64(dataTable.Rows[0][1]), Convert.ToInt64(dataTable.Rows[0][2]));
  }

  /// <summary>Функция возвращает канцелярский документ для поручения.</summary>
  public static long FindOfficeDocument([NotNull] IUserSession session, long resolutionID)
  {
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(OfficeConsts.ObjtypeResolutionsID);
    return OfficeHelper.FindOfficeDocumentLevel(session.GetRelationCollection(OfficeConsts.ReltypeOfficeCompositionID), resolutionID, childrenIdRecursive);
  }

  private static long FindOfficeDocumentLevel(
    [NotNull] IDBRelationCollection relColl,
    long childResolutionID,
    [NotNull] List<int> resolutionTypes)
  {
    RemotingCallContext.SetData("X-IPS-NoFilterQuery", "true");
    DataTable dataTable;
    try
    {
      dataTable = relColl.EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
      {
        (object) -7,
        (object) -2
      }), childResolutionID);
    }
    finally
    {
      RemotingCallContext.FreeNamedDataSlot("X-IPS-NoFilterQuery");
    }
    if (dataTable.Rows.Count <= 0)
      return 0;
    return !resolutionTypes.Contains(Convert.ToInt32(dataTable.Rows[0][0])) ? Convert.ToInt64(dataTable.Rows[0][1]) : OfficeHelper.FindOfficeDocumentLevel(relColl, Convert.ToInt64(dataTable.Rows[0][1]), resolutionTypes);
  }
}

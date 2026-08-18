// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseClientHelper
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Imbase;

public static class ImbaseClientHelper
{
  public static NodeIDPath CreatePathToImbaseObject(IUserSession session, long imbaseObjID)
  {
    Guid sessionGuid = session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService ? session.SessionGUID : throw new ApplicationException(LocalizationHolder.rm.GetString("ImbaseServer_Null"));
    long[] objectList = new long[2]
    {
      imbaseObjID,
      -imbaseObjID
    };
    DataTable foldersForObjects = customService.GetFoldersForObjects(sessionGuid, objectList, (long[]) null);
    if (foldersForObjects == null || foldersForObjects.Rows.Count == 0)
      throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_HierarchyTable_Null"));
    foldersForObjects.DefaultView.Sort = $"{"F_PATH"} ASC";
    DataTable table = foldersForObjects.DefaultView.ToTable();
    long int64_1 = Convert.ToInt64((table.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x["F_OBJECT_TYPE"]) == Consts.ImbaseCatalogTypeID)) ?? throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_SourceCatalog_Null")))["F_OBJECT_ID"]);
    NodeIDPath path = int64_1 != 0L ? new NodeIDPath((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(int64_1)) : throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_SourceCatalog_Null"));
    long projectID = 0;
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      long int64_2 = Convert.ToInt64(row["F_OBJECT_ID"]);
      QuickObjectInfo objectInfo = session.GetObjectInfo(int64_2);
      long num = 0;
      if (projectID != 0L)
      {
        IDBRelation relation = session.GetRelation(projectID, objectInfo.ID);
        num = relation != null ? relation.RelationID : 0L;
      }
      projectID = int64_2;
      CreateObjectNodeParams e = new CreateObjectNodeParams()
      {
        ObjectTypeID = Convert.ToInt32(row["F_OBJECT_TYPE"]),
        ObjectID = int64_2,
        Caption = Convert.ToString(row["CAPTION"]),
        PrjLinkID = num
      };
      path = new NodeIDPath(path, (INodeID) new NodeID(e));
    }
    return path;
  }
}

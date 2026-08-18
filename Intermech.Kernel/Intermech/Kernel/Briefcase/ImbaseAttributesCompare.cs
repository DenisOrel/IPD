// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImbaseAttributesCompare
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services.PortalServices;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal class ImbaseAttributesCompare
{
  public static ImportActions Compare(
    IUserSession session,
    ImportingObject importingObject,
    IDBObject dbObject)
  {
    ImportingImbaseEntity importingImbaseEntity1 = new ImportingImbaseEntity(importingObject);
    ImportingImbaseEntity importingImbaseEntity2 = new ImportingImbaseEntity(dbObject);
    if (!importingImbaseEntity1.IsImbaseEntity && !importingImbaseEntity2.IsImbaseEntity || !string.IsNullOrEmpty(importingImbaseEntity1.Key) && !importingImbaseEntity2.IsImbaseEntity)
      return ImportActions.None;
    if (!string.IsNullOrEmpty(importingImbaseEntity1.Key) && importingImbaseEntity2.IsImbaseEntity || !importingImbaseEntity1.IsImbaseEntity && importingImbaseEntity2.IsImbaseEntity)
      return ImportActions.Ignore;
    if (importingImbaseEntity1.Code != -1L && importingImbaseEntity1.Link != 0L && importingImbaseEntity2.IsImbaseEntity)
    {
      if (Math.Abs(importingImbaseEntity1.Link) == Math.Abs(importingImbaseEntity2.Link) && importingImbaseEntity1.Code == importingImbaseEntity2.Code)
        return ImportActions.Ignore;
      if (SiteTraceLog.Enabled)
        SiteTraceLog.Write($"importEntity.Link = {importingImbaseEntity1.Link}. dbEntity.Link = {importingImbaseEntity2.Link} importEntity.Code = {importingImbaseEntity1.Code} dbEntity.Code = {importingImbaseEntity2.Code}");
      throw new NotEqualImbaseLinksImportExceptions(importingImbaseEntity1.Caption, importingImbaseEntity2.Caption);
    }
    if (importingImbaseEntity1.Code == -1L || importingImbaseEntity1.Link == 0L || importingImbaseEntity2.IsImbaseEntity)
      return ImportActions.None;
    return !(ImbaseAttributesCompare.GetCatalogEnterPoint(session, importingImbaseEntity1.Link) != string.Empty) ? ImportActions.Ignore : ImportActions.RefreshObject;
  }

  private static string GetCatalogEnterPoint(IUserSession session, long objectID)
  {
    IDBAttribute attributeByGuid1 = session.GetObject(objectID).GetAttributeByGuid(new Guid("cad0014d-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid1 != null && !string.IsNullOrEmpty(attributeByGuid1.AsString) && attributeByGuid1.AsString.Length > 2)
    {
      DataTable dataTable = session.ObjectsSelect(new Guid("cad00221-306c-11d8-b4e9-00304f19f545"), new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(new Guid("cad0014d-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) attributeByGuid1.AsString.Substring(0, 2), LogicalOperators.AND, 0)
      }, new object[1]
      {
        (object) MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeEnterPoint)
      }));
      if (dataTable.Rows.Count > 0)
        return Convert.ToString(dataTable.Rows[0][0]);
    }
    else
    {
      long rootCatalog = ImbaseAttributesCompare.GetRootCatalog(session.GetRelationCollection(session.IdentHelper.SortedRelationTypeID), MetaDataHelper.GetObjectTypeID("cad00221-306c-11d8-b4e9-00304f19f545"), objectID);
      if (rootCatalog != 0L)
      {
        IDBAttribute attributeByGuid2 = session.GetObject(rootCatalog).GetAttributeByGuid(PortalConsts.attributeEnterPoint, false);
        if (attributeByGuid2 != null && !string.IsNullOrEmpty(attributeByGuid2.AsString))
          return attributeByGuid2.AsString;
      }
    }
    return string.Empty;
  }

  private static long GetRootCatalog(
    IDBRelationCollection collection,
    int catalogTypeID,
    long childID)
  {
    DataTable dataTable = collection.EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) -2,
      (object) -7
    }), childID);
    if (dataTable.Rows.Count == 0)
      return 0;
    long int64 = Convert.ToInt64(dataTable.Rows[0][0]);
    return Convert.ToInt32(dataTable.Rows[0][1]) == catalogTypeID ? int64 : ImbaseAttributesCompare.GetRootCatalog(collection, catalogTypeID, int64);
  }
}

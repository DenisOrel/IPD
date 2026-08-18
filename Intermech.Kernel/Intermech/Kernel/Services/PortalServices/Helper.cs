// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Helper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Text;


namespace Intermech.Kernel.Services.PortalServices;

public class Helper
{
  internal static string MessageFCAttribute = "Публикуется в виде контейнера атрибутов";

  public static List<int> GetFreeChangeAttributes(
    IUserSession session,
    int objectTypeID,
    bool fileAttributesOnly = false)
  {
    DataTable dataTable = session.GetObjectType(objectTypeID).Attributes.Select(string.Empty);
    List<int> intList = new List<int>(dataTable.Rows.Count);
    int fileAttributeId = session.IdentHelper.FileAttributeID;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int int32_1 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
      FieldTypes int32_2 = (FieldTypes) Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]);
      if ((!fileAttributesOnly || int32_2 == FieldTypes.ftFile) && ((Convert.ToInt32(row["F_OPTIONS"]) & 64 /*0x40*/) == 64 /*0x40*/ && Convert.ToInt32(row["F_CONTENT"]) == 0 || int32_1 == fileAttributeId))
        intList.Add(int32_1);
    }
    return intList.Count <= 0 ? (List<int>) null : intList;
  }

  public static bool FreeChangeAttributesPresent(
    IUserSession session,
    IDBObject obj,
    out List<int> attributes,
    bool fileAttributesOnly)
  {
    attributes = Helper.GetFreeChangeAttributes(session, obj.ObjectType, fileAttributesOnly);
    if (attributes != null)
    {
      for (int attr_index = 0; attr_index < obj.Attributes.Count; attr_index++)
      {
        if (attributes.Exists((Predicate<int>) (x => x.Equals(obj.Attributes[attr_index].AttributeID))))
          return true;
      }
    }
    return false;
  }

  public static string[] GetObjectTypeGuidsList(int[] objtypesIDs)
  {
    if (objtypesIDs == null)
      return (string[]) null;
    List<string> stringList = new List<string>(objtypesIDs.Length);
    foreach (int objtypesId in objtypesIDs)
    {
      Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(objtypesId);
      if (objectTypeGuid != Guid.Empty)
        stringList.Add(objectTypeGuid.ToString());
    }
    return stringList.Count <= 0 ? (string[]) null : stringList.ToArray();
  }

  public static string FormingLogError(Exception ex)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine(ex.Message);
    stringBuilder.AppendLine(ex.StackTrace);
    for (Exception innerException = ex.InnerException; innerException != null; innerException = innerException.InnerException)
    {
      stringBuilder.AppendLine(string.Empty.PadLeft(70, '-'));
      if (innerException is AccessDeniedException)
      {
        foreach (string log in (innerException as AccessDeniedException).LogList)
          stringBuilder.AppendLine(log);
      }
      else
      {
        stringBuilder.AppendLine(innerException.Message);
        stringBuilder.AppendLine(innerException.StackTrace);
      }
    }
    return stringBuilder.ToString();
  }

  public static DataTable GetChildVersions(
    IDBObjectCollection objectsCollection,
    ColumnDescriptor[] columns,
    long parentVersionID)
  {
    DBRecordSetParams paramsSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(0, RelationalOperators.ParentVersionID, (object) parentVersionID, LogicalOperators.AND, 0, false)
    }, columns);
    FiltrationHelper.BlockPluginFiltrations(ref paramsSet, (HybridDictionary) null);
    DataTable toTable = objectsCollection.Select(paramsSet);
    if (toTable.Rows.Count > 0)
    {
      for (int index = 0; index < toTable.Rows.Count; ++index)
      {
        DataTable childVersions = Helper.GetChildVersions(objectsCollection, columns, Convert.ToInt64(toTable.Rows[index][0]));
        if (childVersions.Rows.Count > 0)
          DataSetProcessor.AddTable(toTable, childVersions, false);
      }
    }
    return toTable;
  }

  public static string GetString(int length, BinaryReader br)
  {
    if (length <= 0)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(br.ReadChars(length));
    return stringBuilder.ToString();
  }

  public static int CalculateTaskNo(IDBObjectCollection objColl, ITask task)
  {
    DataTable dataTable = objColl.Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(PortalConsts.attributeTaskType, RelationalOperators.Equal, (object) (int) task.Type, LogicalOperators.AND, 0)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) PortalConsts.attributeTaskNo, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0)
    }));
    return dataTable.Rows.Count <= 0 ? 0 : Convert.ToInt32(dataTable.Rows[dataTable.Rows.Count - 1][0]) + 1;
  }
}

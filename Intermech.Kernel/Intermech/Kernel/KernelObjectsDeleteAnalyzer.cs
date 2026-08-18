// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.KernelObjectsDeleteAnalyzer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Projects;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

public class KernelObjectsDeleteAnalyzer : ObjectsDeleteAnalyzer
{
  private static int projectTypeID = -1;

  protected virtual void FillTypes()
  {
    if (KernelObjectsDeleteAnalyzer.projectTypeID != -1)
      return;
    KernelObjectsDeleteAnalyzer.projectTypeID = MetaDataHelper.GetObjectTypeID("cad00812-306c-11d8-b4e9-00304f19f545");
  }

  public static DataTable LoadComposition(
    IUserSession session,
    long projID,
    int relationTypeID,
    VersionsRule rule)
  {
    DataTable dataTable = (DataTable) null;
    if (session == null || projID == 0L || relationTypeID == -1)
      return dataTable;
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    };
    object[] objArray = new object[0];
    SortOrders[] sortOrdersArray = new SortOrders[0];
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-21, RelationalOperators.Equal, (object) projID, LogicalOperators.NONE, 0, true)
    }, columns);
    IDBRelationCollection relationCollection = session.GetRelationCollection(relationTypeID);
    try
    {
      if (relationCollection != null)
      {
        relationCollection.FiltrationRule = rule;
        dataTable = relationCollection.Select(paramSet);
      }
    }
    catch
    {
    }
    return dataTable;
  }

  protected virtual void AnalyzeProject(
    IUserSession session,
    DeletingObject project,
    DeleteAnalyzerOptions options)
  {
    if (session == null)
      return;
    if (project == null)
      return;
    try
    {
      IDBObject dbObject = session.GetObject(project.ObjectID, false);
      if (dbObject == null || !(dbObject is IDBProjectObject dbProjectObject))
        return;
      long[] linkedObjects = dbProjectObject.LinkedObjects;
      if (linkedObjects == null)
        return;
      for (int index = 0; index < linkedObjects.Length; ++index)
      {
        if (linkedObjects[index] != project.ObjectID)
          project.Items.FindRootParent().Add(0L, 0L, linkedObjects[index], false, LocalizationHolder.rm.GetString("Kernel_487")).LoadDescription(session);
      }
    }
    catch
    {
    }
  }

  protected virtual void AnalyzeGroupingObject(
    IUserSession session,
    DeletingObject groupingObj,
    DeleteAnalyzerOptions options)
  {
    if (session == null)
      return;
    if (groupingObj == null)
      return;
    try
    {
      IDBObject dbObject = session.GetObject(groupingObj.ObjectID, false);
      if (dbObject == null)
        return;
      VersionsRule rule = new VersionsRule();
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad00696-306c-11d8-b4e9-00304f19f545"), false);
      if (attributeByGuid != null)
        rule.LoadFromAttribute(session, attributeByGuid);
      else
        rule = (VersionsRule) null;
      DataTable dataTable = KernelObjectsDeleteAnalyzer.LoadComposition(session, groupingObj.ObjectID, -1, rule);
      if (dataTable == null || dataTable.Rows.Count == 0)
        return;
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        long int64 = Convert.ToInt64(dataTable.Rows[index][0]);
        if (groupingObj.FindDeletingObject(int64) == null)
          groupingObj.Items.FindRootParent().Add(0L, 0L, int64, false, LocalizationHolder.rm.GetString("Kernel_488")).LoadDescription(session);
      }
    }
    catch
    {
    }
  }

  public override int Analyze(
    IUserSession session,
    DeletingObjects deletingObjects,
    DeleteAnalyzerOptions options)
  {
    if (deletingObjects == null || deletingObjects.Count == 0 || session == null)
      return 0;
    this.FillTypes();
    List<DeletingObject> deletingObjects1 = deletingObjects.ExtractDeletingObjects();
    int num1 = 0;
    if (deletingObjects1 == null)
      return 0;
    for (int index = 0; index < deletingObjects1.Count; ++index)
    {
      DeletingObject deletingObject = deletingObjects1[index];
      deletingObject.LoadDescription(session);
      bool flag1 = MetaDataHelper.IsObjectTypeChildOf(deletingObject.ObjectType, KernelObjectsDeleteAnalyzer.projectTypeID);
      bool flag2 = MetaDataHelper.HasObjectTypeGroupingRelTypes(deletingObject.ObjectType);
      if ((options & DeleteAnalyzerOptions.FindLinkedObjects) > DeleteAnalyzerOptions.None && flag1 | flag2)
      {
        if (flag1)
          this.AnalyzeProject(session, deletingObject, options);
        if (flag2)
          this.AnalyzeGroupingObject(session, deletingObject, options);
      }
    }
    int num2 = this.AnalyzeAllVersions(session, deletingObjects, options);
    if (num2 > 0)
      this.Analyze(session, deletingObjects, options);
    int num3 = num1 + num2;
    int num4 = this.AnalyzeLinkedObjects(session, deletingObjects, options);
    if (num4 > 0)
      this.Analyze(session, deletingObjects, options);
    int num5 = num3 + num4;
    deletingObjects.Clear();
    deletingObjects.AddRange((IEnumerable<DeletingObject>) deletingObjects1);
    return num5;
  }

  private int AnalyzeLinkedObjects(
    IUserSession session,
    DeletingObjects deletingObjects,
    DeleteAnalyzerOptions options)
  {
    if (session == null || deletingObjects == null || (options & DeleteAnalyzerOptions.FindLinkedObjects) == DeleteAnalyzerOptions.None)
      return 0;
    int num = 0;
    List<DeletingObject> deletingObjects1 = deletingObjects.ExtractDeletingObjects();
    for (int index1 = 0; index1 < deletingObjects1.Count; ++index1)
    {
      DeletingObject deletingObject = deletingObjects1[index1];
      List<long> linksToObject = this.GetLinksToObject(session, deletingObject.ObjectID);
      if (linksToObject != null)
      {
        for (int index2 = 0; index2 < linksToObject.Count; ++index2)
        {
          if (deletingObjects.FindDeletingObjectFromRoot(linksToObject[index2]) == null)
          {
            deletingObject.Items.Add(-1L, 0L, linksToObject[index2], false, LocalizationHolder.rm.GetString("Kernel_1032")).LoadDescription(session);
            ++num;
          }
        }
      }
    }
    return num;
  }

  private List<long> GetLinksToObject(IUserSession serverSession, long objID)
  {
    List<long> linksToObject = new List<long>();
    if (!(serverSession is UserSession userSession) || objID == 0L)
      return linksToObject;
    IDbDataParameter dbDataParameter = userSession.DataManager.Parameter(":parF_TOOBJECT_ID", (object) Math.Abs(objID));
    string commandText = string.Format("SELECT A.{2}, B.{5}, A.{3} FROM {0} A, {1} B WHERE A.{4} = :parF_TOOBJECT_ID AND A.{2} = B.{2} ORDER BY A.{2}, A.{3}", (object) "IMS_OBJECT_LINKS", (object) "IMS_OBJECTS", (object) "F_OBJECT_ID", (object) "F_ATTRIBUTE_ID", (object) "F_TOOBJECT_ID", (object) "F_OBJECT_TYPE");
    DataTable dataTable = userSession.DataManager.ExecuteDataTable(commandText, dbDataParameter);
    SortedDictionary<long, bool> sortedDictionary1 = new SortedDictionary<long, bool>();
    SortedDictionary<Tuple<int, int>, bool> sortedDictionary2 = new SortedDictionary<Tuple<int, int>, bool>();
    if (dataTable != null)
    {
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        DataRow row = dataTable.Rows[index];
        long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
        int int32Value1 = DataSetProcessor.GetInt32Value(row, 1, -1);
        int int32Value2 = DataSetProcessor.GetInt32Value(row, 2, 0);
        if (int32Value1 != -1 && int64Value != 0L && int32Value2 != 0)
        {
          Tuple<int, int> key = new Tuple<int, int>(int32Value1, int32Value2);
          if (!sortedDictionary2.ContainsKey(key))
          {
            IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(int32Value1, int32Value2);
            sortedDictionary2[key] = attribute4ObjectType != null && !attribute4ObjectType.DisableDeleteLinkedObjects;
          }
          if (!sortedDictionary2[key])
            sortedDictionary1[int64Value] = true;
          else if (!sortedDictionary1.ContainsKey(int64Value) || !sortedDictionary1[int64Value])
            sortedDictionary1[int64Value] = false;
        }
      }
    }
    foreach (KeyValuePair<long, bool> keyValuePair in sortedDictionary1)
    {
      if (keyValuePair.Value && linksToObject.IndexOf(keyValuePair.Key) < 0)
        linksToObject.Add(keyValuePair.Key);
    }
    return linksToObject;
  }
}

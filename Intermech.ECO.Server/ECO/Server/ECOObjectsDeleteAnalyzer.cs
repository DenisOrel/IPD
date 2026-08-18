// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.ECOObjectsDeleteAnalyzer
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.ECO.Server;

internal class ECOObjectsDeleteAnalyzer : ObjectsDeleteAnalyzer
{
  protected virtual void AnalyzeECO(IUserSession session, DeletingObject ecoItem)
  {
    if (session == null)
      return;
    if (ecoItem == null)
      return;
    try
    {
      Dictionary<long, long> dictionary = new Dictionary<long, long>();
      ICompositionLoadService customService = session.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService;
      List<ColumnDescriptor> columns = new List<ColumnDescriptor>(2);
      columns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
      columns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
      int relationTypeId = MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545");
      List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive((IEnumerable<int>) MetaDataHelper.GetApplicabilityChildObjectTypesID(ecoItem.ObjectType, relationTypeId));
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(MetaDataHelper.GetAttributeTypeID("cad00073-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) true, LogicalOperators.NONE, 0, true)
      };
      DataTable dataTable = customService.LoadComposition((object) session.SessionGUID, ecoItem.ObjectID, relationTypeId, (IEnumerable<ColumnDescriptor>) columns, "cad005ac-306c-11d8-b4e9-00304f19f5455", (IEnumerable<ConditionStructure>) conditions, childrenIdRecursive.ToArray());
      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          long int64Value1 = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 0, 0L);
          long int64Value2 = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 1, 0L);
          if (int64Value1 != 0L && int64Value2 != 0L && !dictionary.ContainsKey(int64Value1))
            dictionary.Add(int64Value1, int64Value2);
        }
        dataTable.Dispose();
      }
      if (dictionary.Count <= 0)
        return;
      foreach (KeyValuePair<long, long> keyValuePair in dictionary)
      {
        if (ecoItem.Items.FindDeletingObjectFromRoot(keyValuePair.Key) == null && ecoItem.Items.FindDeletingObjectFromRoot(-keyValuePair.Key) == null)
          ecoItem.Items.FindRootParent().Add(0L, 0L, keyValuePair.Key, true, -1, string.Empty, 0L, 0L, -1, keyValuePair.Value, 0L, false, LocalizationHolder.rm.GetString("ECO.Server_1")).LoadDescription(session);
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
    List<DeletingObject> deletingObjects1 = deletingObjects.ExtractDeletingObjects();
    int num = 0;
    if (deletingObjects1 == null)
      return 0;
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545");
    if ((options & DeleteAnalyzerOptions.FindLinkedObjects) > DeleteAnalyzerOptions.None)
    {
      for (int index = 0; index < deletingObjects1.Count; ++index)
      {
        DeletingObject ecoItem = deletingObjects1[index];
        ecoItem.LoadDescription(session);
        if (MetaDataHelper.IsObjectTypeChildOf(ecoItem.ObjectType, objectTypeId))
          this.AnalyzeECO(session, ecoItem);
      }
    }
    if (this.AnalyzeAllVersions(session, deletingObjects, options) > 0)
      this.Analyze(session, deletingObjects, options);
    return num;
  }
}

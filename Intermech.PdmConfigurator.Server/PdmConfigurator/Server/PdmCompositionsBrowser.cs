// Decompiled with JetBrains decompiler
// Type: Intermech.PdmConfigurator.Server.PdmCompositionsBrowser
// Assembly: Intermech.PdmConfigurator.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 80F94CD1-7E39-423C-8BC4-966315C23D3C
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.PdmConfigurator.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.PdmConfigurator.Server;

internal class PdmCompositionsBrowser : IPdmCompositionBrowser
{
  private Guid guid = Guid.NewGuid();
  private static Guid cyclesLog = new Guid("{72229E51-FD9D-4168-A785-980F2DC0270F}");

  public virtual Guid Guid
  {
    [DebuggerStepThrough] get => this.guid;
  }

  public virtual TraceLog Browse(
    IUserSession session,
    RelationPair rootObject,
    RelationPath rootObjectPath,
    CompositionObjects objects,
    PdmCompositionBrowserEventArgs args)
  {
    TraceLog traceLog1 = new TraceLog();
    traceLog1.Tags = new HybridDictionary();
    List<long> longList1 = new List<long>();
    traceLog1.Tags[(object) PdmCompositionsBrowser.cyclesLog] = (object) longList1;
    IPdmConfiguratorService service = ServerServices.GetService(typeof (IPdmConfiguratorService)) as IPdmConfiguratorService;
    if (args == null || objects == null || objects.Count == 0 || rootObject == null || rootObject.TOP_OBJECT_ID == 0L || rootObject.TOP_OBJECT_TYPE == -1 || service == null)
      return traceLog1;
    RelationPair relationPair = (RelationPair) null;
    if (rootObjectPath != null && !rootObjectPath.Empty)
    {
      for (int index = 0; index < rootObjectPath.Items.Count; ++index)
      {
        SimpleRelationPair simpleRelationPair = rootObjectPath.Items[index];
        if (!simpleRelationPair.Empty)
        {
          RelationPair key = Helper.CreateKey(rootObject.Handle != 0L ? rootObject.Handle : session.ClientConnectionID, rootObject.TOP_OBJECT_ID, rootObject.TOP_OBJECT_TYPE, rootObject.USER_ID != 0L ? rootObject.USER_ID : session.UserID, simpleRelationPair.F_PRJLINK_ID, simpleRelationPair.F_RELATION_TYPE, simpleRelationPair.F_PART_ID, simpleRelationPair.F_OBJECT_TYPE);
          PdmConfiguratorContext configuratorContext;
          if (simpleRelationPair.F_PRJLINK_ID == 0L && simpleRelationPair.F_PART_ID != 0L && MetaDataHelper.IsPdmRootObjectType(simpleRelationPair.F_OBJECT_TYPE))
          {
            IDBObject source = session.GetObject(simpleRelationPair.F_PART_ID, false);
            if (source != null)
            {
              configuratorContext = new PdmConfiguratorContext((object) string.Empty);
              configuratorContext.Key = key;
              configuratorContext.ParentKey = relationPair;
              configuratorContext.Assign((object) source);
            }
            else
              continue;
          }
          else
          {
            IDBRelation relation = session.GetRelation(simpleRelationPair.F_PRJLINK_ID, false);
            if (relation != null)
              configuratorContext = new PdmConfiguratorContext((object) relation);
            else
              continue;
          }
          if (configuratorContext == null)
            configuratorContext = new PdmConfiguratorContext((object) string.Empty);
          configuratorContext.Key = key;
          configuratorContext.ParentKey = relationPair;
          service[(object) (session as UserSession), key] = configuratorContext;
          relationPair = key;
        }
      }
    }
    rootObject = relationPair ?? rootObject;
    for (int index = 0; index < objects.Count; ++index)
    {
      CompositionObject rootObject1 = objects[index];
      RelationPath source = new RelationPath();
      CompositionObject compositionObject = objects[index];
      RelationPair key = Helper.CreateKey(rootObject.Handle != 0L ? rootObject.Handle : session.ClientConnectionID, rootObject.TOP_OBJECT_ID, rootObject.TOP_OBJECT_TYPE, rootObject.USER_ID != 0L ? rootObject.USER_ID : session.UserID, compositionObject.F_PRJLINK_ID, compositionObject.F_RELATION_TYPE, compositionObject.F_OBJECT_ID, compositionObject.F_OBJECT_TYPE);
      args.Tags = args.Tags ?? new HybridDictionary();
      args.Tags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] = (object) key;
      if (key != null && key.TOP_OBJECT_ID != 0L && key.TOP_OBJECT_TYPE != -1)
        source.Items.Add(new SimpleRelationPair(0L, -1, key.TOP_OBJECT_ID, key.TOP_OBJECT_TYPE));
      SimpleRelationPair simpleRelationPair = new SimpleRelationPair(rootObject1.F_PRJLINK_ID, rootObject1.F_PRJLINK_ID != 0L ? rootObject1.F_RELATION_TYPE : -1, rootObject1.F_OBJECT_ID, rootObject1.F_OBJECT_TYPE);
      RelationPath relationPath = new RelationPath((object) source);
      if (relationPath.Items.IndexOf(simpleRelationPair) < 0)
        relationPath.Items.Add(simpleRelationPair);
      if (args.FullTrace)
      {
        rootObject1.Tag = (object) new HybridDictionary();
        if (MetaDataHelper.IsObjectTypeChildOf(rootObject1.F_OBJECT_TYPE, MetaDataHelper.GetObjectTypeID("cad00268-306c-11d8-b4e9-00304f19f545")))
        {
          IDBAttribute attributeById = session.GetObject(rootObject1.F_OBJECT_ID).GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad0058a-306c-11d8-b4e9-00304f19f545"));
          (rootObject1.Tag as HybridDictionary)[(object) "cad0058a-306c-11d8-b4e9-00304f19f545"] = (object) (attributeById != null ? DataSetProcessor.GetInt64Value(attributeById.Value, 0L) : 0L);
        }
      }
      TraceLog traceLog2 = this.Browse(session, rootObject, rootObject1, args, relationPath);
      traceLog1.Merge(traceLog2);
      if (args.Status != null)
        args.Status.Trace = traceLog1;
      if (args.FullTrace && args.Tags != null && rootObject1.F_RELATION_TYPE == MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545"))
      {
        IDBAttribute attributeById = session.GetRelation(rootObject1.F_PRJLINK_ID, false)?.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545"));
        MeasuredValue measuredValue = attributeById != null ? DataSetProcessor.GetMeasuredValue(attributeById.Value, (MeasuredValue) null) : (MeasuredValue) null;
        if (measuredValue == null || measuredValue.Value == 0.0)
        {
          if (!traceLog1.Items.ContainsKey(relationPath))
          {
            traceLog1.Items[relationPath] = new TraceEntry(PdmConfiguratorResult.True, PdmCompositionTraceResult.WithoutQuantity, LocalizationHolder.rm.GetString("PdmConfigurator.Server_13"));
          }
          else
          {
            traceLog1.Items[relationPath].Trace |= PdmCompositionTraceResult.HasSomeRoutes;
            traceLog1.Items[relationPath].Message = !string.IsNullOrEmpty(traceLog1.Items[relationPath].Message) ? traceLog1.Items[relationPath].Message + LocalizationHolder.rm.GetString("PdmConfigurator.Server_14") : LocalizationHolder.rm.GetString("PdmConfigurator.Server_13");
          }
        }
      }
    }
    if (args.FullTrace && args.Tags != null && args.Tags[(object) TraceLog.ObjectsWithRoutesGuid] != null)
    {
      Dictionary<long, RelationPath> vers = args.Tags[(object) TraceLog.ObjectsWithRoutesGuid] as Dictionary<long, RelationPath>;
      if (vers != null && vers.Count > 0)
      {
        List<long> projIDs = new List<long>((IEnumerable<long>) vers.Keys);
        ICompositionLoadService customService = session.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService;
        List<ColumnDescriptor> columns = new List<ColumnDescriptor>();
        columns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.ASC, 0));
        columns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PART_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        DataTable dataTable;
        if (args.Rule != null)
          dataTable = customService.LoadComplexCompositions((object) session, (IEnumerable<long>) projIDs, MetaDataHelper.GetRelationTypeID("cad0019f-306c-11d8-b4e9-00304f19f545"), (IEnumerable<ColumnDescriptor>) columns, args.Rule, MetaDataHelper.GetObjectTypeID("cad0016f-306c-11d8-b4e9-00304f19f545"));
        else
          dataTable = customService.LoadComplexCompositions((object) session, (IEnumerable<long>) projIDs, MetaDataHelper.GetRelationTypeID("cad0019f-306c-11d8-b4e9-00304f19f545"), (IEnumerable<ColumnDescriptor>) columns, args.FiltrationOwnerID, MetaDataHelper.GetObjectTypeID("cad0016f-306c-11d8-b4e9-00304f19f545"));
        Dictionary<long, int> dictionary = new Dictionary<long, int>();
        List<long> longList2 = new List<long>((IEnumerable<long>) vers.Keys);
        if (dataTable != null && dataTable.Rows.Count > 0)
        {
          for (int index = 0; index < dataTable.Rows.Count; ++index)
          {
            DataRow row = dataTable.Rows[index];
            long int64Value1 = DataSetProcessor.GetInt64Value(row, "F_PROJ_ID", 0L);
            long int64Value2 = DataSetProcessor.GetInt64Value(row, "F_PART_ID", 0L);
            if (int64Value1 != 0L && int64Value2 != 0L)
            {
              if (!dictionary.ContainsKey(int64Value1))
                dictionary[int64Value1] = 1;
              else
                ++dictionary[int64Value1];
            }
          }
        }
        foreach (KeyValuePair<long, int> keyValuePair in dictionary)
        {
          if (keyValuePair.Value > 1 && longList2.IndexOf(keyValuePair.Key) >= 0)
            longList2.Remove(keyValuePair.Key);
        }
        longList2.ForEach((Action<long>) (item =>
        {
          if (!vers.ContainsKey(item))
            return;
          vers.Remove(item);
        }));
      }
      traceLog1.Tags[(object) TraceLog.ObjectsWithRoutesGuid] = (object) vers;
      foreach (KeyValuePair<long, RelationPath> keyValuePair in vers)
      {
        if (!traceLog1.Items.ContainsKey(keyValuePair.Value))
        {
          traceLog1.Items[keyValuePair.Value] = new TraceEntry(PdmConfiguratorResult.True, PdmCompositionTraceResult.HasSomeRoutes, LocalizationHolder.rm.GetString("PdmConfigurator.Server_1"));
        }
        else
        {
          traceLog1.Items[keyValuePair.Value].Trace |= PdmCompositionTraceResult.HasSomeRoutes;
          traceLog1.Items[keyValuePair.Value].Message = !string.IsNullOrEmpty(traceLog1.Items[keyValuePair.Value].Message) ? traceLog1.Items[keyValuePair.Value].Message + LocalizationHolder.rm.GetString("PdmConfigurator.Server_2") : LocalizationHolder.rm.GetString("PdmConfigurator.Server_1");
        }
      }
    }
    traceLog1.Tags.Remove((object) PdmCompositionsBrowser.cyclesLog);
    return traceLog1;
  }

  protected virtual TraceLog Browse(
    IUserSession session,
    RelationPair rootKey,
    CompositionObject rootObject,
    PdmCompositionBrowserEventArgs args,
    RelationPath path)
  {
    TraceLog result1 = new TraceLog();
    if (args == null || rootKey == null)
      return result1;
    args.Tags = args.Tags ?? new HybridDictionary();
    args.Tags[(object) TraceLog.TraceLogGuid] = (object) result1;
    args.Tags[(object) RelationPath.RelationPathGuid] = (object) path;
    args.Tags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] = (object) rootKey;
    if (!(args.Tags[(object) PdmCompositionsBrowser.cyclesLog] is List<long> longList1))
      longList1 = new List<long>();
    List<long> longList2 = longList1;
    args.Tags[(object) PdmCompositionsBrowser.cyclesLog] = (object) longList2;
    List<int> intList1 = args.RelTypeID != -1 ? new List<int>(1) : (List<int>) null;
    intList1?.Add(args.RelTypeID);
    int lcLevelId = MetaDataHelper.GetLCLevelID("cad00011-306c-11d8-b4e9-00304f19f545");
    long int64Value1 = !(rootObject.Tag is HybridDictionary tag1) || !tag1.Contains((object) "cad0058a-306c-11d8-b4e9-00304f19f545") ? 0L : DataSetProcessor.GetInt64Value(tag1[(object) "cad0058a-306c-11d8-b4e9-00304f19f545"], 0L);
    Dictionary<long, RelationPath> tag2 = args.Tags[(object) TraceLog.ObjectsWithRoutesGuid] as Dictionary<long, RelationPath>;
    List<int> tag3 = args.Tags[(object) TraceLog.RouteApplsGuid] as List<int>;
    List<int> tag4 = args.Tags[(object) TraceLog.RouteDisabledApplsGuid] as List<int>;
    if (args.FullTrace && tag2 != null && MetaDataHelper.IsEnabledParentType(rootObject.F_OBJECT_TYPE, (IEnumerable<int>) tag3, (IEnumerable<int>) tag4, false) && !tag2.ContainsKey(rootObject.F_OBJECT_ID))
      tag2[rootObject.F_OBJECT_ID] = path;
    if (!(ServerServices.GetService(typeof (ICompositionsAutomaticSortingService)) is ICompositionsAutomaticSortingService service))
      return result1;
    CompositionsAutosortRule autosortRule = service.GetAutosortRule((object) session, false);
    if (autosortRule == null)
      return result1;
    bool flag1 = args.Tags != null && args.Tags.Contains((object) "{78C6A7F1-3B57-4CF9-8E3C-B5D308593A6B}");
    bool flag2 = args.Tags != null && args.Tags.Contains((object) "{7C0E9952-C5C7-4505-AA53-2F662A4E9D2B}");
    autosortRule.UseEvents = true;
    List<int> intList2 = intList1 == null ? autosortRule.GetObjectTypeVisibleRelations(rootObject.F_OBJECT_TYPE, true) : intList1;
    for (int index1 = 0; index1 < intList2.Count; ++index1)
    {
      bool advAttrs = intList2[index1] == MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545");
      IDBRelationCollection relationCollection = session.GetRelationCollection(intList2[index1], args.FiltrationOwnerID);
      if (relationCollection != null)
      {
        if (args.Rule != null)
          relationCollection.FiltrationRule = args.Rule;
        List<int> childObjectTypesId = MetaDataHelper.GetApplicabilityChildObjectTypesID(rootObject.F_OBJECT_TYPE, intList2[index1]);
        if (childObjectTypesId.Count == 1)
          relationCollection.ObjectTypeID = childObjectTypesId[0];
        DBRecordSetParams paramsSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-21, RelationalOperators.Equal, (object) rootObject.F_OBJECT_ID, LogicalOperators.NONE, 0, true)
        }, rootObject.GetColumnDescriptors(advAttrs).ToArray());
        paramsSet.Tags = args.Tags;
        DataTable dataTable;
        try
        {
          Helper.BlockPluginFiltrations(ref paramsSet);
          if (paramsSet.Tags.Contains((object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"))
            paramsSet.Tags.Remove((object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}");
          paramsSet.Tags[(object) "{89F3DEDD-EE3A-4A42-ADD0-55BF26E622E1}"] = (object) true;
          paramsSet.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) false;
          dataTable = relationCollection.Select(paramsSet);
        }
        catch
        {
          dataTable = (DataTable) null;
        }
        if (dataTable != null && dataTable.Rows.Count != 0)
        {
          if (!args.BeforeFirstError || !this.AnyError(result1))
          {
            for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
            {
              bool flag3 = true;
              DataSetProcessor.GetInt64Value(dataTable.Rows[index2], "F_PROJ_ID", 0L);
              long int64Value2 = DataSetProcessor.GetInt64Value(dataTable.Rows[index2], "F_PRJLINK_ID", 0L);
              int int32Value1 = DataSetProcessor.GetInt32Value(dataTable.Rows[index2], "F_RELATION_TYPE", -1);
              long int64Value3 = DataSetProcessor.GetInt64Value(dataTable.Rows[index2], "F_OBJECT_ID", 0L);
              int int32Value2 = DataSetProcessor.GetInt32Value(dataTable.Rows[index2], "F_OBJECT_TYPE", -1);
              long int64Value4 = DataSetProcessor.GetInt64Value(dataTable.Rows[index2], "cad001c0-306c-11d8-b4e9-00304f19f545", 0L);
              DataSetProcessor.GetInt64Value(dataTable.Rows[index2], "cad001c1-306c-11d8-b4e9-00304f19f545", 0L);
              int int32Value3 = DataSetProcessor.GetInt32Value(dataTable.Rows[index2], "F_LEVEL_ID", 0);
              long int64Value5 = DataSetProcessor.GetInt64Value(dataTable.Rows[index2], "cad0038f-306c-11d8-b4e9-00304f19f545", 1L);
              long int64Value6 = DataSetProcessor.GetInt64Value(dataTable.Rows[index2], "cad0058a-306c-11d8-b4e9-00304f19f545", 0L);
              MeasuredValue measuredValue = DataSetProcessor.GetMeasuredValue(dataTable.Rows[index2], "cad00267-306c-11d8-b4e9-00304f19f545", (MeasuredValue) null);
              if (int64Value2 != 0L && int32Value1 != -1 && int64Value3 != 0L && int32Value2 != -1)
              {
                CompositionObject rootObject1 = new CompositionObject(dataTable.Rows[index2]);
                rootObject1.Tag = (object) new HybridDictionary();
                if (MetaDataHelper.IsObjectTypeChildOf(rootObject1.F_OBJECT_TYPE, MetaDataHelper.GetObjectTypeID("cad00268-306c-11d8-b4e9-00304f19f545")))
                {
                  IDBAttribute attributeById = session.GetObject(rootObject1.F_OBJECT_ID).GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad0058a-306c-11d8-b4e9-00304f19f545"));
                  (rootObject1.Tag as HybridDictionary)[(object) "cad0058a-306c-11d8-b4e9-00304f19f545"] = (object) (attributeById != null ? DataSetProcessor.GetInt64Value(attributeById.Value, 0L) : 0L);
                }
                SimpleRelationPair simpleRelationPair = new SimpleRelationPair(rootObject1.F_PRJLINK_ID, rootObject1.F_RELATION_TYPE, rootObject1.F_OBJECT_ID, rootObject1.F_OBJECT_TYPE);
                RelationPath relationPath = new RelationPath((object) path);
                relationPath.Items.Add(simpleRelationPair);
                if (result1.Items != null && result1.Items.ContainsKey(relationPath))
                {
                  TraceEntry traceEntry = result1.Items[relationPath];
                  result1.Items.Remove(relationPath);
                  result1.Items.Add(relationPath, traceEntry);
                }
                RelationPair rootKey1 = rootKey != null ? new RelationPair(rootKey.Handle, rootKey.TOP_OBJECT_ID, rootKey.TOP_OBJECT_TYPE, int64Value2, session.UserID, int64Value3, int32Value1, int32Value2) : (RelationPair) null;
                PdmCompositionBrowserEventArgs args1 = args.Clone() as PdmCompositionBrowserEventArgs;
                if (args.FullTrace)
                {
                  if (int64Value1 == 0L && int64Value6 == 1L)
                  {
                    if (!result1.Items.ContainsKey(relationPath))
                    {
                      result1.Items[relationPath] = new TraceEntry(PdmConfiguratorResult.True, PdmCompositionTraceResult.InstanceInPartyError, LocalizationHolder.rm.GetString("PdmConfigurator.Server_15"));
                    }
                    else
                    {
                      result1.Items[relationPath].Trace |= PdmCompositionTraceResult.InstanceInPartyError;
                      result1.Items[relationPath].Message = !string.IsNullOrEmpty(result1.Items[relationPath].Message) ? result1.Items[relationPath].Message + LocalizationHolder.rm.GetString("PdmConfigurator.Server_16") : LocalizationHolder.rm.GetString("PdmConfigurator.Server_15");
                    }
                  }
                  if (advAttrs && (measuredValue == null || measuredValue.Value == 0.0))
                  {
                    if (!result1.Items.ContainsKey(relationPath))
                    {
                      result1.Items[relationPath] = new TraceEntry(PdmConfiguratorResult.True, PdmCompositionTraceResult.WithoutQuantity, LocalizationHolder.rm.GetString("PdmConfigurator.Server_13"));
                    }
                    else
                    {
                      result1.Items[relationPath].Trace |= PdmCompositionTraceResult.WithoutQuantity;
                      result1.Items[relationPath].Message = !string.IsNullOrEmpty(result1.Items[relationPath].Message) ? result1.Items[relationPath].Message + LocalizationHolder.rm.GetString("PdmConfigurator.Server_14") : LocalizationHolder.rm.GetString("PdmConfigurator.Server_13");
                    }
                  }
                  if (int64Value4 != 0L && !flag2)
                  {
                    if (!result1.Items.ContainsKey(relationPath))
                    {
                      result1.Items[relationPath] = new TraceEntry(PdmConfiguratorResult.True, PdmCompositionTraceResult.HasSubstitutes, LocalizationHolder.rm.GetString("PdmConfigurator.Server_5"));
                    }
                    else
                    {
                      result1.Items[relationPath].Trace |= PdmCompositionTraceResult.HasSubstitutes;
                      result1.Items[relationPath].Message = !string.IsNullOrEmpty(result1.Items[relationPath].Message) ? result1.Items[relationPath].Message + LocalizationHolder.rm.GetString("PdmConfigurator.Server_6") : LocalizationHolder.rm.GetString("PdmConfigurator.Server_5");
                    }
                  }
                  if (tag2 != null && MetaDataHelper.IsEnabledParentType(int32Value2, (IEnumerable<int>) tag3, (IEnumerable<int>) tag4, false) && !tag2.ContainsKey(int64Value3))
                    tag2[int64Value3] = relationPath;
                  if (int32Value3 != lcLevelId && MetaDataHelper.IsObjectTypeChildOf(rootObject1.F_OBJECT_TYPE, MetaDataHelper.GetObjectTypeID("cad00268-306c-11d8-b4e9-00304f19f545")))
                  {
                    if (!result1.Items.ContainsKey(relationPath))
                    {
                      result1.Items[relationPath] = new TraceEntry(PdmConfiguratorResult.True, PdmCompositionTraceResult.NotManufacturingLevel, LocalizationHolder.rm.GetString("PdmConfigurator.Server_3"));
                    }
                    else
                    {
                      result1.Items[relationPath].Trace |= PdmCompositionTraceResult.NotManufacturingLevel;
                      result1.Items[relationPath].Message = !string.IsNullOrEmpty(result1.Items[relationPath].Message) ? result1.Items[relationPath].Message + LocalizationHolder.rm.GetString("PdmConfigurator.Server_4") : LocalizationHolder.rm.GetString("PdmConfigurator.Server_3");
                    }
                  }
                  if (advAttrs & flag1 && int64Value5 == 2L)
                    flag3 = false;
                }
                TraceLog result2 = (TraceLog) null;
                bool flag4 = true;
                for (int index3 = relationPath.Items.Count - 2; index3 >= 0; --index3)
                {
                  flag4 = Math.Abs(rootObject1.F_OBJECT_ID) != Math.Abs(relationPath.Items[index3].F_PART_ID);
                  if (!flag4)
                    break;
                }
                if (flag4 & flag3)
                {
                  result2 = this.Browse(session, rootKey1, rootObject1, args1, relationPath);
                  result1.Merge(result2);
                }
                if (args.Status != null)
                  args.Status.Trace = result1;
                if (args.BeforeFirstError && this.AnyError(result2))
                {
                  dataTable.Dispose();
                  return result1;
                }
              }
            }
            dataTable.Dispose();
          }
          else
            break;
        }
      }
    }
    return result1;
  }

  private bool AnyError(TraceLog result)
  {
    if (result == null)
      return false;
    SortedDictionary<RelationPath, TraceEntry> items = result.Items;
    foreach (RelationPath key in items.Keys)
    {
      TraceEntry traceEntry = items[key];
      if (traceEntry.Flags != PdmConfiguratorResult.False && traceEntry.Flags != PdmConfiguratorResult.True)
        return true;
    }
    return false;
  }
}

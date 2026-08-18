// Decompiled with JetBrains decompiler
// Type: Intermech.PdmConfigurator.Server.PdmComplexOptionsAnalyzer
// Assembly: Intermech.PdmConfigurator.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 80F94CD1-7E39-423C-8BC4-966315C23D3C
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.PdmConfigurator.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.PdmConfigurator.Server;

internal class PdmComplexOptionsAnalyzer : PdmOptionsAnalyzer
{
  public override int Analyze(
    IUserSession session,
    PdmAnalyzedOptionObjects optionObjects,
    PdmAnalyzerFlags options,
    IList<long> excludedObjects,
    IList<long> excludedOptions)
  {
    int num = base.Analyze(session, optionObjects, options, excludedObjects, excludedOptions);
    if ((options & PdmAnalyzerFlags.InCompositions) != PdmAnalyzerFlags.InCompositions && (options & PdmAnalyzerFlags.InCompositionsRecursive) != PdmAnalyzerFlags.InCompositionsRecursive)
      return num;
    SortedDictionary<long, bool> sortedDictionary1 = new SortedDictionary<long, bool>();
    SortedDictionary<int, List<long>> sortedDictionary2 = new SortedDictionary<int, List<long>>();
    SortedDictionary<long, PdmAnalyzedOptionObject> sortedDictionary3 = new SortedDictionary<long, PdmAnalyzedOptionObject>();
    for (int index = 0; index < optionObjects.Count; ++index)
    {
      PdmAnalyzedOptionObject optionObject = optionObjects[index];
      sortedDictionary3[optionObject.ObjectID] = optionObject;
      if (optionObject.ParsedComposition)
      {
        if (optionObject.ParsedObject)
          sortedDictionary1[optionObject.ObjectID] = true;
      }
      else
      {
        if (!sortedDictionary2.ContainsKey(optionObject.ObjectType))
          sortedDictionary2.Add(optionObject.ObjectType, new List<long>());
        List<long> longList = sortedDictionary2[optionObject.ObjectType];
        if (longList.IndexOf(optionObject.ObjectID) < 0)
          longList.Add(optionObject.ObjectID);
      }
    }
    foreach (KeyValuePair<int, List<long>> keyValuePair in sortedDictionary2)
      keyValuePair.Value.Sort();
    if (!(ServerServices.GetService(typeof (ICompositionsAutomaticSortingService)) is ICompositionsAutomaticSortingService service))
      return base.Analyze(session, optionObjects, options, excludedObjects, excludedOptions);
    CompositionsAutosortRule autosortRule = service.GetAutosortRule((object) session, false);
    if (autosortRule == null)
      return base.Analyze(session, optionObjects, options, excludedObjects, excludedOptions);
    foreach (KeyValuePair<int, List<long>> keyValuePair in sortedDictionary2)
    {
      if (keyValuePair.Value.Count != 0)
      {
        List<int> visibleRelations = autosortRule.GetObjectTypeVisibleRelations(keyValuePair.Key, true);
        for (int index1 = 0; index1 < visibleRelations.Count; ++index1)
        {
          if (MetaDataHelper.IsPdmConfigurableRelationType(visibleRelations[index1]))
          {
            IDBRelationCollection relationCollection = session.GetRelationCollection(visibleRelations[index1], "cad005aa-306c-11d8-b4e9-00304f19f545");
            if (relationCollection != null)
            {
              List<int> childObjectTypesId = MetaDataHelper.GetApplicabilityChildObjectTypesID(keyValuePair.Key, visibleRelations[index1]);
              if (childObjectTypesId.Count == 1)
                relationCollection.ObjectTypeID = childObjectTypesId[0];
              List<long> longList = new List<long>((IEnumerable<long>) keyValuePair.Value);
              while (longList.Count > 0)
              {
                long[] numArray;
                if (longList.Count > 25)
                {
                  numArray = new long[25];
                  longList.CopyTo(0, numArray, 0, 25);
                  longList.RemoveRange(0, 25);
                }
                else
                {
                  numArray = new long[longList.Count];
                  longList.CopyTo(numArray);
                  longList.Clear();
                }
                DBRecordSetParams paramsSet = new DBRecordSetParams(new ConditionStructure[1]
                {
                  numArray.Length > 1 ? new ConditionStructure(-21, RelationalOperators.In, (object) numArray, LogicalOperators.AND, 0, true) : new ConditionStructure(-21, RelationalOperators.Equal, (object) numArray[0], LogicalOperators.AND, 0, true)
                }, PdmAnalyzedOptionObject.GetColumnDescriptors().ToArray());
                DataTable dataTable;
                try
                {
                  Helper.BlockPluginFiltrations(ref paramsSet);
                  dataTable = relationCollection.Select(paramsSet);
                }
                catch
                {
                  dataTable = (DataTable) null;
                }
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                  for (int index2 = 0; index2 < numArray.Length; ++index2)
                    sortedDictionary3[numArray[index2]].ParsedComposition = true;
                }
                else
                {
                  for (int index3 = 0; index3 < dataTable.Rows.Count; ++index3)
                  {
                    long int64Value1 = DataSetProcessor.GetInt64Value(dataTable.Rows[index3], 10, 0L);
                    if (sortedDictionary3.ContainsKey(int64Value1))
                      sortedDictionary3[int64Value1].ParsedComposition = true;
                    long int64Value2 = DataSetProcessor.GetInt64Value(dataTable.Rows[index3], 0, 0L);
                    int int32Value = DataSetProcessor.GetInt32Value(dataTable.Rows[index3], 1, -1);
                    if (int64Value2 != 0L && int32Value != -1 && !sortedDictionary1.ContainsKey(int64Value2))
                    {
                      PdmAnalyzedOptionObject analyzedOptionObject = optionObjects.FindObject(int64Value2);
                      if (analyzedOptionObject == null)
                      {
                        analyzedOptionObject = new PdmAnalyzedOptionObject(optionObjects, int64Value2);
                        optionObjects.Add(analyzedOptionObject);
                        ++num;
                      }
                      analyzedOptionObject.LoadDescription(session, dataTable.Rows[index3]);
                      if (excludedObjects != null && excludedObjects.IndexOf(int64Value2) >= 0)
                        analyzedOptionObject.Options = new List<long>();
                      analyzedOptionObject.CheckOptions(session, options, excludedOptions);
                      analyzedOptionObject.ParsedObject = true;
                    }
                  }
                  dataTable.Dispose();
                }
              }
            }
          }
        }
        for (int index = 0; index < keyValuePair.Value.Count; ++index)
          sortedDictionary3[keyValuePair.Value[index]].ParsedComposition = true;
      }
    }
    List<PdmAnalyzedOptionObject> objects = optionObjects.ExtractObjects();
    bool flag = false;
    for (int index = 0; index < objects.Count; ++index)
    {
      flag = !objects[index].ParsedComposition;
      if (flag)
        break;
    }
    if (num > 0 | flag && (options & PdmAnalyzerFlags.InCompositionsRecursive) == PdmAnalyzerFlags.InCompositionsRecursive)
      num += this.Analyze(session, optionObjects, options, excludedObjects, excludedOptions);
    return num;
  }
}

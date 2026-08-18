// Decompiled with JetBrains decompiler
// Type: Intermech.PdmConfigurator.Server.PdmOptionsAnalyzer
// Assembly: Intermech.PdmConfigurator.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 80F94CD1-7E39-423C-8BC4-966315C23D3C
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.PdmConfigurator.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.PdmConfigurator.Server;

internal class PdmOptionsAnalyzer : IPdmOptionsAnalyzer
{
  private Guid guid = Guid.NewGuid();

  public virtual Guid Guid
  {
    [DebuggerStepThrough] get => this.guid;
  }

  public virtual int Analyze(
    IUserSession session,
    PdmAnalyzedOptionObjects optionObjects,
    PdmAnalyzerFlags options)
  {
    return this.Analyze(session, optionObjects, options, (IList<long>) null, (IList<long>) null);
  }

  public virtual int Analyze(
    IUserSession session,
    PdmAnalyzedOptionObjects optionObjects,
    PdmAnalyzerFlags options,
    IList<long> excludedObjects,
    IList<long> excludedOptions)
  {
    int num = 0;
    if (session == null || optionObjects == null || optionObjects.Count == 0)
      return num;
    optionObjects.CheckObjects(excludedObjects);
    SortedDictionary<long, bool> sortedDictionary1 = new SortedDictionary<long, bool>();
    SortedDictionary<int, List<long>> sortedDictionary2 = new SortedDictionary<int, List<long>>();
    for (int index = 0; index < optionObjects.Count; ++index)
    {
      PdmAnalyzedOptionObject optionObject = optionObjects[index];
      if (optionObject.ParsedObject)
      {
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
    foreach (KeyValuePair<int, List<long>> keyValuePair in sortedDictionary2)
    {
      if (keyValuePair.Value.Count != 0)
      {
        IDBObjectCollection objectCollection = session.GetObjectCollection(keyValuePair.Key);
        List<long> longList = new List<long>((IEnumerable<long>) keyValuePair.Value);
        while (longList.Count > 0)
        {
          long[] numArray;
          if (longList.Count > 500)
          {
            numArray = new long[500];
            longList.CopyTo(0, numArray, 0, 500);
            longList.RemoveRange(0, 500);
          }
          else
          {
            numArray = new long[longList.Count];
            longList.CopyTo(numArray);
            longList.Clear();
          }
          DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
          {
            numArray.Length > 1 ? new ConditionStructure(-2, RelationalOperators.In, (object) numArray, LogicalOperators.NONE, 0, true) : new ConditionStructure(-2, RelationalOperators.Equal, (object) numArray[0], LogicalOperators.NONE, 0, true)
          }, PdmAnalyzedOptionObject.GetColumnDescriptors().ToArray());
          DataTable dataTable;
          try
          {
            dataTable = objectCollection.Select(paramSet);
          }
          catch
          {
            dataTable = (DataTable) null;
          }
          if (dataTable != null && dataTable.Rows.Count != 0)
          {
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 0, 0L);
              if (int64Value != 0L && !sortedDictionary1.ContainsKey(int64Value))
              {
                PdmAnalyzedOptionObject analyzedOptionObject = optionObjects.FindObject(int64Value);
                if (analyzedOptionObject == null)
                {
                  analyzedOptionObject = new PdmAnalyzedOptionObject(optionObjects, int64Value);
                  optionObjects.Add(analyzedOptionObject);
                  ++num;
                }
                analyzedOptionObject.LoadDescription(session, dataTable.Rows[index]);
                analyzedOptionObject.CheckOptions(session, options, excludedOptions);
                analyzedOptionObject.ParsedObject = true;
                sortedDictionary1[analyzedOptionObject.ObjectID] = true;
                ++num;
              }
            }
            dataTable.Dispose();
          }
        }
      }
    }
    for (int index = 0; index < optionObjects.Count; ++index)
    {
      PdmAnalyzedOptionObject optionObject = optionObjects[index];
      if (optionObject.Items != null && optionObject.Items.Count > 0)
        num += this.Analyze(session, optionObject.Items, options, excludedObjects, excludedOptions);
    }
    return num;
  }
}

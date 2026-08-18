// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.EditingContextsForObjectsWithCompositionsAnalyzer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

public sealed class EditingContextsForObjectsWithCompositionsAnalyzer : ISearchGroupingObjectAnalyzer
{
  private EditingContextsForObjectsAnalyzer _editingContextsForObjectsAnalyzer;

  public EditingContextsForObjectsWithCompositionsAnalyzer(
    EditingContextsForObjectsAnalyzer editingContextsForObjectsAnalyzer)
  {
    this._editingContextsForObjectsAnalyzer = editingContextsForObjectsAnalyzer != null ? editingContextsForObjectsAnalyzer : throw new ArgumentNullException(nameof (editingContextsForObjectsAnalyzer));
  }

  public string Name => "Поиск среди выделенных версий и в их составах первого уровня";

  public int Analyze(IUserSession session, SearchGroupingObjects searchObjects)
  {
    if (session == null || searchObjects == null || searchObjects.Count == 0)
      return 0;
    int num = 0;
    SortedDictionary<int, List<long>> sortedDictionary = new SortedDictionary<int, List<long>>();
    for (int index = 0; index < searchObjects.Count; ++index)
    {
      SearchGroupingObject searchObject = searchObjects[index];
      if (!sortedDictionary.ContainsKey(searchObject.ObjectTypeID))
        sortedDictionary.Add(searchObject.ObjectTypeID, new List<long>());
      List<long> longList = sortedDictionary[searchObject.ObjectTypeID];
      if (!longList.Contains(searchObject.ObjectID))
        longList.Add(searchObject.ObjectID);
    }
    foreach (KeyValuePair<int, List<long>> keyValuePair in sortedDictionary)
      keyValuePair.Value.Sort();
    CompositionsAutosortRule autosortRule = (session.GetCustomService(typeof (ICompositionsAutomaticSortingService)) as ICompositionsAutomaticSortingService).GetAutosortRule((object) session.SessionGUID, false);
    foreach (KeyValuePair<int, List<long>> keyValuePair in sortedDictionary)
    {
      if (keyValuePair.Value.Count != 0)
      {
        List<int> visibleRelations = autosortRule.GetObjectTypeVisibleRelations(keyValuePair.Key, true);
        if (visibleRelations.Count != 0)
        {
          for (int index1 = 0; index1 < visibleRelations.Count; ++index1)
          {
            IDBRelationCollection relationCollection = session.GetRelationCollection(visibleRelations[index1], "cad001e0-306c-11d8-b4e9-00304f19f545");
            if (relationCollection != null)
            {
              List<int> childObjectTypesId = MetaDataHelper.GetApplicabilityChildObjectTypesID(keyValuePair.Key, visibleRelations[index1]);
              if (childObjectTypesId.Count == 1)
                relationCollection.ObjectTypeID = childObjectTypesId[0];
              else
                relationCollection.LocalTypesMode = true;
              ColumnDescriptor[] columns = new ColumnDescriptor[2]
              {
                new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0),
                new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
              };
              DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
              {
                new ConditionStructure(-21, RelationalOperators.In, (object) keyValuePair.Value.ToArray(), LogicalOperators.AND, 0, true)
              }, columns);
              DataTable dataTable;
              try
              {
                dataTable = relationCollection.Select(paramSet);
              }
              catch
              {
                dataTable = (DataTable) null;
              }
              if (dataTable != null)
              {
                for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
                {
                  object obj1 = dataTable.Rows[index2][0];
                  long result1 = 0;
                  if (obj1 != null && obj1 != DBNull.Value && long.TryParse(obj1.ToString(), out result1))
                  {
                    object obj2 = dataTable.Rows[index2][1];
                    int result2 = 0;
                    if (obj2 != null && obj2 != DBNull.Value && int.TryParse(obj2.ToString(), out result2))
                    {
                      SearchGroupingObject searchGroupingObject1 = searchObjects.FindObject(result1);
                      if (searchGroupingObject1 == null)
                      {
                        SearchGroupingObject searchGroupingObject2 = new SearchGroupingObject(result1, result2, -1L, -1);
                        searchObjects.Add(searchGroupingObject2);
                        ++num;
                      }
                      else
                        searchGroupingObject1.ObjectTypeID = result2;
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    return num + this._editingContextsForObjectsAnalyzer.Analyze(session, searchObjects);
  }
}

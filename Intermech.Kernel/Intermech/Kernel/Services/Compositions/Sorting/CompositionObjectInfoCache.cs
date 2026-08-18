// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.Compositions.Sorting.CompositionObjectInfoCache
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.AutomaticSorting;
using Intermech.Kernel.Search;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;


namespace Intermech.Kernel.Services.Compositions.Sorting;

internal class CompositionObjectInfoCache
{
  private readonly object _dataSyncRoot = new object();
  private readonly ICompositionSortingComparer<CompositionSortingInfoItem> _comparer;
  private readonly IDictionary<ObjInfoItem, CompositionObjectInfo> _data = (IDictionary<ObjInfoItem, CompositionObjectInfo>) new ConcurrentDictionary<ObjInfoItem, CompositionObjectInfo>();

  public CompositionObjectInfoCache(
    [NotNull] ICompositionSortingComparer<CompositionSortingInfoItem> comparer)
  {
    this._comparer = comparer;
    if (comparer.DirectionMode != CompositionSortingDirectionMode.Desc)
      throw new NotSupportedException($"DirectionMode = {comparer.DirectionMode} not supported");
  }

  public void LoadData([NotNull] IUserSession userSession, [NotNull] IEnumerable<ObjInfoItem> objectItems)
  {
    ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) ApplicationServices.Container, true);
    lock (this._dataSyncRoot)
    {
      List<ObjInfoItem> list = objectItems.Where<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => !this._data.ContainsKey(item))).ToList<ObjInfoItem>();
      SomeTypedInfoHelper<ObjInfoItem>.RemoveDuplicateEmpty(list);
      if (list.Count == 0)
        return;
      Dictionary<long, CompositionObjectInfo> dictionary = new Dictionary<long, CompositionObjectInfo>();
      foreach (ObjInfoItem key in list)
      {
        if (!ObjInfoItem.IsEmpty((ITypedInfoItem) key))
        {
          CompositionObjectInfo compositionObjectInfo = new CompositionObjectInfo(key.ObjectID, this._comparer);
          this._data[key] = compositionObjectInfo;
          dictionary[key.ObjectID] = compositionObjectInfo;
        }
      }
      ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) list, userSession);
      List<int> objectTypes = ObjInfoHelper.GetObjectTypes((IEnumerable<ObjInfoItem>) list);
      List<int> intList = new List<int>();
      foreach (int ObjectType in objectTypes)
        intList.AddRange((IEnumerable<int>) this._comparer.SortingRule.GetObjectTypeVisibleRelations(ObjectType, true));
      GenericListHelper.MakeUnique<int>(intList);
      DataTable dataTable1 = service.LoadComplexCompositions((object) userSession, (IEnumerable<ObjInfoItem>) list, (IEnumerable<int>) intList, (IEnumerable<int>) null, CompositionSortingInfoDbScheme.GetSourceTableColumns(), true, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, "cad001e2-306c-11d8-b4e9-00304f19f545", (Dictionary<long, HybridDictionary>) null, 1);
      if (dataTable1 == null || dataTable1.Rows.Count == 0)
        return;
      CompositionObjectInfoCache.UpdateProjTypeInfo((ICollection<ObjInfoItem>) list, dataTable1);
      DataTable dataTable2 = CompositionObjectInfoCache.SortTableByRule(dataTable1, this._comparer.SortingRule) ?? dataTable1;
      int columnIndex = dataTable1.Columns.IndexOf("F_PROJ_ID");
      CompositionSortingInfoDbScheme sortingInfoDbScheme = new CompositionSortingInfoDbScheme();
      for (int index = dataTable2.Rows.Count - 1; index >= 0; --index)
      {
        DataRow row = dataTable2.Rows[index];
        long int64 = Convert.ToInt64(row[columnIndex]);
        CompositionObjectInfo compositionObjectInfo;
        if (dictionary.TryGetValue(int64, out compositionObjectInfo))
        {
          CompositionSortingInfoItem compositionSortingInfoItem = sortingInfoDbScheme.ParseItem(row);
          if (compositionSortingInfoItem.Sorting != -1L && compositionSortingInfoItem.Sorting != 0L)
            compositionObjectInfo.CompositionInfoCache.AddItem(compositionSortingInfoItem);
        }
      }
    }
  }

  public ICompositionSortingComparer<CompositionSortingInfoItem> Сomparer => this._comparer;

  public IDictionary<ObjInfoItem, CompositionObjectInfo> Data
  {
    get
    {
      lock (this._dataSyncRoot)
        return this._data;
    }
  }

  private static void UpdateProjTypeInfo([NotNull] ICollection<ObjInfoItem> objectItems, [NotNull] DataTable dataTable)
  {
    if (!objectItems.Any<ObjInfoItem>() || dataTable.Rows.Count == 0)
      return;
    int columnIndex1 = dataTable.Columns.IndexOf("F_PROJ_ID");
    if (columnIndex1 == -1)
      return;
    int columnIndex2 = dataTable.Columns.IndexOf("F_PROJ_TYPE");
    if (columnIndex2 == -1)
      columnIndex2 = dataTable.Columns.Add("F_PROJ_TYPE", typeof (int)).Ordinal;
    IDictionary<long, int> objectCache = (IDictionary<long, int>) ObjInfoHelper.GetObjectCache((IEnumerable<ObjInfoItem>) objectItems);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row[columnIndex1]);
      int num;
      if (objectCache.TryGetValue(int64, out num))
        row[columnIndex2] = (object) num;
      else if (row[columnIndex2].Equals((object) DBNull.Value))
        row[columnIndex2] = (object) -1;
    }
  }

  public static DataTable SortTableByRule([NotNull] DataTable dataTable, [NotNull] CompositionsAutosortRule sortRule)
  {
    if (dataTable.Rows.Count == 0)
      return (DataTable) null;
    int num1 = dataTable.Columns.IndexOf("F_PROJ_TYPE");
    int num2 = dataTable.Columns.IndexOf("F_RELATION_TYPE");
    int num3 = dataTable.Columns.IndexOf("F_OBJECT_TYPE");
    int num4 = dataTable.Columns.IndexOf("cad00202-306c-11d8-b4e9-00304f19f545");
    if (num1 == -1 || num2 == -1 || num3 == -1 || num4 == -1)
      return (DataTable) null;
    CompositionSortingColumnInfo columnsInfo = new CompositionSortingColumnInfo()
    {
      idx_ProjType = num1,
      idx_RelType = num2,
      idx_PartType = num3,
      idx_Sorting = num4,
      SortingRule = sortRule
    };
    DataTable dataTable1 = dataTable.Copy();
    int ordinal = dataTable1.Columns.Add("F_RULE_SORT", typeof (CompositionSortingValue)).Ordinal;
    try
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
        row[ordinal] = (object) new CompositionSortingValue(row, columnsInfo);
      dataTable1.DefaultView.Sort = "F_RULE_SORT";
      dataTable1 = dataTable1.DefaultView.ToTable();
      return dataTable1;
    }
    finally
    {
      dataTable1.Columns.Remove("F_RULE_SORT");
    }
  }

  private static class Consts
  {
    public const string ProjTypeField = "F_PROJ_TYPE";
    public const string RuleSortField = "F_RULE_SORT";
  }
}

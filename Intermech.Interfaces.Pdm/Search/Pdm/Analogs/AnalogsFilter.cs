// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Analogs.AnalogsFilter
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Search.Data.Adapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Search.Pdm.Analogs;

public sealed class AnalogsFilter
{
  private LazyService<IElementStatusesService> _elementStatusesService = new LazyService<IElementStatusesService>();

  public void Filter(
    IUserSession userSession,
    DataTable dataTable,
    DBRecordSetParams recordSetParams)
  {
    if (userSession == null)
      throw new ArgumentNullException(nameof (userSession));
    if (dataTable == null)
      throw new ArgumentNullException(nameof (dataTable));
    AnalogSelectionMode analogSelectionMode = recordSetParams.ColumnsInfo != null ? AnalogsHelper.GetAnalogSelectionModeFromRecordSetParams(recordSetParams) : throw new ArgumentException();
    if (analogSelectionMode == AnalogSelectionMode.None)
      return;
    IEnumerable<CompositionPart> partsFromDataTable = CompositionPartHelper.CreateCompositionPartsFromDataTable(dataTable, recordSetParams);
    long[] objectVersionIds = this.GetAnalogsSupportedObjectVersionIds(partsFromDataTable);
    if (objectVersionIds.Length == 0)
      return;
    Dictionary<long, List<CompositionPart>> byObjectVersionId = this.FindAnalogsAndCreateAnalogsDictionaryByObjectVersionID(userSession, objectVersionIds);
    DateTime selectionActualDate = this.GetAnalogSelectionActualDate(recordSetParams);
    List<AnalogsFilter.AnalogFiltrationResultEntry> filtrationResultEntryList = new List<AnalogsFilter.AnalogFiltrationResultEntry>();
    foreach (CompositionPart compositionPart in partsFromDataTable)
    {
      if (byObjectVersionId.ContainsKey(compositionPart.Object.VersionID))
      {
        List<CompositionPart> analogs = byObjectVersionId[compositionPart.Object.VersionID];
        switch (analogSelectionMode)
        {
          case AnalogSelectionMode.ActualAnalog:
            AnalogsSelectionStatuses statuses1 = AnalogsSelectionStatuses.None;
            CompositionPart analog1 = this.GetSingleActualAnalog((IEnumerable<CompositionPart>) analogs, selectionActualDate);
            if (analog1 != null)
            {
              statuses1 = AnalogsSelectionStatuses.ActualAnalog | AnalogsSelectionStatuses.PriorityOrOneAnalog;
            }
            else
            {
              analog1 = this.GetFirstPriorityActualAnalog((IEnumerable<CompositionPart>) analogs, selectionActualDate);
              if (analog1 != null)
              {
                statuses1 = AnalogsSelectionStatuses.ActualAnalog | AnalogsSelectionStatuses.PriorityOrOneAnalog;
              }
              else
              {
                analog1 = this.GetFirstActualAnalog((IEnumerable<CompositionPart>) analogs, selectionActualDate);
                if (analog1 != null)
                {
                  statuses1 = AnalogsSelectionStatuses.ActualAnalog;
                }
                else
                {
                  if (this.IsAllEmptyDatesAnalogs((IEnumerable<CompositionPart>) analogs))
                  {
                    analog1 = this.GetFirstPriorityAnalog((IEnumerable<CompositionPart>) analogs);
                    if (analog1 != null)
                      statuses1 = AnalogsSelectionStatuses.PriorityOrOneAnalog;
                  }
                  if (analog1 == null && analogs.Count == 1 && this.IsEmptyDatesAnalog(analogs[0]))
                  {
                    analog1 = analogs[0];
                    statuses1 = AnalogsSelectionStatuses.PriorityOrOneAnalog;
                  }
                }
              }
            }
            if (analog1 != null)
            {
              filtrationResultEntryList.Add(new AnalogsFilter.AnalogFiltrationResultEntry(compositionPart, analog1, statuses1));
              continue;
            }
            filtrationResultEntryList.Add(new AnalogsFilter.AnalogFiltrationResultEntry(compositionPart, AnalogsSelectionStatuses.AnalogsExist));
            continue;
          case AnalogSelectionMode.OneAnalog:
            AnalogsSelectionStatuses statuses2 = AnalogsSelectionStatuses.None;
            CompositionPart analog2;
            if (analogs.Count == 1)
            {
              analog2 = analogs[0];
              statuses2 = AnalogsSelectionStatuses.PriorityOrOneAnalog;
              if (this.IsActualAnalog(analog2, selectionActualDate))
                statuses2 |= AnalogsSelectionStatuses.ActualAnalog;
            }
            else
            {
              analog2 = this.GetFirstPriorityActualAnalog((IEnumerable<CompositionPart>) analogs, selectionActualDate);
              if (analog2 != null)
              {
                statuses2 = AnalogsSelectionStatuses.ActualAnalog | AnalogsSelectionStatuses.PriorityOrOneAnalog;
              }
              else
              {
                analog2 = this.GetFirstActualAnalog((IEnumerable<CompositionPart>) analogs, selectionActualDate);
                if (analog2 != null)
                {
                  statuses2 = AnalogsSelectionStatuses.ActualAnalog;
                }
                else
                {
                  analog2 = this.GetFirstPriorityEmptyDatesAnalog((IEnumerable<CompositionPart>) analogs);
                  if (analog2 != null)
                  {
                    statuses2 = AnalogsSelectionStatuses.PriorityOrOneAnalog;
                  }
                  else
                  {
                    analog2 = this.GetFirstActualAnalog((IEnumerable<CompositionPart>) analogs, selectionActualDate);
                    if (analog2 != null)
                    {
                      statuses2 = AnalogsSelectionStatuses.ActualAnalog | AnalogsSelectionStatuses.Analog;
                    }
                    else
                    {
                      analog2 = this.GetFirstEmptyDatesAnalog((IEnumerable<CompositionPart>) analogs);
                      if (analog2 != null)
                      {
                        statuses2 = AnalogsSelectionStatuses.Analog;
                      }
                      else
                      {
                        analog2 = this.GetFirstPriorityAnalog((IEnumerable<CompositionPart>) analogs);
                        if (analog2 != null)
                        {
                          statuses2 = AnalogsSelectionStatuses.PriorityOrOneAnalog;
                        }
                        else
                        {
                          analog2 = this.GetFirstNotEmptyDatesAnalog((IEnumerable<CompositionPart>) analogs);
                          if (analog2 != null)
                            statuses2 = AnalogsSelectionStatuses.Analog;
                        }
                      }
                    }
                  }
                }
              }
            }
            if (analog2 != null)
            {
              filtrationResultEntryList.Add(new AnalogsFilter.AnalogFiltrationResultEntry(compositionPart, analog2, statuses2));
              continue;
            }
            continue;
          case AnalogSelectionMode.AllAnalogs:
            filtrationResultEntryList.Add(new AnalogsFilter.AnalogFiltrationResultEntry(compositionPart, AnalogsSelectionStatuses.AnalogsExist));
            using (List<CompositionPart>.Enumerator enumerator = analogs.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                CompositionPart current = enumerator.Current;
                AnalogsSelectionStatuses statuses3 = AnalogsSelectionStatuses.None;
                if (this.IsActualAnalog(current, selectionActualDate))
                  statuses3 |= AnalogsSelectionStatuses.ActualAnalog;
                if (this.IsPriorityAnalog(current))
                  statuses3 |= AnalogsSelectionStatuses.PriorityOrOneAnalog;
                if (!statuses3.HasFlag((Enum) AnalogsSelectionStatuses.ActualAnalog) && !statuses3.HasFlag((Enum) AnalogsSelectionStatuses.PriorityOrOneAnalog))
                  statuses3 |= AnalogsSelectionStatuses.Analog;
                filtrationResultEntryList.Add(new AnalogsFilter.AnalogFiltrationResultEntry(compositionPart, current, statuses3));
              }
              continue;
            }
          default:
            continue;
        }
      }
      else
        filtrationResultEntryList.Add(new AnalogsFilter.AnalogFiltrationResultEntry(compositionPart));
    }
    object[][] filtrationResult = this.CreateDataTableBlankFromFiltrationResult(userSession, filtrationResultEntryList.ToArray(), recordSetParams);
    dataTable.Rows.Clear();
    foreach (object[] objArray in filtrationResult)
      dataTable.Rows.Add(objArray);
  }

  private long[] GetAnalogsSupportedObjectVersionIds(IEnumerable<CompositionPart> compositionParts)
  {
    return compositionParts.Where<CompositionPart>((System.Func<CompositionPart, bool>) (o => AnalogsHelper.IsObjectTypeSupportedAnalogs(o.Object.TypeID))).Select<CompositionPart, long>((System.Func<CompositionPart, long>) (o => o.Object.VersionID)).Distinct<long>().ToArray<long>();
  }

  private Dictionary<long, List<CompositionPart>> FindAnalogsAndCreateAnalogsDictionaryByObjectVersionID(
    IUserSession userSession,
    long[] objectVersionIds)
  {
    Dictionary<long, List<CompositionPart>> byObjectVersionId = new Dictionary<long, List<CompositionPart>>();
    IDBRelationCollection relationCollection = userSession.GetRelationCollection(AnalogsConstants.AnalogsRelationTypeID);
    relationCollection.LocalTypesMode = true;
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure()
      {
        Attribute = (object) ObligatoryObjectAttributes.F_PROJ_ID,
        RelationalOperator = RelationalOperators.In,
        Value = (object) objectVersionIds,
        SQL = string.Empty
      }
    };
    ColumnDescriptor[] columnDescriptorArray = new ColumnDescriptor[7];
    columnDescriptorArray[0] = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID);
    columnDescriptorArray[1] = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID);
    ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) AnalogsConstants.StartDateAttributeTypeID);
    columnDescriptor.AttributeSource = AttributeSourceTypes.Relation;
    columnDescriptorArray[2] = columnDescriptor;
    columnDescriptor = new ColumnDescriptor((object) AnalogsConstants.EndDateAttributeTypeID);
    columnDescriptor.AttributeSource = AttributeSourceTypes.Relation;
    columnDescriptorArray[3] = columnDescriptor;
    columnDescriptor = new ColumnDescriptor((object) AnalogsConstants.PriorityAnalogAttributeTypeID);
    columnDescriptor.AttributeSource = AttributeSourceTypes.Relation;
    columnDescriptorArray[4] = columnDescriptor;
    columnDescriptorArray[5] = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID);
    columnDescriptorArray[6] = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE);
    ColumnDescriptor[] columns = columnDescriptorArray;
    DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(conditions, columns);
    foreach (CompositionPart compositionPart in CompositionPartHelper.CreateCompositionPartsFromDataTable(relationCollection.Select(dbRecordSetParams), dbRecordSetParams))
    {
      List<CompositionPart> compositionPartList = (List<CompositionPart>) null;
      if (!byObjectVersionId.TryGetValue(compositionPart.Relation.ProjectVersionID, out compositionPartList))
      {
        compositionPartList = new List<CompositionPart>();
        byObjectVersionId.Add(compositionPart.Relation.ProjectVersionID, compositionPartList);
      }
      compositionPartList.Add(compositionPart);
    }
    return byObjectVersionId;
  }

  private DateTime GetAnalogSelectionActualDate(DBRecordSetParams recordSetParams)
  {
    VersionsRule fromRecordSetParams1 = CoreHelper.GetVersionsRuleFromRecordSetParams(recordSetParams);
    if (fromRecordSetParams1 != null && fromRecordSetParams1.ActualDate != DateTime.MinValue)
      return fromRecordSetParams1.ActualDate;
    SeriesDateSettingsHolder fromRecordSetParams2 = CoreHelper.GetSeriesAndDatesSettingsHolderFromRecordSetParams(recordSetParams);
    return fromRecordSetParams2 != null && fromRecordSetParams2.Date != DateTime.MinValue ? fromRecordSetParams2.Date : DateTime.Now;
  }

  private CompositionPart GetSingleActualAnalog(
    IEnumerable<CompositionPart> analogs,
    DateTime actualDate)
  {
    IEnumerable<CompositionPart> actualAnalogs = this.GetActualAnalogs(analogs, actualDate);
    return actualAnalogs.Count<CompositionPart>() != 1 ? (CompositionPart) null : actualAnalogs.First<CompositionPart>();
  }

  private IEnumerable<CompositionPart> GetActualAnalogs(
    IEnumerable<CompositionPart> analogs,
    DateTime actualDate)
  {
    return analogs.Where<CompositionPart>((System.Func<CompositionPart, bool>) (o => this.IsActualAnalog(o, actualDate)));
  }

  private bool IsActualAnalog(CompositionPart analog, DateTime actualDate)
  {
    DateTime dateTime1 = (DateTime) (analog.Relation.Attributes.GetAttributeValue(AnalogsConstants.StartDateAttributeTypeID) ?? (object) DateTime.MinValue);
    DateTime dateTime2 = (DateTime) (analog.Relation.Attributes.GetAttributeValue(AnalogsConstants.EndDateAttributeTypeID) ?? (object) DateTime.MinValue);
    return dateTime1 != DateTime.MinValue && dateTime2 != DateTime.MinValue && dateTime1 <= actualDate && actualDate <= dateTime2;
  }

  private CompositionPart GetFirstPriorityActualAnalog(
    IEnumerable<CompositionPart> analogs,
    DateTime actualDate)
  {
    return this.GetFirstPriorityAnalog(this.GetActualAnalogs(analogs, actualDate));
  }

  private CompositionPart GetFirstPriorityAnalog(IEnumerable<CompositionPart> analogs)
  {
    return analogs.FirstOrDefault<CompositionPart>((System.Func<CompositionPart, bool>) (o => this.IsPriorityAnalog(o)));
  }

  private bool IsPriorityAnalog(CompositionPart analog)
  {
    return (bool) analog.Relation.Attributes.GetAttributeValue(AnalogsConstants.PriorityAnalogAttributeTypeID);
  }

  private CompositionPart GetFirstActualAnalog(
    IEnumerable<CompositionPart> analogs,
    DateTime actualDate)
  {
    return this.GetActualAnalogs(analogs, actualDate).FirstOrDefault<CompositionPart>();
  }

  private bool IsAllEmptyDatesAnalogs(IEnumerable<CompositionPart> analogs)
  {
    return analogs.All<CompositionPart>((System.Func<CompositionPart, bool>) (o => this.IsEmptyDatesAnalog(o)));
  }

  private bool IsEmptyDatesAnalog(CompositionPart analog)
  {
    return (DateTime) (analog.Relation.Attributes.GetAttributeValue(AnalogsConstants.StartDateAttributeTypeID) ?? (object) DateTime.MinValue) == DateTime.MinValue && (DateTime) (analog.Relation.Attributes.GetAttributeValue(AnalogsConstants.EndDateAttributeTypeID) ?? (object) DateTime.MinValue) == DateTime.MinValue;
  }

  private CompositionPart GetFirstPriorityEmptyDatesAnalog(IEnumerable<CompositionPart> analogs)
  {
    return this.GetFirstPriorityAnalog(this.GetEmptyDatesAnalogs(analogs));
  }

  private IEnumerable<CompositionPart> GetEmptyDatesAnalogs(IEnumerable<CompositionPart> analogs)
  {
    return analogs.Where<CompositionPart>((System.Func<CompositionPart, bool>) (o => this.IsEmptyDatesAnalog(o)));
  }

  private CompositionPart GetFirstEmptyDatesAnalog(IEnumerable<CompositionPart> analogs)
  {
    return this.GetEmptyDatesAnalogs(analogs).FirstOrDefault<CompositionPart>();
  }

  private CompositionPart GetFirstNotEmptyDatesAnalog(IEnumerable<CompositionPart> analogs)
  {
    return analogs.Where<CompositionPart>((System.Func<CompositionPart, bool>) (o => !this.IsEmptyDatesAnalog(o))).FirstOrDefault<CompositionPart>();
  }

  private object[][] CreateDataTableBlankFromFiltrationResult(
    IUserSession userSession,
    AnalogsFilter.AnalogFiltrationResultEntry[] filtrationResult,
    DBRecordSetParams recordSetParams)
  {
    List<int> fromRecordSetParams = this.GetObjectAttributeTypeIdsFromRecordSetParams(recordSetParams);
    fromRecordSetParams.Remove(-2);
    fromRecordSetParams.Insert(0, -2);
    Dictionary<int, List<long>> resultForAnalogs = this.CreateObjectVersionIdsDictionaryByObjectTypeIDFromFiltrationResultForAnalogs(filtrationResult);
    Dictionary<long, object[]> byObjectVersionId = this.FindObjectAttributeValuesAndCreateDictionaryByObjectVersionID(userSession, resultForAnalogs, fromRecordSetParams);
    List<object[]> objArrayList = new List<object[]>();
    foreach (AnalogsFilter.AnalogFiltrationResultEntry entry in filtrationResult)
    {
      object[] filtrationResultEntry = this.CreateDataRowItemArrayFromAnalogFiltrationResultEntry(entry, byObjectVersionId, fromRecordSetParams.ToArray(), recordSetParams);
      objArrayList.Add(filtrationResultEntry);
    }
    return objArrayList.ToArray();
  }

  private List<int> GetObjectAttributeTypeIdsFromRecordSetParams(DBRecordSetParams recordSetParams)
  {
    List<int> fromRecordSetParams = new List<int>();
    foreach (ColumnInfo columnInfo in recordSetParams.ColumnsInfo)
    {
      int attributeTypeId = CoreHelper.GetAttributeTypeID(columnInfo);
      if (CoreHelper.GetAttributeSourceType(columnInfo) == AttributeSourceTypes.Object && !fromRecordSetParams.Contains(attributeTypeId))
        fromRecordSetParams.Add(attributeTypeId);
    }
    return fromRecordSetParams;
  }

  private Dictionary<int, List<long>> CreateObjectVersionIdsDictionaryByObjectTypeIDFromFiltrationResultForAnalogs(
    AnalogsFilter.AnalogFiltrationResultEntry[] filtrationResult)
  {
    Dictionary<int, List<long>> resultForAnalogs = new Dictionary<int, List<long>>();
    foreach (AnalogsFilter.AnalogFiltrationResultEntry filtrationResultEntry in filtrationResult)
    {
      if (filtrationResultEntry.Analog != null)
      {
        List<long> longList = (List<long>) null;
        if (!resultForAnalogs.TryGetValue(filtrationResultEntry.Analog.Object.TypeID, out longList))
        {
          longList = new List<long>();
          resultForAnalogs.Add(filtrationResultEntry.Analog.Object.TypeID, longList);
        }
        if (!longList.Contains(filtrationResultEntry.Analog.Object.VersionID))
          longList.Add(filtrationResultEntry.Analog.Object.VersionID);
      }
    }
    return resultForAnalogs;
  }

  private Dictionary<long, object[]> FindObjectAttributeValuesAndCreateDictionaryByObjectVersionID(
    IUserSession userSession,
    Dictionary<int, List<long>> objectVersionIdsDictionaryByObjectTypeID,
    List<int> attributeTypeIds)
  {
    Dictionary<long, object[]> byObjectVersionId = new Dictionary<long, object[]>();
    foreach (KeyValuePair<int, List<long>> keyValuePair in objectVersionIdsDictionaryByObjectTypeID)
    {
      IDBObjectCollection objectCollection = userSession.GetObjectCollection(keyValuePair.Key);
      objectCollection.ShowAllModifications = true;
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
      // ISSUE: explicit reference operation
      (^ref dbRecordSetParams).Conditions = new ConditionStructure[1]
      {
        new ConditionStructure()
        {
          Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          RelationalOperator = RelationalOperators.In,
          Value = (object) keyValuePair.Value.ToArray(),
          SQL = string.Empty
        }
      };
      dbRecordSetParams.Columns = attributeTypeIds.Cast<object>().ToArray<object>();
      dbRecordSetParams.RecordCount = -1;
      DBRecordSetParams paramSet = dbRecordSetParams;
      foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
      {
        long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
        byObjectVersionId.Add(int64Value, row.ItemArray);
      }
    }
    return byObjectVersionId;
  }

  private object[] CreateDataRowItemArrayFromAnalogFiltrationResultEntry(
    AnalogsFilter.AnalogFiltrationResultEntry entry,
    Dictionary<long, object[]> analogAttributeValuesDictionaryByObjectVersionID,
    int[] analogObjectAttributeTypeIds,
    DBRecordSetParams recordSetParams)
  {
    if (entry.Analog != null)
    {
      object[] objArray = analogAttributeValuesDictionaryByObjectVersionID[entry.Analog.Object.VersionID];
      List<object> objectList = new List<object>();
      int num = Math.Min(recordSetParams.ColumnsInfo.Length, ((AttributeCollectionDataRowAdapter) entry.CompositionPart.Object.Attributes).DataRow.ItemArray.Length);
      for (int index = 0; index < num; ++index)
      {
        ColumnInfo columnInfo = recordSetParams.ColumnsInfo[index];
        int attributeTypeId = CoreHelper.GetAttributeTypeID(columnInfo);
        AttributeSourceTypes attributeSourceType = CoreHelper.GetAttributeSourceType(columnInfo);
        if (attributeTypeId == -77)
        {
          byte[] elementStatuses = (byte[]) entry.CompositionPart.Object.Statuses.Clone();
          this._elementStatusesService.Value.SetElementStatuses32("2B55A281-C8CE-4D0E-9F78-737301FA9369", elementStatuses, (int) entry.Statuses);
          objectList.Add((object) elementStatuses);
        }
        else if (attributeSourceType == AttributeSourceTypes.Object)
          objectList.Add(objArray[Array.IndexOf<int>(analogObjectAttributeTypeIds, attributeTypeId)]);
        else
          objectList.Add(((AttributeCollectionDataRowAdapter) entry.CompositionPart.Object.Attributes).DataRow.ItemArray[index]);
      }
      return objectList.ToArray();
    }
    object[] filtrationResultEntry = (object[]) ((AttributeCollectionDataRowAdapter) entry.CompositionPart.Object.Attributes).DataRow.ItemArray.Clone();
    if (((IEnumerable<object>) recordSetParams.Columns).Contains<object>((object) -77))
    {
      int index = Array.IndexOf<object>(recordSetParams.Columns, (object) -77);
      byte[] elementStatuses = (byte[]) ((Array) filtrationResultEntry[index]).Clone();
      this._elementStatusesService.Value.SetElementStatuses32("2B55A281-C8CE-4D0E-9F78-737301FA9369", elementStatuses, (int) entry.Statuses);
      filtrationResultEntry[index] = (object) elementStatuses;
    }
    return filtrationResultEntry;
  }

  private sealed class AnalogFiltrationResultEntry
  {
    public AnalogFiltrationResultEntry(CompositionPart compositionPart)
    {
      this.CompositionPart = compositionPart != null ? compositionPart : throw new ArgumentNullException(nameof (compositionPart));
    }

    public AnalogFiltrationResultEntry(
      CompositionPart compositionPart,
      AnalogsSelectionStatuses statuses)
    {
      this.CompositionPart = compositionPart != null ? compositionPart : throw new ArgumentNullException(nameof (compositionPart));
      this.Statuses = statuses;
    }

    public AnalogFiltrationResultEntry(
      CompositionPart compositionPart,
      CompositionPart analog,
      AnalogsSelectionStatuses statuses)
    {
      if (compositionPart == null)
        throw new ArgumentNullException(nameof (compositionPart));
      if (analog == null)
        throw new ArgumentNullException(nameof (analog));
      this.CompositionPart = compositionPart;
      this.Analog = analog;
      this.Statuses = statuses;
    }

    public CompositionPart CompositionPart { get; private set; }

    public CompositionPart Analog { get; private set; }

    public AnalogsSelectionStatuses Statuses { get; private set; }
  }
}

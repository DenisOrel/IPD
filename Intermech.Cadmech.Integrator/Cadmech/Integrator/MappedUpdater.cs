// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.MappedUpdater
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class MappedUpdater
{
  private PartGuidAllocator guidAllocator;

  public MappedUpdater() => this.guidAllocator = new PartGuidAllocator();

  public SpecDummy CreateSpecDummy(StructFile structFile)
  {
    return structFile != null ? (SpecDummy) this.InternalCreateSpecDummy(structFile) : throw new ArgumentNullException(nameof (structFile), "Не задан обменный файл.");
  }

  private MappedSpecDummy InternalCreateSpecDummy(StructFile structFile)
  {
    MappedSpecDummy specDummy = new MappedSpecDummy();
    Dictionary<PartData, List<RowData>> dictionary = this.ClusterizeByPart(structFile.Rows);
    for (int index1 = 0; index1 < structFile.Parts.Count; ++index1)
    {
      PartData part1 = structFile.Parts[index1];
      List<RowData> rows;
      if (dictionary.TryGetValue(part1, out rows))
      {
        MappedUpdater.CheckOccurencesFormatConstraint(rows);
        MappedUpdater.CheckPositionsConstraint(rows);
        part1.PartGuid = this.guidAllocator.Allocate(part1, rows);
        string position = this.PopulatePositions(rows);
        PartData part2 = part1.Clone();
        specDummy.Parts.Add(part2);
        SpecRecordMap[] recordMaps = this.CreateRecordMaps(rows);
        this.CreateSpecRecords(part2, position, recordMaps);
        for (int index2 = 0; index2 < recordMaps.Length; ++index2)
        {
          SpecRecordMap specRecordMap = recordMaps[index2];
          specDummy.Records.Add(specRecordMap.Record);
          specDummy.RecordMaps.Add(specRecordMap);
        }
      }
    }
    return specDummy;
  }

  private Dictionary<PartData, List<RowData>> ClusterizeByPart(List<RowData> rows)
  {
    Dictionary<PartData, List<RowData>> dictionary = new Dictionary<PartData, List<RowData>>();
    for (int index = 0; index < rows.Count; ++index)
    {
      RowData row = rows[index];
      List<RowData> rowDataList;
      if (!dictionary.TryGetValue(row.Part, out rowDataList))
      {
        rowDataList = new List<RowData>();
        dictionary.Add(row.Part, rowDataList);
      }
      rowDataList.Add(row);
    }
    return dictionary;
  }

  private string PopulatePositions(List<RowData> rows)
  {
    string str = string.Empty;
    for (int index = 0; index < rows.Count; ++index)
    {
      RowData row = rows[index];
      if (!string.IsNullOrEmpty(row.Position))
      {
        str = row.Position;
        break;
      }
    }
    if (str != string.Empty)
    {
      for (int index = 0; index < rows.Count; ++index)
        rows[index].Position = str;
    }
    return str;
  }

  private SpecRecordMap[] CreateRecordMaps(List<RowData> rows)
  {
    Dictionary<string, SpecRecordMap> dictionary = new Dictionary<string, SpecRecordMap>();
    for (int index1 = 0; index1 < rows.Count; ++index1)
    {
      RowData row = rows[index1];
      for (int index2 = 0; index2 < row.Refs.Count; ++index2)
      {
        string designation = row.Refs[index2].Designation;
        SpecRecordMap specRecordMap;
        if (!dictionary.TryGetValue(designation, out specRecordMap))
        {
          specRecordMap = new SpecRecordMap();
          specRecordMap.ProjectDesignation = designation;
          dictionary.Add(designation, specRecordMap);
        }
        specRecordMap.Rows.Add(row);
      }
    }
    SpecRecordMap[] array = new SpecRecordMap[dictionary.Values.Count];
    dictionary.Values.CopyTo(array, 0);
    return array;
  }

  private void CreateSpecRecords(PartData part, string position, SpecRecordMap[] recordMaps)
  {
    for (int index = 0; index < recordMaps.Length; ++index)
    {
      SpecRecordMap recordMap = recordMaps[index];
      recordMap.Record = this.CreateSpecRecord(part, position, recordMap);
    }
  }

  private SpecRecord CreateSpecRecord(PartData part, string position, SpecRecordMap recordMap)
  {
    string str1 = this.SummarizeZones(recordMap.Rows);
    string str2 = this.SummarizeNotes(recordMap.Rows);
    MeasuredValue measuredValue = this.CalcSummaryCount(recordMap);
    return new SpecRecord()
    {
      ProjectDesignation = recordMap.ProjectDesignation,
      Zone = str1,
      Position = position,
      Part = part,
      Count = measuredValue,
      Note = str2
    };
  }

  private string SummarizeZones(List<RowData> rows)
  {
    IComparer<string> cmp = (IComparer<string>) StringComparer.CurrentCultureIgnoreCase;
    List<string> stringList = new List<string>(rows.Count);
    for (int index = 0; index < rows.Count; ++index)
    {
      string zone = rows[index].Zone;
      if (!string.IsNullOrEmpty(zone) && stringList.FindIndex((Predicate<string>) (item => cmp.Compare(item, zone) == 0)) == -1)
        stringList.Add(zone);
    }
    StringBuilder stringBuilder = new StringBuilder(stringList.Count * 4);
    for (int index = 0; index < stringList.Count; ++index)
    {
      if (index > 0)
        stringBuilder.Append(", ");
      stringBuilder.Append(stringList[index]);
    }
    return stringBuilder.ToString();
  }

  private string SummarizeNotes(List<RowData> rows)
  {
    IComparer<string> cmp = (IComparer<string>) StringComparer.CurrentCultureIgnoreCase;
    List<string> stringList = new List<string>(rows.Count);
    for (int index = 0; index < rows.Count; ++index)
    {
      string note = rows[index].Note;
      if (!string.IsNullOrEmpty(note) && stringList.FindIndex((Predicate<string>) (item => cmp.Compare(item, note) == 0)) == -1)
        stringList.Add(note);
    }
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < stringList.Count; ++index)
    {
      if (index > 0)
        stringBuilder.Append("; ");
      stringBuilder.Append(stringList[index]);
    }
    return stringBuilder.ToString();
  }

  private MeasuredValue CalcSummaryCount(SpecRecordMap recordMap)
  {
    RowData row = recordMap.Rows[0];
    MeasuredValue operand1 = (MeasuredValue) this.GetOccurence(recordMap.ProjectDesignation, row.Refs).Count.Clone();
    for (int index = 1; index < recordMap.Rows.Count; ++index)
    {
      OccurenceRef occurence = this.GetOccurence(recordMap.ProjectDesignation, recordMap.Rows[index].Refs);
      operand1 = MeasureHelper.Add(operand1, occurence.Count);
    }
    return operand1;
  }

  private static void CheckOccurencesFormatConstraint(List<RowData> rows)
  {
    bool flag = rows[0].OccurenceFormat == OccurenceFormat.AllProjects;
    for (int index = 1; index < rows.Count; ++index)
    {
      if (rows[index].OccurenceFormat == OccurenceFormat.AllProjects != flag)
        throw new FaultException("Нарушено ограничение на виды полок в чертеже. Все полки, в которых упоминается компонент, должны описывать либо все исполнения сборочной единицы, либо только некоторые исполнения.");
    }
  }

  private static void CheckPositionsConstraint(List<RowData> rows)
  {
    for (int index1 = 0; index1 < rows.Count - 1; ++index1)
    {
      RowData row1 = rows[index1];
      if (row1.Position != string.Empty)
      {
        for (int index2 = index1 + 1; index2 < rows.Count; ++index2)
        {
          RowData row2 = rows[index2];
          if (row2.Position != string.Empty && row2.Position != row1.Position)
            throw new FaultException("Нарушено ограничение на номера позиций. Все полки, в которых упоминается компонент, должны иметь одну позицию.");
        }
      }
    }
  }

  public UpdateResult UpdateSpecDummy(StructFile structFile, SpecDummy specDummy)
  {
    if (structFile == null)
      throw new ArgumentNullException(nameof (structFile), "Не задан обменный файл.");
    return specDummy is MappedSpecDummy specDummy1 ? this.InternalUpdate(structFile, specDummy1) : throw new ArgumentException(string.Format("Значение свойства specDummy должно быть создано методом CreateSpecDummy."), nameof (specDummy));
  }

  private UpdateResult InternalUpdate(StructFile structFile, MappedSpecDummy specDummy)
  {
    UpdateResult updateResult = new UpdateResult();
    this.MarkUnusedMaps(specDummy);
    this.RemoveUnusedOccurences(specDummy);
    this.RemoveUnusedRows(structFile.Rows);
    this.RemoveUnusedMaps(specDummy);
    MappedUpdater.CheckPartsConstraint(specDummy.RecordMaps);
    MappedUpdater.CheckPositionsConstraint(specDummy.RecordMaps);
    this.UpdateProjects(specDummy);
    this.InsertNewRows(structFile, specDummy, updateResult);
    this.UpdateRows(structFile.Rows, this.CollectRowMaps(specDummy.RecordMaps));
    this.RebuildPartData(structFile);
    return updateResult;
  }

  private void MarkUnusedMaps(MappedSpecDummy specDummy)
  {
    for (int index = 0; index < specDummy.RecordMaps.Count; ++index)
    {
      SpecRecordMap recordMap = specDummy.RecordMaps[index];
      if (specDummy.Records.IndexOf(recordMap.Record) == -1)
        recordMap.Record = (SpecRecord) null;
    }
  }

  private void RemoveUnusedOccurences(MappedSpecDummy specDummy)
  {
    for (int index1 = 0; index1 < specDummy.RecordMaps.Count; ++index1)
    {
      SpecRecordMap recordMap = specDummy.RecordMaps[index1];
      if (recordMap.Record == null)
      {
        for (int index2 = 0; index2 < recordMap.Rows.Count; ++index2)
        {
          RowData row = recordMap.Rows[index2];
          int index3 = this.IndexOfOccurence(recordMap.ProjectDesignation, row.Refs);
          if (index3 >= 0)
            row.Refs.RemoveAt(index3);
        }
      }
    }
  }

  private void RemoveUnusedRows(List<RowData> rows)
  {
    int index = 0;
    while (index < rows.Count)
    {
      if (rows[index].Refs.Count == 0)
        rows.RemoveAt(index);
      else
        ++index;
    }
  }

  private void RemoveUnusedMaps(MappedSpecDummy specDummy)
  {
    int index = 0;
    while (index < specDummy.RecordMaps.Count)
    {
      if (specDummy.RecordMaps[index].Record == null)
        specDummy.RecordMaps.RemoveAt(index);
      else
        ++index;
    }
  }

  private static void CheckPositionsConstraint(List<SpecRecordMap> recordMaps)
  {
    for (int index = 0; index < recordMaps.Count - 1; ++index)
    {
      if (string.IsNullOrEmpty(recordMaps[index].Record.Position))
        throw new FaultException("Не все позиции проставлены. Воспользуйтесь автоматической нумерацией позиций.");
    }
    int num = recordMaps.Count - 1;
    for (int index1 = 0; index1 < num; ++index1)
    {
      SpecRecordMap recordMap1 = recordMaps[index1];
      for (int index2 = index1 + 1; index2 < recordMaps.Count; ++index2)
      {
        SpecRecordMap recordMap2 = recordMaps[index2];
        if (recordMap2.Record.Part == recordMap1.Record.Part && recordMap2.Record.Position != recordMap1.Record.Position)
          throw new FaultException("Нарушено ограничение на поле Position. Все записи спецификации с одним и тем же компонентом должны иметь одинаковые позиции.");
      }
    }
  }

  private static void CheckPartsConstraint(List<SpecRecordMap> recordMaps)
  {
    int num = recordMaps.Count - 1;
    for (int index1 = 0; index1 < num; ++index1)
    {
      SpecRecordMap recordMap1 = recordMaps[index1];
      for (int index2 = index1 + 1; index2 <= num; ++index2)
      {
        SpecRecordMap recordMap2 = recordMaps[index2];
        if (recordMap2.Rows[0].Part == recordMap1.Rows[0].Part && recordMap2.Record.Part != recordMap1.Record.Part)
          throw new FaultException("Нарушено ограничение на поле Part. Замена в записи спецификации одного компонента на другой не поддерживается.");
      }
    }
  }

  private void UpdateProjects(MappedSpecDummy specDummy)
  {
    for (int index1 = 0; index1 < specDummy.RecordMaps.Count; ++index1)
    {
      SpecRecordMap recordMap = specDummy.RecordMaps[index1];
      string projectDesignation1 = recordMap.ProjectDesignation;
      string projectDesignation2 = recordMap.Record.ProjectDesignation;
      if (projectDesignation1 != projectDesignation2)
      {
        for (int index2 = 0; index2 < recordMap.Rows.Count; ++index2)
        {
          RowData row = recordMap.Rows[index2];
          OccurenceRef occurence = this.GetOccurence(projectDesignation1, row.Refs);
          occurence.Designation = projectDesignation2;
          occurence.Ind = projectDesignation2;
        }
        recordMap.ProjectDesignation = projectDesignation2;
      }
    }
  }

  private void InsertNewRows(
    StructFile structFile,
    MappedSpecDummy specDummy,
    UpdateResult updateResult)
  {
    List<SpecRecordMap> oldRecords = new List<SpecRecordMap>();
    List<SpecRecord> newRecords = new List<SpecRecord>();
    this.SplitRecordsByNovelty(specDummy, oldRecords, newRecords);
    for (int index = 0; index < newRecords.Count; ++index)
    {
      SpecRecord record = newRecords[index];
      if (this.IsNewModificationRecord(record, oldRecords) || this.IsNewPositionRecord(record, oldRecords, newRecords))
      {
        OccurenceRef occurenceRef = new OccurenceRef()
        {
          Designation = record.ProjectDesignation
        };
        occurenceRef.Ind = occurenceRef.Designation;
        RowData rowData = new RowData();
        rowData.OccurenceFormat = OccurenceFormat.OneProject;
        rowData.Refs.Add(occurenceRef);
        SpecRecordMap specRecordMap = new SpecRecordMap();
        specRecordMap.ProjectDesignation = occurenceRef.Designation;
        specRecordMap.Record = record;
        specRecordMap.Rows.Add(rowData);
        structFile.Rows.Add(rowData);
        specDummy.RecordMaps.Add(specRecordMap);
        updateResult.NewRecords.Add(record);
      }
    }
  }

  private void SplitRecordsByNovelty(
    MappedSpecDummy specDummy,
    List<SpecRecordMap> oldRecords,
    List<SpecRecord> newRecords)
  {
    for (int index1 = 0; index1 < specDummy.Records.Count; ++index1)
    {
      SpecRecord record = specDummy.Records[index1];
      int index2 = -1;
      for (int index3 = 0; index3 < specDummy.RecordMaps.Count; ++index3)
      {
        SpecRecordMap recordMap = specDummy.RecordMaps[index3];
        if (record == recordMap.Record)
        {
          index2 = index3;
          break;
        }
      }
      if (index2 == -1)
        newRecords.Add(record);
      else
        oldRecords.Add(specDummy.RecordMaps[index2]);
    }
  }

  private bool IsNewModificationRecord(SpecRecord record, List<SpecRecordMap> oldRecords)
  {
    for (int index = 0; index < oldRecords.Count; ++index)
    {
      SpecRecordMap oldRecord = oldRecords[index];
      if (oldRecord.ProjectDesignation != string.Empty && oldRecord.Record.Part == record.Part && oldRecord.Record.Position == record.Position)
        return true;
    }
    return false;
  }

  private bool IsNewPositionRecord(
    SpecRecord record,
    List<SpecRecordMap> oldRecords,
    List<SpecRecord> newRecords)
  {
    for (int index = 0; index < oldRecords.Count; ++index)
    {
      if (oldRecords[index].Record.Part == record.Part)
        return false;
    }
    for (int index = 0; index < newRecords.Count; ++index)
    {
      SpecRecord newRecord = newRecords[index];
      if (newRecord.Part == record.Part && newRecord.Position != record.Position)
        return false;
    }
    return true;
  }

  private Dictionary<RowData, List<SpecRecordMap>> CollectRowMaps(List<SpecRecordMap> recordMaps)
  {
    Dictionary<RowData, List<SpecRecordMap>> dictionary = new Dictionary<RowData, List<SpecRecordMap>>();
    for (int index1 = 0; index1 < recordMaps.Count; ++index1)
    {
      SpecRecordMap recordMap = recordMaps[index1];
      for (int index2 = 0; index2 < recordMap.Rows.Count; ++index2)
      {
        RowData row = recordMap.Rows[index2];
        List<SpecRecordMap> specRecordMapList;
        if (!dictionary.TryGetValue(row, out specRecordMapList))
        {
          specRecordMapList = new List<SpecRecordMap>();
          dictionary.Add(row, specRecordMapList);
        }
        specRecordMapList.Add(recordMap);
      }
    }
    return dictionary;
  }

  private void UpdateRows(List<RowData> rows, Dictionary<RowData, List<SpecRecordMap>> rowMaps)
  {
    Dictionary<long, PartData> partCache = new Dictionary<long, PartData>();
    for (int index = 0; index < rows.Count; ++index)
    {
      RowData row = rows[index];
      List<SpecRecordMap> rowMap = rowMaps[row];
      SpecRecord record = rowMap[0].Record;
      PartData newPart = this.GetNewPart(record, partCache);
      string position = record.Position;
      if (newPart.TaggingMode == TaggingModes.ImbaseKey)
        newPart.SectionCode = newPart.OriginalSectionCode;
      this.UpdateRow(row, newPart, position, rowMap);
    }
  }

  private PartData GetNewPart(SpecRecord record, Dictionary<long, PartData> partCache)
  {
    PartData newPart;
    if (!partCache.TryGetValue(record.Part.ObjectId, out newPart))
    {
      if (record.Part.OriginalTag == null)
      {
        this.InitPartTaggingMode(record.Part);
        this.InitPartSection(record.Part);
        this.InitPartGuid(record.Part);
      }
      newPart = record.Part.Clone();
      partCache.Add(newPart.ObjectId, newPart);
    }
    return newPart;
  }

  private void InitPartTaggingMode(PartData partData)
  {
    if (string.IsNullOrEmpty(partData.ImbaseKey))
    {
      if (string.IsNullOrEmpty(partData.Designation))
      {
        partData.TaggingMode = TaggingModes.FakeDesignation;
        partData.OriginalTag = Guid.NewGuid().ToString("N");
      }
      else
      {
        partData.TaggingMode = TaggingModes.Designation;
        partData.OriginalTag = partData.Designation;
      }
    }
    else
    {
      partData.TaggingMode = TaggingModes.ImbaseKey;
      partData.OriginalTag = partData.ImbaseKey;
    }
  }

  private void InitPartSection(PartData partData)
  {
    if (partData.TaggingMode == TaggingModes.ImbaseKey)
      partData.OriginalSectionCode = 'I';
    else
      partData.OriginalSectionCode = partData.SectionCode;
  }

  private void InitPartGuid(PartData partData)
  {
    if (!(partData.PartGuid == Guid.Empty))
      return;
    partData.PartGuid = this.guidAllocator.Allocate(partData);
  }

  private void UpdateRow(
    RowData row,
    PartData newPart,
    string newPosition,
    List<SpecRecordMap> recordMaps)
  {
    row.Part = newPart;
    row.PartGuid = newPart.PartGuid;
    row.Position = newPosition;
    for (int index = 0; index < recordMaps.Count; ++index)
    {
      SpecRecordMap recordMap = recordMaps[index];
      if (recordMap.Rows.Count == 1)
        this.UpdateRowFields(row, recordMap, recordMaps.Count == 1);
    }
  }

  private void UpdateRowFields(RowData row, SpecRecordMap recordMap, bool allFields)
  {
    if (allFields)
    {
      row.Zone = recordMap.Record.Zone;
      row.Refs[0].Count = (MeasuredValue) recordMap.Record.Count.Clone();
      row.Note = recordMap.Record.Note;
    }
    else
      this.GetOccurence(recordMap.ProjectDesignation, row.Refs).Count = (MeasuredValue) recordMap.Record.Count.Clone();
  }

  private void RebuildPartData(StructFile structFile)
  {
    List<PartData> parts = structFile.Parts;
    parts.Clear();
    List<RowData> rows = structFile.Rows;
    for (int index = 0; index < rows.Count; ++index)
    {
      RowData rowData = rows[index];
      if (!parts.Contains(rowData.Part))
        parts.Add(rowData.Part);
    }
  }

  private int IndexOfOccurence(string projectDesignation, List<OccurenceRef> occRefs)
  {
    for (int index = 0; index < occRefs.Count; ++index)
    {
      if (occRefs[index].Designation == projectDesignation)
        return index;
    }
    return -1;
  }

  private OccurenceRef GetOccurence(string projectDesignation, List<OccurenceRef> occRefs)
  {
    int index = this.IndexOfOccurence(projectDesignation, occRefs);
    return occRefs[index];
  }
}

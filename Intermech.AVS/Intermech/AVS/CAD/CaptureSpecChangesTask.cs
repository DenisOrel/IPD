// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.CAD.CaptureSpecChangesTask
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Cadmech.Integrator;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS.CAD;

internal sealed class CaptureSpecChangesTask
{
  public void Execute(StructData structData, AVSDocument avsSpec)
  {
    List<AVSRow> allRows = avsSpec.GetAllRows(true, true);
    allRows.RemoveAll((Predicate<AVSRow>) (avsRow => avsRow.RelType != AvsIDCache.Relation_Project));
    allRows.RemoveAll((Predicate<AVSRow>) (avsRow => !SpecSections.IsSectionSupported(avsRow.SectionID)));
    SpecDummy spec = structData.Spec;
    Dictionary<AVSRow, SpecRecord> avsRowStates;
    Dictionary<SpecRecord, bool> dummyRowStates;
    this.ClassifyRows(allRows, spec, out avsRowStates, out dummyRowStates);
    this.RemoveUnusedRows(spec, dummyRowStates);
    this.AddNewRows(spec, allRows, avsRowStates);
    Dictionary<PartData, bool> partStates = new Dictionary<PartData, bool>();
    this.UpdateRowFields(spec, allRows, avsRowStates, partStates);
  }

  private void ClassifyRows(
    List<AVSRow> avsRows,
    SpecDummy specDummy,
    out Dictionary<AVSRow, SpecRecord> avsRowStates,
    out Dictionary<SpecRecord, bool> dummyRowStates)
  {
    avsRowStates = new Dictionary<AVSRow, SpecRecord>();
    dummyRowStates = new Dictionary<SpecRecord, bool>();
    for (int index = 0; index < avsRows.Count; ++index)
    {
      AVSRow avsRow = avsRows[index];
      SpecRecord dummyRow = this.FindDummyRow(avsRow, specDummy);
      if (dummyRow == null)
      {
        avsRowStates.Add(avsRow, (SpecRecord) null);
      }
      else
      {
        avsRowStates.Add(avsRow, dummyRow);
        dummyRowStates.Add(dummyRow, true);
      }
    }
    for (int index = 0; index < specDummy.Records.Count; ++index)
    {
      SpecRecord record = specDummy.Records[index];
      if (!dummyRowStates.ContainsKey(record))
        dummyRowStates.Add(record, false);
    }
  }

  private SpecRecord FindDummyRow(AVSRow avsRow, SpecDummy specDummy)
  {
    for (int index1 = 0; index1 < specDummy.Records.Count; ++index1)
    {
      SpecRecord record = specDummy.Records[index1];
      bool flag = true;
      for (int index2 = 0; index2 < avsRow.Relations.Count; ++index2)
      {
        RelationAttributeValuesCache relation = avsRow.Relations[index2];
        if (this.FindRelation(relation.ProjectId, relation.RelationGuid, record.Relations) == null)
        {
          flag = false;
          break;
        }
      }
      if (flag)
        return record;
    }
    return (SpecRecord) null;
  }

  private SpecRelation FindRelation(
    long projectId,
    Guid relationGuid,
    List<SpecRelation> relations)
  {
    for (int index = 0; index < relations.Count; ++index)
    {
      if (relations[index].ProjectId == projectId && relations[index].RelationGuid == relationGuid)
        return relations[index];
    }
    return (SpecRelation) null;
  }

  private void RemoveUnusedRows(SpecDummy specDummy, Dictionary<SpecRecord, bool> dummyRowStates)
  {
    int index = 0;
    while (index < specDummy.Records.Count)
    {
      SpecRecord record = specDummy.Records[index];
      if (!dummyRowStates[record])
      {
        specDummy.Records.RemoveAt(index);
        dummyRowStates.Remove(record);
      }
      else
        ++index;
    }
  }

  private void AddNewRows(
    SpecDummy specDummy,
    List<AVSRow> avsRows,
    Dictionary<AVSRow, SpecRecord> avsRowStates)
  {
    for (int index = 0; index < avsRows.Count; ++index)
    {
      AVSRow avsRow = avsRows[index];
      if (avsRowStates[avsRow] == null)
      {
        long objectId = avsRow.ObjectId;
        PartData partData = this.FindPart(objectId, specDummy.Parts);
        if (partData == null)
        {
          partData = this.CreateEmptyPart();
          partData.ObjectId = objectId;
          specDummy.Parts.Add(partData);
        }
        SpecRecord emptyRecord = this.CreateEmptyRecord();
        emptyRecord.Part = partData;
        specDummy.Records.Add(emptyRecord);
        avsRowStates[avsRow] = emptyRecord;
      }
    }
  }

  private PartData FindPart(long partId, List<PartData> parts)
  {
    for (int index = 0; index < parts.Count; ++index)
    {
      if (parts[index].ObjectId == partId)
        return parts[index];
    }
    return (PartData) null;
  }

  private PartData CreateEmptyPart()
  {
    return new PartData()
    {
      TaggingMode = TaggingModes.Designation,
      OriginalSectionCode = char.MinValue,
      OriginalTag = (string) null,
      PartGuid = Guid.Empty,
      Designation = string.Empty,
      ImbaseKey = string.Empty,
      Name = string.Empty,
      SectionCode = 'D',
      DocumentFormat = string.Empty,
      OKP = string.Empty,
      Mass = new MeasuredValue(0.0, IDCache.Default.KilogramMeasure.Id),
      Dimensions = string.Empty,
      PosDesignations = string.Empty,
      MaterialId = 0,
      ObjectId = 0,
      OldArticleId = string.Empty
    };
  }

  private SpecRecord CreateEmptyRecord()
  {
    return new SpecRecord()
    {
      Part = (PartData) null,
      Note = string.Empty,
      Zone = string.Empty,
      Position = string.Empty,
      ProjectDesignation = string.Empty,
      Count = new MeasuredValue(0.0, IDCache.Default.ItemsMeasure.Id)
    };
  }

  private void UpdateRowFields(
    SpecDummy specDummy,
    List<AVSRow> avsRows,
    Dictionary<AVSRow, SpecRecord> avsRowStates,
    Dictionary<PartData, bool> partStates)
  {
    for (int index = 0; index < avsRows.Count; ++index)
    {
      AVSRow avsRow = avsRows[index];
      SpecRecord avsRowState = avsRowStates[avsRow];
      if (!partStates.ContainsKey(avsRowState.Part))
      {
        partStates.Add(avsRowState.Part, true);
        this.UpdatePartFields(avsRowState.Part, avsRow);
      }
      this.UpdateRelationFields(avsRowState, avsRow);
    }
  }

  private void UpdatePartFields(PartData partData, AVSRow avsRow)
  {
    AttributeValuesCache objectAttributesCache = avsRow.ObjectAttributesCache;
    partData.Designation = AVSHelper.GetFieldValue<string>(objectAttributesCache, AvsIDCache.Attr_Designation, partData.Designation);
    partData.Name = AVSHelper.GetFieldValue<string>(objectAttributesCache, AvsIDCache.Attr_Name, partData.Name);
    partData.SectionCode = SpecSections.ToSectionCode(avsRow.SectionID, avsRow.Section.Caption);
    partData.DocumentFormat = AVSHelper.GetFieldValue<string>(objectAttributesCache, AvsIDCache.Attr_Format, partData.DocumentFormat);
    partData.OKP = AVSHelper.GetFieldValue<string>(objectAttributesCache, AvsIDCache.Attr_OKPCode, partData.OKP);
    partData.Mass = AVSHelper.CloneFieldValue<MeasuredValue>(objectAttributesCache, AvsIDCache.Attr_Weight, partData.Mass);
    partData.MaterialId = AVSHelper.GetFieldValue<long>(objectAttributesCache, AvsIDCache.Attr_Material, partData.MaterialId);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectAttributesCache.ObjectId, true);
      IDBAttribute attributeById = dbObject.GetAttributeByID(IDCache.Default.ImbaseRef.Id);
      if (attributeById != null && !attributeById.IsNull)
        partData.ImbaseKey = "IG" + dbObject.GUID.ToString();
      Guid guid = dbObject.GUID;
      partData.OldArticleId = guid.ToString("D");
    }
  }

  private void UpdateRelationFields(SpecRecord dummyRow, AVSRow avsRow)
  {
    RelationAttributeValuesCache relation = avsRow.Relations[0];
    dummyRow.Note = AVSHelper.GetFieldValue<string>((AttributeValuesCache) relation, AvsIDCache.Attr_Note, dummyRow.Note);
    dummyRow.Zone = AVSHelper.GetFieldValue<string>((AttributeValuesCache) relation, AvsIDCache.Attr_Zone, dummyRow.Zone);
    dummyRow.Position = AVSHelper.GetFieldValue<string>((AttributeValuesCache) relation, AvsIDCache.Attr_Position, dummyRow.Position);
    dummyRow.Count = AVSHelper.CloneFieldValue<MeasuredValue>((AttributeValuesCache) relation, AvsIDCache.Attr_Count, dummyRow.Count);
    if (avsRow.Relations.Count == 1)
      dummyRow.ProjectDesignation = relation.projInfo.Designation;
    dummyRow.Relations.Clear();
    for (int index = 0; index < avsRow.Relations.Count; ++index)
    {
      SpecRelation specRelation = new SpecRelation(avsRow.Relations[index].ProjectId, avsRow.Relations[index].RelationGuid);
      dummyRow.Relations.Add(specRelation);
    }
  }
}

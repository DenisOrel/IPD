// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.ReadTableAction
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Pdm;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class ReadTableAction : IDecodeAction
{
  public ReadTableAction(IServiceProvider owner)
  {
  }

  public void Run(DecodeData decodeData)
  {
    this.CheckStructTableColumns(decodeData);
    this.DecodeTableData(this.MergeTableRows(decodeData), decodeData);
  }

  private void CheckStructTableColumns(DecodeData decodeData)
  {
    DataTable structTable = decodeData.StructTable;
    for (int index = 0; index < StructTableColumns.InputColumns.Length; ++index)
    {
      if (!structTable.Columns.Contains(StructTableColumns.InputColumns[index]))
        throw new StructFileConfigException(string.Format(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_8"), (object) StructTableColumns.InputColumns[index], (object) decodeData.FieldLayoutFile.FileName));
    }
  }

  private List<ReadTableAction.PartRef> MergeTableRows(DecodeData decodeData)
  {
    Dictionary<string, List<int>> dictionary = new Dictionary<string, List<int>>();
    DataTable structTable = decodeData.StructTable;
    for (int index = 0; index < structTable.Rows.Count; ++index)
    {
      DataRow row = structTable.Rows[index];
      TaggingModes taggingModes = (TaggingModes) Enum.ToObject(typeof (TaggingModes), Convert.ToInt32(row["TAGGING_MODE"]));
      int sectionCode = (int) Convert.ToChar(row["TAG_SECTION_CODE"]);
      string str1 = Convert.ToString(row["TAG"]);
      string str2 = Convert.ToString(row["COD_OKP"]);
      string str3 = Convert.ToString(row["NAME"]);
      int tagMode = (int) taggingModes;
      string tag = str1;
      string okpCode = str2;
      string name = str3;
      string key = PartKey.Calculate((char) sectionCode, (TaggingModes) tagMode, tag, okpCode, name);
      List<int> intList;
      if (!dictionary.TryGetValue(key, out intList))
      {
        intList = new List<int>();
        dictionary.Add(key, intList);
      }
      intList.Add(index);
    }
    List<ReadTableAction.PartRef> partRefList = new List<ReadTableAction.PartRef>();
    foreach (KeyValuePair<string, List<int>> keyValuePair in dictionary)
    {
      ReadTableAction.PartRef partRef = new ReadTableAction.PartRef(keyValuePair.Key, keyValuePair.Value);
      partRefList.Add(partRef);
    }
    return partRefList;
  }

  private void DecodeTableData(List<ReadTableAction.PartRef> partRefs, DecodeData decodeData)
  {
    DataTable structTable = decodeData.StructTable;
    for (int index1 = 0; index1 < partRefs.Count; ++index1)
    {
      ReadTableAction.PartRef partRef = partRefs[index1];
      PartData partData = this.DecodePartData(partRef, structTable);
      decodeData.StructFile.Parts.Add(partData);
      for (int index2 = 0; index2 < partRef.Rows.Count; ++index2)
      {
        RowData rowData = this.DecodeRowData(partRef.Rows[index2], structTable);
        rowData.Part = partData;
        decodeData.StructFile.Rows.Add(rowData);
      }
    }
  }

  private PartData DecodePartData(ReadTableAction.PartRef partRef, DataTable structTable)
  {
    PartData partData = new PartData();
    int row1 = partRef.Rows[0];
    DataRow row2 = structTable.Rows[row1];
    partData.TaggingMode = (TaggingModes) Enum.ToObject(typeof (TaggingModes), Convert.ToInt32(row2["TAGGING_MODE"]));
    partData.OriginalSectionCode = Convert.ToChar(row2["TAG_SECTION_CODE"]);
    partData.OriginalTag = Convert.ToString(row2["TAG"]);
    switch (partData.TaggingMode)
    {
      case TaggingModes.Designation:
        partData.Designation = partData.OriginalTag;
        partData.ImbaseKey = string.Empty;
        break;
      case TaggingModes.FakeDesignation:
        partData.Designation = string.Empty;
        partData.ImbaseKey = string.Empty;
        break;
      case TaggingModes.ImbaseKey:
        partData.Designation = string.Empty;
        partData.ImbaseKey = partData.OriginalTag;
        break;
    }
    partData.OldArticleId = Convert.ToString(row2["DOC_ID"]);
    partData.OKP = Convert.ToString(row2["COD_OKP"]);
    partData.Name = Convert.ToString(row2["NAME"]);
    partData.SectionCode = partData.OriginalSectionCode;
    partData.DocumentFormat = Convert.ToString(row2["SHT-SZ"]);
    partData.PosDesignations = Convert.ToString(row2["POS_DESIGNATIONS"]);
    partData.Dimensions = Convert.ToString(row2["DIMENSIONS"]);
    partData.Mass = this.DecodeMass(Convert.ToString(row2["MASS"]));
    partData.MaterialId = this.DecodeMaterial(Convert.ToString(row2["MATERIAL"]));
    return partData;
  }

  private MeasuredValue DecodeMass(string fieldValue)
  {
    if (string.IsNullOrEmpty(fieldValue))
      return (MeasuredValue) null;
    try
    {
      MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(MeasureHelper.GetBaseMeasureID(IDCache.Default.MassPhysQty.Id));
      MeasuredValue mv = !descriptor.Empty ? MeasureHelper.ConvertToMeasuredValue(fieldValue, descriptor, true) : throw new Exception("Не найдена базовая единица измерения для массы. Возможно, MeasureHelper не был проинициализирован должным образом.");
      MeasureHelper.CorrectCaption(mv);
      return mv;
    }
    catch (Exception ex)
    {
      throw new StructFileParsingException(string.Format(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_9"), (object) "MASS", (object) fieldValue, (object) ex.Message));
    }
  }

  private long DecodeMaterial(string fieldValue)
  {
    if (string.IsNullOrEmpty(fieldValue))
      return 0;
    try
    {
      IArticleService service = ServiceUtils.GetService<IArticleService>((object) ServicesManager.ServiceContainer, true);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
        return service.GetMaterialID(fieldValue, editorRule.OwnerId, (object) sessionKeeper.Session, false);
      }
    }
    catch (Exception ex)
    {
      throw new StructFileParsingException(string.Format(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_10"), (object) "MATERIAL", (object) fieldValue, (object) ex.Message));
    }
  }

  private RowData DecodeRowData(int rowIndex, DataTable structTable)
  {
    RowData rowData = new RowData();
    DataRow row = structTable.Rows[rowIndex];
    rowData.Zone = Convert.ToString(row["ZONE"]);
    rowData.Position = Convert.ToString(row["ORDER_NM"]);
    rowData.PartGuid = this.DecodePartGuid(Convert.ToString(row["GUID"]));
    rowData.Note = Convert.ToString(row["NOTE"]);
    this.DecodeOccurenceRefs(rowData, row);
    return rowData;
  }

  private Guid DecodePartGuid(string fieldValue)
  {
    try
    {
      return string.IsNullOrEmpty(fieldValue) ? Guid.Empty : new Guid(fieldValue);
    }
    catch (Exception ex)
    {
      throw new StructFileParsingException(string.Format(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_11"), (object) "GUID", (object) fieldValue, (object) ex.Message));
    }
  }

  private void DecodeOccurenceRefs(RowData rowData, DataRow data)
  {
    string str = Convert.ToString(data["RELISE"]);
    MeasuredValue numberValue = this.DecodeCount(ReadTableAction.RemoveBrackets(Convert.ToString(data["NUMBER"])));
    if (string.IsNullOrEmpty(str))
    {
      rowData.OccurenceFormat = OccurenceFormat.AllProjects;
      rowData.Refs.AddRange((IEnumerable<OccurenceRef>) this.DecodeSingleRef("<AllProjects>", numberValue));
    }
    else if (str.Contains(","))
    {
      rowData.OccurenceFormat = OccurenceFormat.VariousProjects;
      rowData.Refs.AddRange((IEnumerable<OccurenceRef>) this.DecodeVariousRefs(str, numberValue));
    }
    else
    {
      rowData.OccurenceFormat = OccurenceFormat.OneProject;
      rowData.Refs.AddRange((IEnumerable<OccurenceRef>) this.DecodeSingleRef(this.NormalizeBasicProject(str), numberValue));
    }
  }

  private List<OccurenceRef> DecodeSingleRef(string projectInd, MeasuredValue numberValue)
  {
    return new List<OccurenceRef>(1)
    {
      new OccurenceRef()
      {
        Ind = projectInd,
        Count = (MeasuredValue) numberValue.Clone()
      }
    };
  }

  private List<OccurenceRef> DecodeVariousRefs(string reliseValue, MeasuredValue numberValue)
  {
    string[] strArray = reliseValue.Split(',');
    List<OccurenceRef> occurenceRefList = new List<OccurenceRef>(strArray.Length);
    for (int index = 0; index < strArray.Length; ++index)
    {
      int length = strArray[index].IndexOf('#');
      if (length >= 0)
        occurenceRefList.Add(new OccurenceRef()
        {
          Ind = this.NormalizeBasicProject(strArray[index].Substring(0, length).Trim()),
          Count = this.DecodeCount(strArray[index].Substring(length + 1, strArray[index].Length - length - 1).Trim())
        });
      else
        occurenceRefList.Add(new OccurenceRef()
        {
          Ind = this.NormalizeBasicProject(strArray[index].Trim()),
          Count = (MeasuredValue) numberValue.Clone()
        });
    }
    return occurenceRefList;
  }

  private string NormalizeBasicProject(string ind)
  {
    return !(ind == "0") && !(ind == "00") && !(ind == "000") ? ind : "<BasicProject>";
  }

  private MeasuredValue DecodeCount(string fieldValue)
  {
    try
    {
      MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(IDCache.Default.ItemsMeasure.Id);
      MeasuredValue mv = !descriptor.Empty ? MeasureHelper.ConvertToMeasuredValue(fieldValue, descriptor, true) : throw new Exception("Не найдена единица измерения для количества. Возможно, MeasureHelper не был проинициализирован должным образом.");
      MeasureHelper.CorrectCaption(mv);
      return mv;
    }
    catch (Exception ex)
    {
      throw new StructFileParsingException(string.Format(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_12"), (object) "NUMBER", (object) fieldValue, (object) ex.Message));
    }
  }

  private static string RemoveBrackets(string fieldValue)
  {
    int length = fieldValue.Length;
    return length >= 2 && fieldValue[0] == '(' && fieldValue[length - 1] == ')' ? fieldValue.Substring(1, length - 2) : fieldValue;
  }

  private class PartRef
  {
    private string partKey;
    private List<int> rows;

    public PartRef(string partKey, List<int> rows)
    {
      this.partKey = partKey;
      this.rows = rows;
    }

    public string PartKey => this.partKey;

    public List<int> Rows => this.rows;
  }
}

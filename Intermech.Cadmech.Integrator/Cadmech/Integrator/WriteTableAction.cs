// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.WriteTableAction
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
using System.Text;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class WriteTableAction : IEncodeAction
{
  public void Run(EncodeData encodeData)
  {
    DataTable outputTable = this.CreateOutputTable();
    StructFile structFile = encodeData.StructFile;
    for (int index = 0; index < structFile.Rows.Count; ++index)
    {
      RowData row1 = structFile.Rows[index];
      DataRow row2 = outputTable.NewRow();
      this.WritePartData(row1.Part, row2);
      this.WriteRelationData(row1, row2);
      this.WriteOccData(row1, row2);
      outputTable.Rows.Add(row2);
    }
    encodeData.StructTable = outputTable;
  }

  private DataTable CreateOutputTable()
  {
    DataTable outputTable = new DataTable();
    for (int index = 0; index < StructTableColumns.OutputColumns.Length; ++index)
      outputTable.Columns.Add(StructTableColumns.CreateDataColumn(StructTableColumns.OutputColumns[index]));
    for (int index = 0; index < StructTableColumns.VirtualColumns.Length; ++index)
      outputTable.Columns.Add(StructTableColumns.CreateDataColumn(StructTableColumns.VirtualColumns[index]));
    return outputTable;
  }

  private void WritePartData(PartData partData, DataRow row)
  {
    row["TAGGING_MODE"] = (object) (int) partData.TaggingMode;
    row["TAG_SECTION_CODE"] = (object) partData.OriginalSectionCode;
    row["TAG"] = (object) partData.OriginalTag;
    row["SECTION_CODE"] = (object) partData.SectionCode;
    switch (partData.TaggingMode)
    {
      case TaggingModes.Designation:
        row["DESIGN"] = (object) partData.Designation;
        break;
      case TaggingModes.FakeDesignation:
        row["DESIGN"] = (object) partData.Designation;
        break;
      case TaggingModes.ImbaseKey:
        row["DESIGN"] = (object) partData.ImbaseKey;
        break;
    }
    row["DOC_ID"] = (object) partData.OldArticleId;
    row["NAME"] = (object) partData.Name;
    row["COD_OKP"] = (object) partData.OKP;
    row["SHT-SZ"] = (object) partData.DocumentFormat;
    row["DIMENSIONS"] = (object) partData.Dimensions;
    row["MASS"] = (object) this.EncodeMass(partData.Mass);
    row["MATERIAL"] = (object) this.EncodeMaterial(partData.MaterialId);
  }

  private string EncodeMass(MeasuredValue mass)
  {
    return mass == null || string.IsNullOrEmpty(mass.Caption) ? string.Empty : mass.ToString();
  }

  private string EncodeMaterial(long materialId)
  {
    if (Consts.IsUndefinedObjectId(materialId))
      return string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServiceUtils.GetService<IArticleService>((object) ServicesManager.ServiceContainer, true).GetMaterialName(materialId, (object) sessionKeeper.Session);
  }

  private void WriteRelationData(RowData rowData, DataRow row)
  {
    row["GUID"] = (object) rowData.PartGuid;
    row["NOTE"] = (object) rowData.Note;
    row["ZONE"] = (object) rowData.Zone;
    row["ORDER_NM"] = (object) rowData.Position;
  }

  private void WriteOccData(RowData rowData, DataRow row)
  {
    string str1 = string.Empty;
    string str2 = string.Empty;
    switch (rowData.OccurenceFormat)
    {
      case OccurenceFormat.AllProjects:
        str2 = $"({this.EncodeCount(rowData.Refs[0].Count)})";
        break;
      case OccurenceFormat.OneProject:
        str1 = this.EncodeInd(rowData.Refs[0].Ind);
        str2 = $"({this.EncodeCount(rowData.Refs[0].Count)})";
        break;
      case OccurenceFormat.VariousProjects:
        StringBuilder stringBuilder = new StringBuilder();
        List<OccurenceRef> refs = rowData.Refs;
        int num = refs.Count - 1;
        for (int index = 0; index <= num; ++index)
        {
          stringBuilder.Append(this.EncodeInd(refs[index].Ind));
          stringBuilder.Append('#');
          stringBuilder.Append(this.EncodeCount(refs[index].Count));
          if (index < num)
            stringBuilder.Append(", ");
        }
        str1 = stringBuilder.ToString();
        break;
    }
    row["RELISE"] = (object) str1;
    row["NUMBER"] = (object) str2;
  }

  private string EncodeInd(string ind) => ind == "<BasicProject>" ? "00" : ind;

  private string EncodeCount(MeasuredValue count)
  {
    return count.MeasureID == IDCache.Default.ItemsMeasure.Id ? ((long) Math.Truncate(count.Value)).ToString() : count.ToString();
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.PackOverridedFieldsAction
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System;
using System.Data;
using System.Text;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class PackOverridedFieldsAction : IEncodeAction
{
  public void Run(EncodeData encodeData)
  {
    DataTable structTable = encodeData.StructTable;
    for (int index = 0; index < structTable.Rows.Count; ++index)
    {
      DataRow row = structTable.Rows[index];
      this.CorrectFakeDesignations(row);
      this.PackIndField(row, encodeData);
      this.PackDesignField(row, encodeData);
      this.PackRefField(row, encodeData);
    }
  }

  private void CorrectFakeDesignations(DataRow row)
  {
    if (Convert.ToInt32(row["TAGGING_MODE"]) != 1)
      return;
    string str1 = Convert.ToString(row["TAG"]);
    if (str1[0] != '_')
    {
      StringBuilder stringBuilder = new StringBuilder(str1.Length + 1);
      stringBuilder.Append('_');
      stringBuilder.Append(str1);
      str1 = stringBuilder.ToString();
      row["TAG"] = (object) str1;
    }
    if (!(Convert.ToString(row["DESIGN"]) == string.Empty))
      return;
    string str2 = str1;
    row["DESIGN"] = (object) str2;
  }

  private void PackIndField(DataRow row, EncodeData encodeData)
  {
    char ch = Convert.ToChar(row["TAG_SECTION_CODE"]);
    string str = Convert.ToString(row["TAG"]);
    row["IND"] = (object) $"{ch}{str}";
  }

  private void PackDesignField(DataRow row, EncodeData encodeData)
  {
    char ch = Convert.ToChar(row["SECTION_CODE"]);
    string str = Convert.ToString(row["DESIGN"]);
    row["DESIGN"] = (object) $"{ch}{str}";
  }

  private void PackRefField(DataRow row, EncodeData encodeData)
  {
    if (encodeData.Job.ProcessingMode == StructFileProcessingModes.Technikon)
    {
      row["REF"] = row["POS_DESIGNATIONS"];
    }
    else
    {
      string str1 = Convert.ToString(row["NOTE"]);
      string str2 = Convert.ToString(row["DIMENSIONS"]);
      if (string.IsNullOrEmpty(str2))
        row["REF"] = (object) str1;
      else
        row["REF"] = (object) $"@{str2} {str1}";
    }
  }
}

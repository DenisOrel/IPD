// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.UnpackOverridedFieldsAction
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System;
using System.Data;
using System.Globalization;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class UnpackOverridedFieldsAction : IDecodeAction
{
  private TextInfo textInfo;

  public UnpackOverridedFieldsAction() => this.textInfo = CultureInfo.InvariantCulture.TextInfo;

  public void Run(DecodeData decodeData)
  {
    DataTable structTable = decodeData.StructTable;
    for (int index = 0; index < structTable.Rows.Count; ++index)
    {
      DataRow row = structTable.Rows[index];
      this.SplitIndField(row, decodeData);
      this.SplitRefField(row, decodeData);
    }
  }

  private void SplitIndField(DataRow row, DecodeData decodeData)
  {
    string indValue = Convert.ToString(row["IND"]);
    if (string.IsNullOrEmpty(indValue))
      throw new StructFileParsingException(string.Format(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_25"), (object) "IND"));
    IIndParser[] indParserArray = new IIndParser[3]
    {
      (IIndParser) new WeirdDesignationParser(),
      (IIndParser) new NormalImbaseKeyParser(),
      (IIndParser) new NormalDesignationParser()
    };
    for (int index = 0; index < indParserArray.Length; ++index)
    {
      if (indParserArray[index].CanUnpack(indValue))
      {
        indParserArray[index].Unpack(indValue, row);
        return;
      }
    }
    throw IndParserUtils.MakeCantParseException(indValue);
  }

  private void SplitRefField(DataRow row, DecodeData decodeData)
  {
    string str1 = Convert.ToString(row["REF"]);
    if (decodeData.Job.ProcessingMode == StructFileProcessingModes.Technikon)
    {
      row["NOTE"] = (object) string.Empty;
      row["POS_DESIGNATIONS"] = (object) str1;
    }
    else if (str1.Length >= 1 && str1[0] == '@')
    {
      int num = str1.IndexOf(' ');
      string str2 = num >= 0 ? str1.Substring(1, num - 1).TrimEnd() : string.Empty;
      string str3 = num < 0 || num + 1 >= str1.Length ? string.Empty : str1.Substring(num + 1, str1.Length - num - 1).TrimStart();
      row["DIMENSIONS"] = (object) str2;
      row["NOTE"] = (object) str3;
    }
    else
      row["NOTE"] = (object) str1;
  }
}

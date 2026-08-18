// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.StructTableColumns
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System;
using System.Data;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal static class StructTableColumns
{
  public const string IND = "IND";
  public const string NUMBER = "NUMBER";
  public const string REF = "REF";
  public const string NAME = "NAME";
  public const string DOC_ID = "DOC_ID";
  public const string SHT_SZ = "SHT-SZ";
  public const string ORDER_NM = "ORDER_NM";
  public const string ZONE = "ZONE";
  public const string MASS = "MASS";
  public const string MATERIAL = "MATERIAL";
  public const string COD_OKP = "COD_OKP";
  public const string COD_PZ = "COD_PZ";
  public const string RELISE = "RELISE";
  public const string DESIGN = "DESIGN";
  public const string GUID = "GUID";
  public const string TAGGING_MODE = "TAGGING_MODE";
  public const string TAG_SECTION_CODE = "TAG_SECTION_CODE";
  public const string TAG = "TAG";
  public const string NOTE = "NOTE";
  public const string POS_DESIGNATIONS = "POS_DESIGNATIONS";
  public const string DIMENSIONS = "DIMENSIONS";
  public const string DESIGN_SECTION_CODE = "SECTION_CODE";
  public static readonly string[] InputColumns = new string[14]
  {
    nameof (IND),
    nameof (NUMBER),
    nameof (REF),
    nameof (NAME),
    nameof (DOC_ID),
    "SHT-SZ",
    nameof (ORDER_NM),
    nameof (ZONE),
    nameof (MASS),
    nameof (MATERIAL),
    nameof (COD_OKP),
    nameof (COD_PZ),
    nameof (RELISE),
    nameof (GUID)
  };
  public static readonly string[] OutputColumns = new string[15]
  {
    nameof (IND),
    nameof (NUMBER),
    nameof (REF),
    nameof (NAME),
    nameof (DOC_ID),
    "SHT-SZ",
    nameof (ORDER_NM),
    nameof (ZONE),
    nameof (MASS),
    nameof (MATERIAL),
    nameof (COD_OKP),
    nameof (COD_PZ),
    nameof (RELISE),
    nameof (DESIGN),
    nameof (GUID)
  };
  public static readonly string[] VirtualColumns = new string[7]
  {
    nameof (TAGGING_MODE),
    nameof (TAG_SECTION_CODE),
    nameof (TAG),
    nameof (NOTE),
    nameof (POS_DESIGNATIONS),
    nameof (DIMENSIONS),
    "SECTION_CODE"
  };

  public static DataColumn CreateDataColumn(string fieldName)
  {
    DataColumn dataColumn;
    switch (fieldName)
    {
      case "TAGGING_MODE":
        dataColumn = new DataColumn(fieldName, typeof (int));
        dataColumn.AllowDBNull = false;
        dataColumn.DefaultValue = (object) 0;
        break;
      case "TAG_SECTION_CODE":
        dataColumn = new DataColumn(fieldName, typeof (char));
        dataColumn.AllowDBNull = false;
        dataColumn.DefaultValue = (object) 'D';
        break;
      case "SECTION_CODE":
        dataColumn = new DataColumn(fieldName, typeof (char));
        dataColumn.AllowDBNull = false;
        dataColumn.DefaultValue = (object) 'D';
        break;
      default:
        dataColumn = new DataColumn(fieldName, typeof (string));
        dataColumn.AllowDBNull = false;
        dataColumn.DefaultValue = (object) string.Empty;
        break;
    }
    if (Array.IndexOf<string>(StructTableColumns.VirtualColumns, fieldName) >= 0)
      dataColumn.ExtendedProperties[(object) "VirtualColumn"] = (object) true;
    return dataColumn;
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.Records.FieldRecord
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.ImpExp.Interface;
using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server.Sync.Records;

internal class FieldRecord
{
  public int Key;
  public string Field;
  public string Units;
  public int Sort;
  public int Flags;
  public long Width;
  public ImDataMode DataMode;
  public int Required;
  public FieldTypes DataType;
  public ImEnterMode EnterMode;
  public string Data;
  public string LongName;
  public string ShortName;
  public Guid GUID;

  public FieldRecord(DataRow row)
  {
    this.Key = Convert.ToInt32(row["F_KEY"]);
    this.Field = Convert.ToString(row["F_FIELD"]);
    this.LongName = Convert.ToString(row["F_LONGNAME"]);
    if (this.LongName.Equals(string.Empty))
      this.LongName = "Атрибут";
    this.ShortName = Convert.ToString(row["F_SHORTNAME"]);
    this.Units = Convert.ToString(row["F_UNITS"]);
    this.Sort = Convert.ToInt32(row["F_SORT"]);
    this.Flags = Convert.ToInt32(row["F_FLAGS"]);
    this.DataMode = (ImDataMode) Convert.ToInt32(row["F_TYPE"]);
    this.Required = Convert.ToInt32(row["F_REQUIRED"]);
    this.Width = (long) Convert.ToInt32(row["F_WIDTH"]);
    this.EnterMode = (ImEnterMode) Convert.ToInt32(row["F_ENTERMODE"]);
    this.Data = Convert.ToString(row["F_DATA"]);
    if (!this.Units.Equals(string.Empty))
    {
      this.DataType = FieldTypes.ftMeasured;
    }
    else
    {
      switch (Convert.ToInt32(row["F_DATATYPE"]))
      {
        case 0:
          this.DataType = FieldTypes.ftUnknown;
          break;
        case 1:
          this.DataType = FieldTypes.ftString;
          break;
        case 2:
          this.DataType = FieldTypes.ftInteger;
          break;
        case 3:
          this.DataType = FieldTypes.ftDouble;
          break;
        case 4:
          this.DataType = FieldTypes.ftBoolean;
          break;
        case 5:
          this.DataType = this.IsObjectLinkType(this.EnterMode) ? FieldTypes.ftObjectLink : FieldTypes.ftString;
          break;
        default:
          this.DataType = FieldTypes.ftString;
          break;
      }
    }
    this.GUID = Guid.NewGuid();
  }

  private bool IsObjectLinkType(ImEnterMode mode)
  {
    return mode == ImEnterMode.IEM_FOLDER || mode == ImEnterMode.IEM_SEARCH_DOCUMENT || mode == ImEnterMode.IEM_SEARCH_OBJECT || mode == ImEnterMode.IEM_TABLE;
  }
}

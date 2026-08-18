// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.Records.TableRecord
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server.Sync.Records;

internal class TableRecord
{
  public int Key;
  public string TableName;
  public ImTablesType TableType;
  public ImFileAtt State;
  public string Description;
  public DateTime Created;
  public DateTime Modified;
  public string User;
  public int Openmode;
  public int Order;
  public int TextID;
  public int GraphID;
  public int Access;

  public TableRecord(DataRow row)
  {
    this.Key = Convert.ToInt32(row["F_KEY"]);
    this.TableName = Convert.ToString(row["F_TABLE"]).Trim();
    switch (Convert.ToString(row["F_TYPE"]).Trim())
    {
      case "CATALOG":
        this.TableType = ImTablesType.IMTT_CATALOG;
        break;
      case "CTLREC":
        this.TableType = ImTablesType.IMTT_CTLREC;
        break;
      case "CTLREF":
        this.TableType = ImTablesType.IMTT_CTLREF;
        break;
      case "INDEX":
        this.TableType = ImTablesType.IMTT_INDEX;
        break;
      case "TABLE":
      case "TBLREF":
        this.TableType = ImTablesType.IMTT_TABLE;
        break;
      case "TCREF":
      case "TECHREF":
        this.TableType = ImTablesType.IMTT_TECHREF;
        break;
      default:
        this.TableType = ImTablesType.IMTT_UNKNOWN;
        break;
    }
    if (row["F_STATE"] != DBNull.Value)
      this.State = (ImFileAtt) Convert.ToInt32(row["F_STATE"]);
    this.Description = Convert.ToString(row["F_DESCR"]).Trim();
    if (row["F_CREATED"] != DBNull.Value)
      this.Created = Convert.ToDateTime(row["F_CREATED"]);
    if (row["F_MODIFIED"] != DBNull.Value)
      this.Modified = Convert.ToDateTime(row["F_MODIFIED"]);
    this.User = Convert.ToString(row["F_USER"]).Trim();
    if (row["F_OPENMODE"] != DBNull.Value)
      this.Openmode = Convert.ToInt32(row["F_OPENMODE"]);
    if (row["F_ORDER"] != DBNull.Value)
      this.Order = Convert.ToInt32(row["F_ORDER"]);
    if (row["F_TEXTID"] != DBNull.Value)
      this.TextID = Convert.ToInt32(row["F_TEXTID"]);
    if (row["F_GRAPHID"] != DBNull.Value)
      this.GraphID = Convert.ToInt32(row["F_GRAPHID"]);
    if (row["F_ACCESS"] == DBNull.Value)
      return;
    this.Access = Convert.ToInt32(row["F_ACCESS"]);
  }
}

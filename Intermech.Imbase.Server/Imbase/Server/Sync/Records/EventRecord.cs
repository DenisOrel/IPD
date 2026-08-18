// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.Records.EventRecord
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server.Sync.Records;

internal class EventRecord
{
  public int Key;
  public int Owner;
  public int Type;
  public int Code;
  public char State;
  public string User;
  public string Computer;
  public DateTime Date;
  public int Catalog;
  public int Folder;
  public int Table;
  public int ObjKey;
  public char Source;
  public string Text;
  public object Data;

  public EventRecord()
  {
  }

  public EventRecord(DataRow row)
  {
    this.Key = Convert.ToInt32(row["F_KEY"]);
    this.Owner = Convert.ToInt32(row["F_OWNER"]);
    this.Type = Convert.ToInt32(row["F_TYPE"]);
    this.Code = Convert.ToInt32(row["F_CODE"]);
    if (row["F_STATE"] != DBNull.Value && Convert.ToString(row["F_STATE"]) != string.Empty)
      this.State = Convert.ToChar(row["F_STATE"]);
    this.User = Convert.ToString(row["F_USER"]);
    this.Computer = Convert.ToString(row["F_COMPUTER"]);
    this.Date = Convert.ToDateTime(row["F_DATE"]);
    this.Catalog = Convert.ToInt32(row["F_CATALOG"]);
    this.Folder = Convert.ToInt32(row["F_FOLDER"]);
    this.Table = Convert.ToInt32(row["F_TABLE"]);
    this.ObjKey = Convert.ToInt32(row["F_OBJKEY"]);
    if (row["F_SOURCE"] != DBNull.Value && Convert.ToString(row["F_SOURCE"]) != string.Empty)
      this.Source = Convert.ToChar(row["F_SOURCE"]);
    this.Text = Convert.ToString(row["F_TEXT"]);
    this.Data = row["F_TEXT"];
  }
}

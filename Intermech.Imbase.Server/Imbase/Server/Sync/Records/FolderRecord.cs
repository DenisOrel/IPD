// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.Records.FolderRecord
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server.Sync.Records;

internal class FolderRecord
{
  public int Key;
  public int Owner;
  public int Level;
  public string Name;
  public int Sort;
  public int Mask;
  public int Tag1;
  public int Tag2;
  public int Tag3;
  public int Tag4;
  public int TextID;
  public int GraphID;
  public DateTime Created;
  public string User;

  public FolderRecord(DataRow row)
  {
    this.Key = Convert.ToInt32(row["F_KEY"]);
    if (row["F_OWNER"] != DBNull.Value)
      this.Owner = Convert.ToInt32(row["F_OWNER"]);
    if (row["F_LEVEL"] != DBNull.Value)
      this.Level = Convert.ToInt32(row["F_LEVEL"]);
    this.Name = Convert.ToString(row["F_NAME"]);
    if (row["F_SORT"] != DBNull.Value)
      this.Sort = Convert.ToInt32(row["F_SORT"]);
    if (row["F_MASK"] != DBNull.Value)
      this.Mask = Convert.ToInt32(row["F_MASK"]);
    if (row["F_TAG1"] != DBNull.Value)
      this.Tag1 = Convert.ToInt32(row["F_TAG1"]);
    if (row["F_TAG2"] != DBNull.Value)
      this.Tag2 = Convert.ToInt32(row["F_TAG2"]);
    if (row["F_TAG3"] != DBNull.Value)
      this.Tag3 = Convert.ToInt32(row["F_TAG3"]);
    if (row["F_TAG4"] != DBNull.Value)
      this.Tag4 = Convert.ToInt32(row["F_TAG4"]);
    if (row["F_TEXTID"] != DBNull.Value)
      this.TextID = Convert.ToInt32(row["F_TEXTID"]);
    if (row["F_GRAPHID"] != DBNull.Value)
      this.GraphID = Convert.ToInt32(row["F_GRAPHID"]);
    if (row["F_CREATED"] != DBNull.Value)
      this.Created = Convert.ToDateTime(row["F_CREATED"]);
    this.User = Convert.ToString(row["F_USER"]);
  }
}

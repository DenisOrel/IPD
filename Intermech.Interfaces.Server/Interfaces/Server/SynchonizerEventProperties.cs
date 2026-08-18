// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.SynchonizerEventProperties
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Server;

[Serializable]
public class SynchonizerEventProperties
{
  public long KeyID { get; private set; }

  public string SourceServer { get; private set; }

  public string DestinationServer { get; private set; }

  public Guid Guid { get; private set; }

  public string StringInfo { get; private set; }

  public DateTime EventDate { get; private set; }

  public bool DeleteOnStart { get; private set; }

  public SynchonizerEventProperties(DataRow row)
  {
    this.KeyID = (long) Convert.ToInt32(row["F_KEY"]);
    this.SourceServer = row["F_SERVER_SRC"].ToString();
    this.DestinationServer = row["F_SERVER_DST"].ToString();
    this.Guid = new Guid(row["F_GUID"].ToString());
    this.StringInfo = row["F_STRING_INFO"].ToString();
    this.EventDate = Convert.ToDateTime(row["F_DATE"]);
    this.DeleteOnStart = Convert.ToInt16(row["F_DELETE_ON_START"]) != (short) 0;
  }

  public SynchonizerEventProperties(
    string destServer,
    Guid eventGuid,
    string stringInfo,
    bool deleteOnStart)
  {
    this.KeyID = 0L;
    this.SourceServer = string.Empty;
    this.DestinationServer = destServer;
    this.Guid = eventGuid;
    this.StringInfo = stringInfo;
    this.EventDate = DateTime.UtcNow;
    this.DeleteOnStart = deleteOnStart;
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.RelationImportedEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

#nullable disable
namespace Intermech.Interfaces.Server;

public class RelationImportedEventArgs : SessionableEventArgs
{
  public int RelationType { get; private set; }

  public long ProjectID { get; private set; }

  public long PartID { get; private set; }

  public bool PartIsNew { get; private set; }

  public RelationImportedEventArgs(
    IUserSession session,
    int relationType,
    long projectId,
    long partID,
    bool partIsNew)
    : base(session)
  {
    this.RelationType = relationType;
    this.ProjectID = projectId;
    this.PartID = partID;
    this.PartIsNew = partIsNew;
  }
}
